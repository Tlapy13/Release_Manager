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

namespace Release_Manager
{
    public partial class Form1 : Form
    {
        RecordsManager rm;
        private readonly ILogger _logger = new SerilogClass().logger;
        private JsonHandler _jsonHandler = new JsonHandler();
        private Settings settingsForm;
        private RecordsManager records;
        Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public Form1()
        {
            InitializeComponent();
            SetFunctionality(false);
            LoadConfig();
        }

        private void LoadConfig()
        {
            try
            {
                DirectoryInfo xmlFolder;
                DirectoryInfo reportFolder;
                string path = Path.GetFullPath(configFile.AppSettings.Settings["solutions_config_path"].Value);
                string xmlFinal = Directory.GetCurrentDirectory() + "\\xml\\final.xml";
                string reports = Directory.GetCurrentDirectory() + "\\reports";
              //  string TempXML = Directory.GetCurrentDirectory() + "\\TempXML";
                if (!File.Exists(path))
                    AddDummyDataToSolutionsConfigs(_jsonHandler);

                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\xml"))
                    xmlFolder = Directory.CreateDirectory("xml");

                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\reports"))
                {
                    reportFolder = Directory.CreateDirectory(reports);
                    configFile.AppSettings.Settings["pdf_folder_path"].Value = reports;
                }
                

                if (!File.Exists(xmlFinal))
                {
                    File.Create(xmlFinal);
                    configFile.AppSettings.Settings["final_xml_file_path"].Value = xmlFinal;
                }
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
                _logger.Error($"Error occurred while attempting to open and read JSON content: {ex.InnerException.Message}");
                throw new Exception("GetFileSpecsFromJson - specs loading issue occured", ex);
            }
            configFile.Save();
        }

        /// <summary>
        /// Create XML report of selected directory including subdirectories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Createevidence_rep_Click(object sender, EventArgs e)
        {
            _logger.Debug("============== Started to create a new report right now. ============== ");

            if (Directory.Exists(txt_Apppath.Text))
            {
                statusBox.Text += "Solution hash is being calculated, please wait.";
                string path = txt_Apppath.Text;
                //string dateAndTime = " '' " + DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString() + " '' ";
               string xmlPath = Path.GetFullPath(
                configFile.AppSettings.Settings["temporary_xml_file_path"].Value);
               // string xmlPath = $"{configFile.AppSettings.Settings["temporary_xml_file_path"].Value}\\Temp_{ DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}.xml";

                records = new RecordsManager(path, xmlPath);
                if (records.CurrentRecords == null)
                {
                    MessageError("Report cannot be created. Check error log.");
                    return;
                }
                records.SaveXML(xmlPath);

                var solutionHash = records.SolutionHash;
                statusBox.Clear();
                statusBox.ForeColor = Color.Black;
                statusBox.Text += $"Solution hash: \r\n{solutionHash}";
                SetFunctionality(true);
                btn_Viewevidence_rep.Focus();

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            else
                MessageError("Selected directory does not exist. Create one or Select another one.");
        }

        

        private void SetFunctionality(bool enable)
        {
            btn_Createevidence_rep.Enabled = !enable;
            btn_Saveevidence_rep.Enabled = enable;
            btn_Viewevidence_rep.Enabled = enable;
        }

        private void btn_Viewevidence_rep_Click(object sender, EventArgs e)
        {
           
            Form2 fm2 = new Form2(records);
            fm2.Show();
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
        
            StringBuilderData sd = new StringBuilderData();
            sd.Header = "Evidence Report Created: ";
            sd.FileName = $"{configFile.AppSettings.Settings["pdf_folder_path"].Value}\\REPORT_{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}.pdf";
            sd.ChangeId = txt_Changeid.Text;
            sd.Note = txt_Note.Text;
            sd.rm = records;

            PdfWriterCls pdf = new PdfWriterCls(sd);


            //if (sb.Length == 0)
            //{
            //    pdf.WriteToPdf("no changes detected");
            //}

            //pdf.WriteToPdf(sb);
          
            txt_Changeid.Text = String.Empty;
            txt_Note.Text = String.Empty;
            string temp = $"{configFile.AppSettings.Settings["temporary_xml_file_path"].Value}";
            string final = $"{configFile.AppSettings.Settings["final_xml_file_path"].Value}";
            CopyXmlDocument(temp, final);
            //File.Replace(temp, final,temp);

            MessageOK("Evidence report has been generated.");
        }
            private static void CopyXmlDocument(string temp, string final)
            {
             
                XmlDocument document = new XmlDocument();
                document.Load(temp);

                // Modify XML file using XmlDocument here

              //  Console.WriteLine(document.OuterXml);
                if (File.Exists(final))
                  // File.Delete(final);
                document.Save(final);
      
          //  File.Delete(temp);

        }


        

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

        private void Form1_Load(object sender, EventArgs e)
        {
            cbo_App.BackColor = Color.AliceBlue;
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            settingsForm = new Settings();
            settingsForm.CustomFormClosed += CloseListener;
            settingsForm.Show(this);
        }

        private void fileToolStripMenuItem_Click(Object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(Object sender, EventArgs e)
        {

        }

        private void CloseListener(object sender, EventArgs e)
        {
            _jsonHandler.DeserializeConfigFile();
            cbo_App.DataSource = _jsonHandler.SolutionsConfigs;
            cbo_App.Refresh();
            if (_jsonHandler.SolutionsConfigs.Count == 0)
                txt_Apppath.Text = String.Empty;
        }

        public void AddDummyDataToSolutionsConfigs(JsonHandler jsonHandler)
        {
            _logger.Debug("No JSON configuration file supplied. New dummy one is to be created.");
            jsonHandler.SolutionsConfigs.Add(new SolutionsConfig() { SolutionName = "Example1Folder", SolutionPath = "C:\\Projects\\ExampleProject1" });
            jsonHandler.SolutionsConfigs.Add(new SolutionsConfig() { SolutionName = "Example2Folder", SolutionPath = "C:\\Projects\\ExampleProject2" });
            jsonHandler.SolutionsConfigs.Add(new SolutionsConfig() { SolutionName = "Example3Folder", SolutionPath = "C:\\Projects\\ExampleProject3" });
            jsonHandler.SerializeConfigFile();
            _logger.Debug("Dummy data added to JSON configuration file.");
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

        private void txt_Apppath_TextChanged(object sender, EventArgs e)
        {

        }
    }
}