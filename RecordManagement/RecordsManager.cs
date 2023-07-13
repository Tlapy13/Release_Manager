using Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
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
        public int recordsProcessed = 0;
        private readonly ILogger _logger = new SerilogClass().logger;

        /// <summary>
        /// Constructor takes two collections - current check of directory files and previous check of a directory files.
        /// </summary>
        /// <param name="currentRecords">File collection retrieved by current check.</param>
        /// <param name="savedRecords">File collection stored from previous check.</param>
        public RecordsManager(string currentRecords, string savedRecords)
        {
            ReadRecords(currentRecords, savedRecords);
        }

        public void ReadRecords(string currentRecords, string savedRecords)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            SavedRecords = Records.LoadFromXML(savedRecords);
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            _logger.Debug("Loading data for SavedRecords took: {elapsedTime} miliseconds.", elapsedTime);

            watch.Reset();
            watch.Start();

            CurrentRecords = Records.ReadDirectory(currentRecords);
            if (CurrentRecords == null)
                return;

            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds;
            _logger.Debug("Loading data for CurrentRecords took: {elapsedTime} miliseconds.", elapsedTime);
        }

        public void CheckRecords()
        {
            foreach (Record currentRecord in CurrentRecords)
            {
                if (SavedRecords.TryGetValue(currentRecord, out Record savedRecord))
                {
                    if (currentRecord.Hash != savedRecord.Hash)
                    {
                        currentRecord.Changed = true;
                        currentRecord.Version = savedRecord.Version + 1;
                    }
                }
            };
        }

        public void SaveXML(string File)
        {
            CurrentRecords.SaveToXML(File);
            _logger.Debug("Data has been saved to XML file successfully.");
        }

        public void GetSolutionHash()
        {
            StringBuilder sb = new StringBuilder();

            string hashlist = "";
            foreach (Record record in CurrentRecords)
            {
                hashlist += record.Hash;
            }

            using (var md5 = MD5.Create())
            {
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(hashlist));
                foreach (byte b in hashValue)
                {
                    sb.Append($"{b:X2}");
                }
            }

            SolutionHash = sb.ToString();
            _logger.Debug("MD5 solution hash computed: {SolutionHash} .", SolutionHash);
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
