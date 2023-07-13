using FileScanner;
using Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;

namespace Release_Manager
{
    public partial class Overview : Form
    {
        RecordsManager rm;
        private readonly ILogger _logger = new SerilogClass().logger;


        public Overview(FileScanner.RecordsManager rmData)
        {   
            InitializeComponent();
            rm = rmData;
            
            ShowViewResult();
        }

        private void Print(string title, List<Record> records)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(title);
            sb.Append(Environment.NewLine);
            sb.AppendLine(string.Concat(Enumerable.Repeat("-", title.Length - 1)));
            records.ForEach(r => sb.Append(r.ToString() + Environment.NewLine));
            sb.AppendLine(Environment.NewLine);

            textBox2.AppendText(sb.ToString());
            sb.Clear();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

    

        private async Task ShowViewResult()
        {
            textBox2.Clear();

            if (DeletedChk.Checked)
                Print("Removed files: ", await rm.RemovedFiles());

            if (ChangedChk.Checked)
                Print("Changed files: ", await rm.ChangedFiles());

            if (AddedChk.Checked)
                Print("New files: ", await rm.NewFiles());

            if (UnchangedChk.Checked)
                Print("Unchanged files: ", await rm.UnchangedFiles());

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
