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

namespace Release_Manager
{
    public partial class Form1 : Form
    {
        RecordsManager rm;
        private readonly ILogger _logger = new SerilogClass().logger;
        private JsonHandler _jsonHandler = new JsonHandler();
        private Settings settingsForm;

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
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                string path = Path.GetFullPath(
                    configFile.AppSettings.Settings["solutions_config_path"].Value);

                if (!File.Exists(path))
                {
                    AddDummyDataToSolutionsConfigs(_jsonHandler);
                }

                _logger.Debug($"Attempting to load JSON file stored at: {path}");

                //SolutionsConfig sc = new SolutionsConfig();
                //sc.SolutionName = "Test";
                //sc.SolutionPath = @"E:\Download\FileScanner";
                //solutions.Add(sc);

                //sc = new SolutionsConfig();
                //sc.SolutionName = "Test2";
                //sc.SolutionPath = @"E:\Download\Demo_AsyncAwaitSQLClient";
                //solutions.Add(sc);

                //using (StreamWriter writer = new StreamWriter(path))
                //{
                //    JsonSerializer.SerializeToWriter(solutions, writer);
                //}

                //solutions.Clear();

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


                comboBox1.DataSource = _jsonHandler.SolutionsConfigs;

            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while attempting to open and read JSON content: {ex.InnerException.Message}");
                throw new Exception("GetFileSpecsFromJson - specs loading issue occured", ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox1.Text))
            {
                statusBox.Text += "Solution hash is being calculated, please wait.";
                string path = textBox1.Text;
                string xmlPath = Directory.GetCurrentDirectory() + "\\test.xml";
                rm = new RecordsManager(path, xmlPath);
                rm.SaveXML(xmlPath);

                var solutionHash = GetSolutionHash(path);
                statusBox.Clear();
                statusBox.Text += $"Solution hash: \r\n {solutionHash}";
                SetFunctionality(true);
                button3.Focus();

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            else
            {
                MessageError("Selected directory does not exist. Create one or Select another one.");
            }
            
        }

        private string GetSolutionHash(string path)
        {
            string solutionHash = String.Empty;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var files = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
                        .OrderBy(p => p)
                        .ToList();

            _logger.Debug($"{files.Count} file(s) found in solution.");

            SHA512 sha512 = SHA512.Create();
            Parallel.ForEach(files, file =>
            {
                string fileTmp = file;

                string relativePath = file.Substring(path.Length + 1);
                byte[] pathBytes = Encoding.UTF8.GetBytes(relativePath.ToLower());
                sha512.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);

                // hash contents
                byte[] contentBytes = File.ReadAllBytes(fileTmp);
                if (file == files.Last())
                {
                    sha512.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
                    solutionHash = BitConverter.ToString(sha512.Hash).Replace("-", "").ToLower();
                }
                else
                    sha512.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
            });
            
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            _logger.Debug("SHA512 solution calculation took: {elapsedTime} miliseconds.", elapsedTime);
            statusBox.Text = String.Empty;
            return solutionHash;
        }

        private void SetFunctionality(bool enable)
        {
            button1.Enabled = !enable;
            button2.Enabled = enable;
            button3.Enabled = enable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //TODO: change naming convention
            Form2 fm2 = new Form2(rm);
            fm2.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            statusBox.Clear();
            SetFunctionality(false);
            textBox1.Text = (comboBox1.SelectedItem as SolutionsConfig).SolutionPath + "\\" + (comboBox1.SelectedItem as SolutionsConfig).SolutionName;
            textBox2.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TODO: change naming convention
            StringBuilderData sd = new StringBuilderData();
            sd.Header = "Evidence Reported Created: ";
            sd.FileName = "test2.pdf";
            sd.ChangeId = textBox2.Text;
            sd.Note = textBox3.Text;
            sd.rm = rm;

            PdfWriterCls pdf = new PdfWriterCls(sd);

       
            //if (sb.Length == 0)
            //{
            //    pdf.WriteToPdf("no changes detected");
            //}

            //pdf.WriteToPdf(sb);
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
                    sb.AppendLine("ModificationDate: " + r.ModificationDate.ToString());
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
            comboBox1.BackColor = Color.AliceBlue;
            
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
            comboBox1.DataSource = _jsonHandler.SolutionsConfigs;
            comboBox1.Refresh();
            if (_jsonHandler.SolutionsConfigs.Count == 0)
                textBox1.Text = String.Empty;
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

    }
}