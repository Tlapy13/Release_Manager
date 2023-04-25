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

        public RecordsManager(string directory, string File)
        {

           
           SavedRecords = Records.LoadFromXML(File);
          

            CurrentRecords = Records.ReadDirectory(directory);

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
            }
        }

        public void SaveXML(string File)
        {
            CurrentRecords.SaveToXML(File);
        }

        public List<Record> NewFiles()
        {
            return CurrentRecords.Except(SavedRecords).ToList();
        }

        public List<Record> ChangedFiles()
        {
            return CurrentRecords.Where(x => x.Changed).ToList();
        }

        public List<Record> RemovedFiles()
        {
            return SavedRecords.Except(CurrentRecords).ToList();
        }
        public List<Record> UnchangedFiles()
        {
            return CurrentRecords.Where(x => !x.Changed).ToList();
        }
    }
}
