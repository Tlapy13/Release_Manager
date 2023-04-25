using FileScanner;
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

namespace Release_Manager
{
    public partial class Form1 : Form
    {
        RecordsManager rm;
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
                List<SolutionsConfig> solutions = new List<SolutionsConfig>();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "solutions_config.json");

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
                        solutions = JsonSerializer.DeserializeFromReader<List<SolutionsConfig>>(reader);
                    }
                }
                else
                    MessageBox.Show("Solution configuration file solutions_config.json was not found!");

                comboBox1.DataSource = solutions;

            }
            catch (Exception ex)
            {

                throw new Exception("GetFileSpecsFromJson - specs loading issue occured", ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string path = textBox1.Text;
            rm = new RecordsManager(path, "test.xml");
            rm.SaveXML("test.xml");
            label5.Text = GetSolutionHash(path);
            SetFunctionality(true);
            button3.Focus();
        }

        private string GetSolutionHash(string path)
        {
            MessageBox.Show("implement me");
            return "";
        }

        private void SetFunctionality(bool enable)
        {
            button1.Enabled = !enable;
            button2.Enabled = enable;
            button3.Enabled = enable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2(rm);
            fm2.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFunctionality(false);
            textBox1.Text = (comboBox1.SelectedItem as SolutionsConfig).SolutionPath;
            textBox2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}