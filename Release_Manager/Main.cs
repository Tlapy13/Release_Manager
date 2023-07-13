using FileScanner;
using iText.IO.Util;
using Serilog;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logging;
using System.Configuration;
using System.Security.Cryptography;
using SerilogTimings;
using System.Xml;
using System.Net.Mail;
using System.DirectoryServices.AccountManagement;
using System.Runtime.Remoting.Channels;
using Microsoft.Win32;
using System.Security.Principal;
using System.Threading;

namespace Release_Manager
{
    public partial class Main : Form
    {
        private RecordsManager _rm;
        private readonly ILogger _logger = new SerilogClass().logger;
        private JsonHandler _jsonHandler = new JsonHandler();
        private Settings _settingsForm;
        private RecordsManager _records;

        public Main()
        {
            InitializeComponent();
            SetFunctionality(false);
            LoadConfig();
        }

        private void LoadConfig()
        {
            try
            {
                string path = Path.GetFullPath(ConfigurationManager.AppSettings["solutions_config_path"]);
                if (!File.Exists(path))
                    AddDummyDataToSolutionsConfigs(_jsonHandler);

                SetSenderEmail();
                CheckPdfReportFolderPresence();
                CheckXmlFolderPresence();

                _logger.Debug($"Attempting to load JSON file stored at: {path}");

                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        _jsonHandler.SolutionsConfigs = JsonSerializer.DeserializeFromReader<List<SolutionsConfig>>(reader);
                    }
                }
                else
                {
                    MessageBox.Show("Solution configuration file solutions_config.json was not found!");
                    _logger.Warning("Solution configuration file not found, nor created automatically.");
                }
                cbo_App.DataSource = _jsonHandler.SolutionsConfigs;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while attempting to open and read JSON content: {ex}");
            }
        }

        /// <summary>
        /// Create XML report of selected directory including subdirectories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Createevidence_rep_Click(object sender, EventArgs e)
        {
            statusBox.AppendText("Solution check in progress, please wait.");
            CreateEvidenceReport();
        }

        private void SetFunctionality(bool enable)
        {
            btn_Createevidence_rep.Enabled = !enable;
            btn_Saveevidence_rep.Enabled = enable;
            btn_Viewevidence_rep.Enabled = enable;
        }

        private void btn_Viewevidence_rep_Click(object sender, EventArgs e)
        {
           
            Overview overview = new Overview(_records);
            overview.ShowDialog();
        }

        private void cbo_App_SelectedIndexChanged(object sender, EventArgs e)
        {
            statusBox.Clear();
            SetFunctionality(false);
            txt_Apppath.Text = Path.Combine((cbo_App.SelectedItem as SolutionsConfig).SolutionPath, (cbo_App.SelectedItem as SolutionsConfig).SolutionName);
            txt_Changeid.Focus();
        }

        private void btn_Saveevidence_rep_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string xmlFinalPath = ConfigurationManager.AppSettings["final_xml_file_path"] + $"\\{(cbo_App.SelectedItem as SolutionsConfig).SolutionName}.xml";
                configFile.AppSettings.Settings["final_xml_file_path"].Value = xmlFinalPath;
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                Properties.Settings.Default.Reload();

                StringBuilderData sd = new StringBuilderData();
                sd.Header = "Evidence Report Created: ";
                sd.FileName = $"{configFile.AppSettings.Settings["pdf_folder_path"].Value}\\REPORT_{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}.pdf";
                sd.ChangeId = txt_Changeid.Text;
                sd.Note = txt_Note.Text;
                sd.rm = _records;

                PdfWriterCls pdf = new PdfWriterCls(sd);

                txt_Changeid.Text = String.Empty;
                txt_Note.Text = String.Empty;

                CopyXmlDocument(ConfigurationManager.AppSettings["temporary_xml_file_path"], ConfigurationManager.AppSettings["final_xml_file_path"]);

                MessageOK("Evidence report has been generated.");
                
                string userCanBeNotified = $"{ConfigurationManager.AppSettings["notifyUserByEmail"]}";
                if (userCanBeNotified == "true")
                    NotifyUserByEmail(new Attachment(sd.FileName));

                SetXmlFinalPathToDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                MessageError("Some error occurred while creating PDF or sending PDF attachment. Check log.");
                return;
            }
        }

        private static void CopyXmlDocument(string temp, string final)
        {
            XmlDocument document = new XmlDocument();
            document.Load(temp);
            document.Save(final);
        }

        // What's the purpose of this?
        private StringBuilder ExportObjectData(string title, List<Record> records)
        {
            if (records.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(title);
                sb.AppendLine(string.Concat(Enumerable.Repeat("-", title.Length - 1)));
                sb.AppendLine(Environment.NewLine);

                foreach (var r in records)
                {
                    sb.AppendLine("Filename: " + r.Filename);
                    sb.AppendLine("Hash: " + r.Hash);
                    sb.AppendLine("Date modified: " + r.ModificationDate.ToString());
                    sb.AppendLine("Version: " + r.Version.ToString());
                    sb.AppendLine();
                }
                sb.AppendLine(Environment.NewLine);

                return sb;
            }
            return null;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            cbo_App.BackColor = Color.AliceBlue;
        }

        private void ShowAboutApplication(object sender, EventArgs e)
        {
            //TODO: add changelog as separate window with textbox?
            MessageBox.Show("(c) 2023 Martin Tlapak");
        }

        private void QuitApplication(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            _settingsForm = new Settings();
            _settingsForm.CustomFormClosed += CloseListener;
            _settingsForm.ShowDialog(this);
        }

        private void fileToolStripMenuItem_Click(Object sender, EventArgs e)
        {

        }

        //TODO: delete?
        private void textBox2_TextChanged(Object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Provide feedback (the update of solution list in combobox) to parent window (Main) when Settings are closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseListener(object sender, EventArgs e)
        {
            _jsonHandler.DeserializeConfigFile();
            cbo_App.DataSource = _jsonHandler.SolutionsConfigs;
            cbo_App.Refresh();
            if (_jsonHandler.SolutionsConfigs.Count == 0)
                txt_Apppath.Text = String.Empty;
        }

        /// <summary>
        /// Adds dummy data to solution configuration JSON file in case it is missing.
        /// </summary>
        /// <param name="jsonHandler"></param>
        public void AddDummyDataToSolutionsConfigs(JsonHandler jsonHandler)
        {
            try
            {
                _logger.Debug("No JSON configuration file supplied. New dummy one is to be created.");
                jsonHandler.SolutionsConfigs.Add(new SolutionsConfig() { SolutionName = "Example1Folder", SolutionPath = "C:\\Projects\\ExampleProject1" });
                jsonHandler.SolutionsConfigs.Add(new SolutionsConfig() { SolutionName = "Example2Folder", SolutionPath = "C:\\Projects\\ExampleProject2" });
                jsonHandler.SolutionsConfigs.Add(new SolutionsConfig() { SolutionName = "Example3Folder", SolutionPath = "C:\\Projects\\ExampleProject3" });
                jsonHandler.SerializeConfigFile();
                _logger.Debug("Dummy data added to JSON configuration file.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }

        }

        private void MessageOK(string message)
        {
            statusBox.ForeColor = Color.Green;
            statusBox.Text = message;
        }

        private void MessageError(string message)
        {
            statusBox.ForeColor = Color.Red;
            statusBox.Text = message;
        }

        private void ClearStatusBoxTextWhenClicked(Object sender, MouseEventArgs e)
        {
            if (statusBox.Text != String.Empty)
            {
                statusBox.Text = String.Empty;
                statusBox.ForeColor = Color.Black;
            }
        }

        //TODO: delete?
        private void txt_Apppath_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Notify the user by sending an e-mail with PDF report attachment.
        /// </summary>
        /// <param name="file"></param>
        private void NotifyUserByEmail(Attachment file)
        {
            string sender = ConfigurationManager.AppSettings["report_sender"];
            string recipient = ConfigurationManager.AppSettings["report_recipient"];

            if (!string.IsNullOrEmpty(sender))
            {
                EmailNotifier notification = new EmailNotifier(sender, UserPrincipal.Current.DisplayName, recipient, file);
                if (notification.Status.Contains("ERROR:"))
                    MessageError(notification.Status);
                else
                    MessageOK(statusBox.Text + "\r\n" + notification.Status);
            }

        }

        /// <summary>
        /// Creates XML report of selected solution and compares it to previous one if existing.
        /// </summary>
        private void CreateEvidenceReport()
        {
            try
            {
                _logger.Debug("============== Started to create a new report right now. ============== ");

                if (Directory.Exists(txt_Apppath.Text))
                {
                    string path = txt_Apppath.Text;
                    string xmlPath = Path.GetFullPath(ConfigurationManager.AppSettings["temporary_xml_file_path"]);

                    List<string> files = new List<string>();

                    Directory.GetFiles(Path.GetDirectoryName(xmlPath))
                        .ToList()
                        .ForEach(file => files.Add(Path.GetFileNameWithoutExtension(file)));

                    _logger.Debug($"Report is being created for: {path}");

                    if (files.Any(file => new DirectoryInfo(path).Name == Path.GetFileNameWithoutExtension(file)))
                    {
                        _records = new RecordsManager(path, xmlPath);
                        _records.CheckRecords();
                        _records.GetSolutionHash();
                    }
                    else
                    {
                        _records = new RecordsManager(path, path);
                        _records.CheckRecords();
                        _records.GetSolutionHash();
                    }

                    _records.SaveXML(xmlPath);

                    var solutionHash = _records.SolutionHash;
                    statusBox.Clear();
                    statusBox.ForeColor = Color.Black;
                    statusBox.Text += $"Solution hash: \r\n{solutionHash}";
                    SetFunctionality(true);
                    
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                else
                    MessageError("Selected directory does not exist. Create one or Select another one.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        private void CheckPdfReportFolderPresence()
        {
            try
            {
                if (!Directory.Exists(Path.GetFullPath(ConfigurationManager.AppSettings["pdf_folder_path"])))
                {
                    Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    DirectoryInfo reports = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\reports");
                    configFile.AppSettings.Settings["pdf_folder_path"].Value = reports.FullName;
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    Properties.Settings.Default.Reload();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        /// <summary>
        /// If final XML folder is missing, new one is created in application folder.
        /// If there is also temporary XML folder path missing, it will utilize folder used by final XML files.
        /// </summary>
        private void CheckXmlFolderPresence()
        {
            try
            {
                if (!Directory.Exists(Path.GetFullPath(ConfigurationManager.AppSettings["final_xml_file_path"])))
                {
                    Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    DirectoryInfo xmlFolder = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\xml");
                    configFile.AppSettings.Settings["final_xml_file_path"].Value = xmlFolder.FullName;
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    Properties.Settings.Default.Reload();
                }

                if (!Directory.Exists(Path.GetFullPath(ConfigurationManager.AppSettings["temporary_xml_file_path"])) &&
                     Directory.Exists(Path.GetFullPath(ConfigurationManager.AppSettings["final_xml_file_path"])))
                {
                    Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    string temporaryXmlPath = Path.GetFullPath(ConfigurationManager.AppSettings["final_xml_file_path"]) + "\\temp.xml";
                    new Records().SaveToXML(temporaryXmlPath);
                    configFile.AppSettings.Settings["temporary_xml_file_path"].Value = temporaryXmlPath;
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    Properties.Settings.Default.Reload();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Sets current user e-mail address as sender using domain record (UserPrincipal).
        /// </summary>
        private void SetSenderEmail()
        {
            Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configFile.AppSettings.Settings["report_sender"].Value = UserPrincipal.Current.EmailAddress;
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            Properties.Settings.Default.Reload();
        }

        private void SetXmlFinalPathToDefault()
        {
            string file = Path.GetFullPath(ConfigurationManager.AppSettings["final_xml_file_path"]);
            string xmlFinalDirectory = Path.GetDirectoryName(file);

            Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configFile.AppSettings.Settings["final_xml_file_path"].Value = xmlFinalDirectory;
            configFile.Save();
            ConfigurationManager.RefreshSection("appSettings");
            Properties.Settings.Default.Reload();
        }
    }
}