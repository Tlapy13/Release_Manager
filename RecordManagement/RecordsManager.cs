using Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FileScanner
{
    public class RecordsManager
    {

        public Records SavedRecords;
        public Records CurrentRecords;
        private readonly ILogger _logger = new SerilogClass().logger;

        public RecordsManager(string directory, string File)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                SavedRecords = Records.LoadFromXML(File);
                CurrentRecords = Records.ReadDirectory(directory);

                Parallel.ForEach(CurrentRecords, currentRecord =>
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
                });
                watch.Stop();
                var elapsedTime = watch.ElapsedMilliseconds;
                _logger.Debug("Comparison of records took: {elapsedTime} miliseconds.", elapsedTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException.Message);
            }
            
        }

        public void SaveXML(string File)
        {
            CurrentRecords.SaveToXML(File);
            _logger.Debug("Data has been saved to XML file successfully.");

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
