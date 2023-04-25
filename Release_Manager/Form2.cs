using FileScanner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Release_Manager
{
    public partial class Form2 : Form
    {
        RecordsManager rm;
        public Form2(FileScanner.RecordsManager rmData)
        {   
            InitializeComponent();
            rm = rmData;
            
            ShowViewResult();
        }

        private void Print(string title, List<Record> records)
        {
            textBox2.AppendText(title);
            textBox2.AppendText(Environment.NewLine);
            textBox2.AppendText(string.Concat(Enumerable.Repeat("-", title.Length-1)));
            textBox2.AppendText(Environment.NewLine);

            foreach (var r in records)
            {
                textBox2.AppendText(r.ToString());
                textBox2.AppendText(Environment.NewLine);
            }
            textBox2.AppendText(Environment.NewLine);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

    

        private void ShowViewResult()
        {
            textBox2.Clear();

            if (DeletedChk.Checked)
                Print("Removed files: ", rm.RemovedFiles());

            if (ChangedChk.Checked)
                Print("Changed files: ", rm.ChangedFiles());

            if (AddedChk.Checked)
                Print("New files: ", rm.NewFiles());

            if (UnchangedChk.Checked)
                Print("Unchanged files: ", rm.UnchangedFiles());

            textBox2.SelectionStart = 0;
            textBox2.ScrollToCaret();
        }

        private void ChangedChk_CheckedChanged(object sender, EventArgs e)
        {
            ShowViewResult();
        }

        private void DeletedChk_CheckedChanged(object sender, EventArgs e)
        {
            ShowViewResult();
        }

        private void UnchangedChk_CheckedChanged(object sender, EventArgs e)
        {
            ShowViewResult();
        }
        private void NewChk_CheckedChanged(object sender, EventArgs e)
        {
            ShowViewResult();
        }
    }
}
