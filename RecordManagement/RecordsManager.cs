using Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace FileScanner
{
    public class RecordsManager
    {

        public Records SavedRecords;
        public Records CurrentRecords;
        public string SolutionHash;
        private readonly ILogger _logger = new SerilogClass().logger;

        public RecordsManager(string directory, string File)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            SavedRecords = Records.LoadFromXML(File);
            CurrentRecords = Records.ReadDirectory(directory);
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            _logger.Debug("Loading data for SavedRecords and CurrentRecords took: {elapsedTime} miliseconds.", elapsedTime);

            foreach (Record currentRecord in CurrentRecords)
            {
                Record savedRecord;
                if (SavedRecords.TryGetValue(currentRecord, out savedRecord))
                {
                    if (currentRecord.Hash != savedRecord.Hash)
                    {
                        currentRecord.Changed = true;
                        currentRecord.Version = savedRecord.Version + 1;
                    }
                }
            };


            GetSolutionHash();
        }

        public void SaveXML(string File)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            CurrentRecords.SaveToXML(File);
            _logger.Debug("Data has been saved to XML file successfully.");
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            _logger.Debug("Saving records to XML took: {elapsedTime} miliseconds.", elapsedTime);

        }

        private void GetSolutionHash()
        {

            StringBuilder sb = new StringBuilder();

            string hashlist = "";
            foreach (Record record in CurrentRecords)
            {
                hashlist += record.Hash;
            }


            var watch = System.Diagnostics.Stopwatch.StartNew();
            using (var md5 = MD5.Create())
            {
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(hashlist));
                foreach (byte b in hashValue)
                {
                    sb.Append($"{b:X2}");
                }
            }
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            _logger.Debug("MD5 solution calculation took: {elapsedTime} miliseconds.", elapsedTime);

            SolutionHash = sb.ToString();
            sb.Clear();
        }

        public async Task<List<Record>> NewFiles()
        {
            return CurrentRecords.Except(SavedRecords).ToList();
        }

        public async Task<List<Record>> ChangedFiles()
        {
            return CurrentRecords.Where(x => x.Changed).ToList();
        }

        public async Task<List<Record>> RemovedFiles()
        {
            return SavedRecords.Except(CurrentRecords).ToList();
        }
        public async Task<List<Record>> UnchangedFiles()
        {
            return CurrentRecords.Where(x => !x.Changed).ToList();
        }
    }
}
