using FileScanner;
using Release_Manager;
using ServiceStack.Text.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Release_Manager
{
    public class StringBuilderFormatter : AbstractStringBuilderFormatter
    {
        public StringBuilderFormatter(StringBuilderData SBData)
        {
            _sb = new StringBuilder();

            WriteLine("Evidence Reported Created: " + DateTime.Now.ToString());
            WriteLine("Change ID: " + SBData.ChangeId);
            WriteLine("Note:");
            WriteLine(SBData.Note);
            WriteEmptyLine();
            WritteRM(SBData.rm);
            Save();
        }

        private void WritteRecordData(string title, IEnumerable<object> records)
        {
            if (records is List<Record>)
            {
                List<Record> recordList = records as List<Record>;
                if (recordList.Count > 0)
                {
                    _sb.AppendLine(title);
                    _sb.AppendLine(string.Concat(Enumerable.Repeat("-", title.Length - 1)));
                    _sb.AppendLine(Environment.NewLine);

                    foreach (var r in recordList)
                    {
                        _sb.AppendLine("Filename: " + r.Filename);
                        _sb.AppendLine("Hash: " + r.Hash);
                        _sb.AppendLine("ModificationDate: " + r.ModificationDate.ToString());
                        _sb.AppendLine("Version: " + r.Version.ToString());
                        _sb.AppendLine();
                    }
                    _sb.AppendLine(Environment.NewLine);
                }

            }
            else
                throw new Exception("invalid object provided, please implement new class if needed");

        }
        private async Task WritteRM(RecordsManager rm)
        {
            WritteRecordData("Removed files: ", await rm.RemovedFiles());
            WritteRecordData("Changed files: ", await rm.ChangedFiles());
            WritteRecordData("New files: ", await rm.NewFiles());

            if ((await rm.RemovedFiles()).Count == 0 && (await rm.ChangedFiles()).Count == 0 && (await rm.NewFiles()).Count == 0)
            {
                WriteLine("no changes detected");
            }
        }

    }
}

public class StringBuilderData
{
    public string ChangeId { get; set; }
    public string Note { get; set; }
    public string Header { get; set; }
    public string FileName { get; set; }
    public RecordsManager rm { get; set; }
}

