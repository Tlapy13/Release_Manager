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

namespace Release_Manager
{
    public partial class Settings : Form
    {
        private readonly ILogger _logger = new SerilogClass().logger;
        private JsonHandler jsonHandler = new JsonHandler();
        private BindingSource _bindingSource = new BindingSource();
        Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


        public Settings()
        {
            InitializeComponent();
            PopulateSolutionsConfigTable();
            PopulateTextBoxes();
        }


        /// <summary>
        /// Load values stored in application configuration file.
        /// </summary>
        private void PopulateTextBoxes()
        {
            logPathTextBox.Text =
                configFile.AppSettings.Settings["serilog:write-to:File.path"].Value;
            
            configPathTextbox.Text = Path.GetFullPath(
                configFile.AppSettings.Settings["solutions_config_path"].Value);

            xmlPathTextBox.Text = Path.GetFullPath(
                configFile.AppSettings.Settings["final_xml_file_path"].Value);

            logPathTextBox.ReadOnly = true;
            configPathTextbox.ReadOnly = true;
            xmlPathTextBox.ReadOnly = true;
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
                    var record = jsonHandler.SolutionsConfigs.Single(item => item.SolutionPath == folderPathTextbox.Text);

                    jsonHandler.SolutionsConfigs.Remove(record);
                    _bindingSource.Remove(record);

                    dataGridView1.Update();
                    dataGridView1.Refresh();

                    _logger.Debug($"This solution {folderNameTextbox.Text} : {folderPathTextbox.Text} was removed from JSON configuration file.");
                    folderNameTextbox.Text = String.Empty;
                    folderPathTextbox.Text= String.Empty;

                    MessageOK("Selected record has been deleted from the list.");
                }
                else
                    MessageError("No item was selected, no item could not have been deleted.");


            }
            catch (Exception ex)
            {
                _logger.Error($"Error encountered while attempted to delete this record: {folderNameTextbox.Text}, {folderPathTextbox.Text}. Error message: {ex.InnerException.Message}");
                MessageError("Selected record cannot be deleted. Check log for more details.");
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

        private void SelectXMLPath(Object sender, EventArgs e)
        {
            try
            {
                xmlPathDialog.Filter = "XML Files (*.xml)|*.xml";
                DialogResult result = xmlPathDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    xmlPathTextBox.Text = xmlPathDialog.FileName;
                    _logger.Debug($"This XML solution file collection is selected: {xmlPathDialog.FileName}");
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"XML path was not selected. See error details: {ex.InnerException.Message}");
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



        /// <summary>
        /// Save changes if all paths are supplied and configuration was serialized successfully.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges(Object sender, EventArgs e)
        {
            if (
                ChangePath("serilog:write-to:File.path", logPathTextBox) && 
                ChangePath("solutions_config_path", configPathTextbox) &&
                ChangePath("final_xml_file_path", xmlPathTextBox) &&
                jsonHandler.SerializeConfigFile())
            {
                MessageOK("All changes have been saved successfully.");
            }
            else
                MessageError("Updated log/configuration path cannot be changed. See log for errors.");
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
                configFile.AppSettings.Settings[key].Value = box.Text;
                configFile.Save();
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

                statusBox.ForeColor = Color.Green;
                statusBox.Text = "Log/configuration/xml path has been updated.";
                
                if (box.Name == "logPathTextBox")
                    _logger.Debug($"This is current log path: {box.Text}.\n");
                else if (box.Name == "configPathTextbox")
                    _logger.Debug($"This is current solution config path: {box.Text}.\n");
                else
                    _logger.Debug($"This is XML final file path: {box.Text}.\n");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"Log path was not changed. See error details: {ex.InnerException.Message}");
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
            }
            catch (Exception ex)
            {
                _logger.Error($"Solution path was not selected. See error details: {ex.InnerException.Message}");
            }
        }

        private void SelectSolutionPath(Object sender, CancelEventArgs e)
        {
            folderPathTextbox.Text = solutionPathBrowserDialog.SelectedPath;
            _logger.Debug($"This solution is selected: {folderPathTextbox.Text}");
        }

        

        private void SelectDataGridViewRow(Object sender, DataGridViewCellEventArgs e)
        {
            folderNameTextbox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            folderPathTextbox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            folderNameTextbox.ReadOnly = true;
            folderPathTextbox.ReadOnly = true;
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
                
                xmlPathTextBox.Text = Directory.GetCurrentDirectory() + $"\\xml\\final.xml";

                _bindingSource.Clear();
                dataGridView1.DataSource = _bindingSource;
                jsonHandler.SolutionsConfigs.Clear();

                new Form1().AddDummyDataToSolutionsConfigs(jsonHandler);
                PopulateSolutionsConfigTable();


                if (!Directory.Exists("C:\\Logs"))
                {
                    Directory.CreateDirectory("C:\\Logs");
                    _logger.Debug("Log directory re-created at C:\\Logs");
                }

                if (!File.Exists(xmlPathTextBox.Text))
                {
                    File.Create(xmlPathTextBox.Text);
                }

                _logger.Debug($"JSON Solution configuration file renewed and stored at {configPathTextbox.Text}");

                ChangePath("serilog:write-to:File.path", logPathTextBox);
                ChangePath("solutions_config_path", configPathTextbox);
                ChangePath("final_xml_file_path", xmlPathTextBox);

                _logger.Debug("Application settings resetted.");
                MessageOK("Application settings resetted.");


            }
            catch (Exception ex)

            {
                _logger.Error($"JSON configuration backup file not found, it must be probably deleted or moved. Add JSON file manually to app folder.\r\n" +
                    $"See also error message detail: {ex.Message}, {ex.InnerException.Message}");
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

        private void SettingsClosed(object sender, FormClosedEventArgs e)
        {
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
    }


}
