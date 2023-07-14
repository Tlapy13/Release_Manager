using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Tsp;
using Serilog;
using Logging;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Net;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace Release_Manager
{
    public partial class Settings : Form
    {
        private readonly ILogger _logger = new SerilogClass().logger;
        private JsonHandler jsonHandler = new JsonHandler();
        private BindingSource _bindingSource = new BindingSource();

        public Settings()
        {
            InitializeComponent();
            PopulateSolutionsConfigTable();
            PopulateTextBoxes();
            if (ConfigurationManager.AppSettings["notifyUserByEmail"] == "true")
            {
                userNotificationCheckbox.Checked = true;
                ChangeEmailSettingsAccessibility();
            }
            else
            {
                userNotificationCheckbox.Checked = false;
                ChangeEmailSettingsAccessibility();
            }
        }


        /// <summary>
        /// Load values stored in application configuration file.
        /// </summary>
        private void PopulateTextBoxes()
        {
            logPathTextBox.Text =
                ConfigurationManager.AppSettings["serilog:write-to:File.path"];
            
            configPathTextbox.Text = Path.GetFullPath(
                ConfigurationManager.AppSettings["solutions_config_path"]);

            xmlPathTextBox.Text = Path.GetFullPath(
                ConfigurationManager.AppSettings["final_xml_file_path"]);

            reportPathTextBox.Text = Path.GetFullPath(
                ConfigurationManager.AppSettings["pdf_folder_path"]);

            if (ConfigurationManager.AppSettings["notifyUserByEmail"] == "true")
                    userNotificationCheckbox.Checked = true;
            else 
                userNotificationCheckbox.Checked = false;


            logPathTextBox.ReadOnly = true;
            configPathTextbox.ReadOnly = true;
            xmlPathTextBox.ReadOnly = true;
            reportPathTextBox.ReadOnly = true;
            
        }

        /// <summary>
        /// Load key-value pairs from JSON configuration file into DataGridView.
        /// </summary>
        /// <returns></returns>
        private void PopulateSolutionsConfigTable()
        {            
            if (!jsonHandler.DeserializeConfigFile())
                MessageError($"JSON configuration might be empty ({jsonHandler.SolutionsConfigs.Count} records found) or corrupted. See log for more details.");
            
            if (!(jsonHandler.SolutionsConfigs is null))
            { 
                foreach (SolutionsConfig solution in jsonHandler.SolutionsConfigs)
                {
                    _bindingSource.Add(solution);
                }
            }
            dataGridView1.DataSource = _bindingSource;
        }

        private void AddSolution(Object sender, EventArgs e)
        {
            if (
                folderNameTextbox.Text != String.Empty && 
                folderPathTextbox.Text != String.Empty &&
                !jsonHandler.SolutionsConfigs.Any(x => x.SolutionPath == folderPathTextbox.Text && x.SolutionName == folderNameTextbox.Text))
            {
                var solution = new SolutionsConfig
                {
                    SolutionName = folderNameTextbox.Text.Trim(),
                    SolutionPath = folderPathTextbox.Text.Trim()
                };

                jsonHandler.SolutionsConfigs.Add(solution);
                _bindingSource.Add(solution);

                dataGridView1.Update();
                dataGridView1.Refresh();

                folderNameTextbox.Text = String.Empty;
                folderPathTextbox.Text = String.Empty;

                MessageOK("New record added to the list.");
                _logger.Debug($"New record added to list: {folderNameTextbox.Text} : {folderPathTextbox.Text}");
            }
            else
            {
                MessageError("No solution or existing one was selected. Record cannot be added.");
            }
        }

        /// <summary>
        /// Remove solution from JSON configuration file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveSolution(Object sender, EventArgs e)
        {
            try
            {
                if (folderPathTextbox.Text != String.Empty)
                {
                    var record = jsonHandler.SolutionsConfigs.Single(item => Path.Combine(item.SolutionPath, item.SolutionName) == Path.Combine(folderPathTextbox.Text, folderNameTextbox.Text));

                    jsonHandler.SolutionsConfigs.Remove(record);
                    _bindingSource.Remove(record);

                    dataGridView1.Update();
                    dataGridView1.Refresh();

                    _logger.Debug($"This solution {folderNameTextbox.Text} : {folderPathTextbox.Text} was removed from JSON configuration file.");
                    folderNameTextbox.Text = String.Empty;
                    folderPathTextbox.Text = String.Empty;
                    xmlPathTextBox.Text = String.Empty;

                    MessageOK("Selected record has been deleted from the list.");
                }
                else
                    MessageError("No item was selected, no item could not have been deleted.");


            }
            catch (Exception ex)
            {
                _logger.Error($"Error encountered while attempted to delete this record: {folderNameTextbox.Text}, {folderPathTextbox.Text}. Error message: {ex.InnerException.Message}");
                MessageError("Selected record cannot be deleted. Check log for more details.");
                return;
            }
        }

        /// <summary>
        /// Store selected log directory path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectLogPath(Object sender, EventArgs e)
        {
            if (logPathBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                logPathTextBox.Text = logPathBrowserDialog.SelectedPath;
                logPathTextBox.Text += "\\logs-.txt";
            }
        }

        private void SelectConfigPath(Object sender, EventArgs e)
        {
            try
            {
                configPathDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
                DialogResult result = configPathDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    configPathTextbox.Text = configPathDialog.FileName;
                    _logger.Debug($"This JSON configuration file is selected: {configPathDialog.FileName}");
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"Solution path was not selected. See error details: {ex.InnerException.Message}");
            }
        }

        private void SelectReportPath(Object sender, EventArgs e)
        {
            if (reportPathBrowserDialog.ShowDialog() == DialogResult.OK)
                reportPathTextBox.Text = reportPathBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// Save changes if all paths are supplied and configuration was serialized successfully.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges(Object sender, EventArgs e)
        {

            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (
                ChangePath("serilog:write-to:File.path", logPathTextBox) &&
                ChangePath("solutions_config_path", configPathTextbox) &&
                // ChangePath("final_xml_file_path", xmlPathTextBox) &&
                ChangePath("pdf_folder_path", reportPathTextBox) &&
                IsEmailAddressValid(senderTextbox.Text) &&
                IsEmailAddressValid(recipientTextbox.Text) &&
                ChangePath("report_sender", senderTextbox) &&
                ChangePath("report_recipient", recipientTextbox) &&
                ChangePath("smtphost", smtpTextbox) &&
                jsonHandler.SerializeConfigFile())
                {
                    MessageOK("All changes have been saved successfully.");
                    
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    Properties.Settings.Default.Reload();

                }
                else
                    MessageError("Settings change cannot be saved. See log for errors.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());   
            }
            
        }

        /// <summary>
        /// Change path for selected item (log folder / configuration folder).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        private bool ChangePath(string key, TextBox box)
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configFile.AppSettings.Settings[key].Value = box.Text;

                statusBox.ForeColor = Color.Green;
                statusBox.Text = "Log/configuration/xml path has been updated.";

                if (box.Name == "logPathTextBox")
                    _logger.Debug($"This is current log path: {box.Text}.\n");
                else if (box.Name == "configPathTextbox")
                    _logger.Debug($"This is current solution config path: {box.Text}.\n");
                else if (box.Name == "reportPathTextBox")
                    _logger.Debug($"This is current report path: {box.Text}.\n");
                else if (box.Name == "recipientTextbox")
                    _logger.Debug($"This is current recipient e-mail: {box.Text}.\n");
                else if (box.Name == "senderTextbox")
                    _logger.Debug($"This is current sender e-mail: {box.Text}.\n");
                else if (box.Name == "smtpTextbox")
                    _logger.Debug($"This is current smtp server: {box.Text}.\n");
                else
                    _logger.Debug($"This is XML final file path: {box.Text}.\n");


                configFile.Save();
                ConfigurationManager.RefreshSection("appSettings");
                Properties.Settings.Default.Reload();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"Log path was not changed. See error details: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Store selected solution path and filename to textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectSolutionFile(Object sender, EventArgs e)
        {
            try
            {
                DialogResult result = solutionPathBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _logger.Debug($"This solution is selected: {solutionPathBrowserDialog.SelectedPath}");
                    folderPathTextbox.Text = Path.GetDirectoryName(solutionPathBrowserDialog.SelectedPath);
                    folderNameTextbox.Text = Path.GetFileName(solutionPathBrowserDialog.SelectedPath);
                }
                UpdateXmlFinalPathTextbox();
            }
            catch (Exception ex)
            {
                _logger.Error($"Solution path was not selected. See error details: {ex.InnerException.Message}");
            }
        }

        private void SelectDataGridViewRow(Object sender, DataGridViewCellEventArgs e)
        {
            folderNameTextbox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            folderPathTextbox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            folderNameTextbox.ReadOnly = true;
            folderPathTextbox.ReadOnly = true;
            UpdateXmlFinalPathTextbox();
        }

        private void ClearStatusBoxTextWhenClicked(Object sender, MouseEventArgs e)
        {
            if (statusBox.Text != String.Empty)
            {
                statusBox.Text = String.Empty;
                statusBox.ForeColor = Color.Black;
            }

        }

        private void ResetSettings(Object sender, EventArgs e)
        {
            try
            {
                logPathTextBox.Text = "C:\\Logs\\Release_Manager-.txt";
                configPathTextbox.Text = Directory.GetCurrentDirectory() + "\\solutions_config.json";
                xmlPathTextBox.Text = Directory.GetCurrentDirectory() + "\\xml";
                reportPathTextBox.Text = Directory.GetCurrentDirectory() + "\\reports";
                senderTextbox.Text = UserPrincipal.Current.EmailAddress;
                recipientTextbox.Text = "abc@appension.dk";
                smtpTextbox.Text = "outlook.appension.dk";

                _bindingSource.Clear();
                dataGridView1.DataSource = _bindingSource;
                jsonHandler.SolutionsConfigs.Clear();

                new Main().AddDummyDataToSolutionsConfigs(jsonHandler);
                PopulateSolutionsConfigTable();


                if (!Directory.Exists("C:\\Logs"))
                {
                    Directory.CreateDirectory("C:\\Logs");
                    _logger.Debug("Log directory re-created at C:\\Logs");
                }

                if (!Directory.Exists(xmlPathTextBox.Text))
                {
                    Directory.CreateDirectory(xmlPathTextBox.Text);
                }

                _logger.Debug($"JSON Solution configuration file renewed and stored at {configPathTextbox.Text}");

                ChangePath("serilog:write-to:File.path", logPathTextBox);
                ChangePath("solutions_config_path", configPathTextbox);
                ChangePath("final_xml_file_path", xmlPathTextBox);
                ChangePath("pdf_folder_path", reportPathTextBox);
                ChangePath("report_sender", senderTextbox);
                ChangePath("report_recipient", recipientTextbox);
                ChangePath("smtphost", smtpTextbox);



                _logger.Debug("Application settings resetted.");
                MessageOK("Application settings resetted.");
            }
            catch (Exception ex)

            {
                _logger.Error($"JSON configuration backup file not found, it must be probably deleted or moved. Add JSON file manually to app folder.\r\n" +
                    $"See also error message detail: {ex}");
                MessageError("Reset cannot be executed - missing or corrupted JSON configuration file in application folder.");
                return;
            }

            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void Cancel(Object sender, EventArgs e)
        {
            logPathTextBox.Text = String.Empty;
            configPathTextbox.Text = String.Empty;

            Close();
        }

        private void OpenLogsFolder(Object sender, EventArgs e)
        {
            string logDirectory = Path.GetDirectoryName(logPathTextBox.Text);
            if (Directory.Exists(logDirectory))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = logDirectory,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
            else
                MessageError("Directory does not exist. Create new one or specify path to existing one.");
        }

        public delegate void CustomFormClosedHandler(object sender, FormClosedEventArgs e);
        public event CustomFormClosedHandler CustomFormClosed;

        /// <summary>
        /// Pass feedback to parent window. When Settings windows is closed, final XML path is set to folder path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsClosed(object sender, FormClosedEventArgs e)
        {
            string xmlFinalDirectory = new DirectoryInfo(Path.GetFullPath(ConfigurationManager.AppSettings["final_xml_file_path"])).FullName;
            
            Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configFile.AppSettings.Settings["final_xml_file_path"].Value = xmlFinalDirectory;
            configFile.Save();
            ConfigurationManager.RefreshSection("appSettings");
            Properties.Settings.Default.Reload();

            CustomFormClosed(sender, e);
        }
        
        private void MessageOK (string message)
        {
            statusBox.ForeColor = Color.Green;
            statusBox.Text = message;
        }

        private void MessageError (string message)
        {
            statusBox.ForeColor = Color.Red;
            statusBox.Text = message;
        }

        private void UserNotificationAllowed(Object sender, EventArgs e)
        {
            ChangeEmailSettingsAccessibility();
        }

        private void ChangeEmailSettingsAccessibility()
        {
            if (userNotificationCheckbox.Checked)
            {
                senderTextbox.Text = ConfigurationManager.AppSettings["report_sender"];
                recipientTextbox.Text = ConfigurationManager.AppSettings["report_recipient"];
                smtpTextbox.Text = ConfigurationManager.AppSettings["smtphost"];

                senderTextbox.ReadOnly = false;
                recipientTextbox.ReadOnly = false;
                smtpTextbox.ReadOnly = false;

                senderTextbox.BackColor = System.Drawing.SystemColors.Window;
                recipientTextbox.BackColor = System.Drawing.SystemColors.Window;
                smtpTextbox.BackColor = System.Drawing.SystemColors.Window;

                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configFile.AppSettings.Settings["notifyUserByEmail"].Value = "true";
                configFile.Save();
                ConfigurationManager.RefreshSection("appSettings");
                Properties.Settings.Default.Reload();
            }
            else
            {
                senderTextbox.ReadOnly = true;
                recipientTextbox.ReadOnly = true;
                smtpTextbox.ReadOnly = true;

                senderTextbox.Text = String.Empty;
                recipientTextbox.Text = String.Empty;
                smtpTextbox.Text = String.Empty;

                senderTextbox.BackColor = System.Drawing.SystemColors.InactiveCaption;
                recipientTextbox.BackColor = System.Drawing.SystemColors.InactiveCaption;
                smtpTextbox.BackColor = System.Drawing.SystemColors.InactiveCaption;

                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configFile.AppSettings.Settings["notifyUserByEmail"].Value = "false";
                configFile.Save();
                ConfigurationManager.RefreshSection("appSettings");
                Properties.Settings.Default.Reload();
            }
        }

        private void CheckEmailValidityInSenderTextbox(Object sender, EventArgs e)
        {
            if (!IsEmailAddressValid(senderTextbox.Text))
                MessageError("Entered sender e-mail is not valid. It has to appear in format: user-initials@appension.dk, e.g.: abc@appension.dk");
            else
                MessageOK("");

        }

        private void CheckEmailValidityInRecipientTextbox(Object sender, EventArgs e)
        {
            if (!IsEmailAddressValid(recipientTextbox.Text))
                MessageError("Entered recipient e-mail is not valid. It has to appear in format: user-initials@appension.dk, e.g.: abc@appension.dk");
            else
                MessageOK("");
        }

        private bool IsEmailAddressValid(string email)
        {
            if (userNotificationCheckbox.CheckState == CheckState.Checked)
            {
                string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
               + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
               + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

                Regex rule = new Regex(pattern, RegexOptions.IgnoreCase);
                bool result = rule.IsMatch(email);
                if (!result)
                    _logger.Debug($"Entered e-mail address - {email} - is not in correct format. Sender e-mail can only belong to appension.dk domain.");
                return rule.IsMatch(email);
            }
            else
                return true;
           
        }

        private void UpdateXmlFinalPathTextbox()
        {
            xmlPathTextBox.Text = Path.GetFullPath(ConfigurationManager.AppSettings["final_xml_file_path"] + "\\" + folderNameTextbox.Text + ".xml");
        }
    }
}
