using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Serilog;
using Logging;
using System.Runtime.CompilerServices;

namespace FileScanner
{
    [Serializable]
    public class Records : HashSet<Record>, ISerializable
    {
        private readonly ILogger _logger = new SerilogClass().logger;

        public void SaveToXML(string file)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Records));
                xs.Serialize(sw, this);
            }
        }

        public static Records LoadFromXML(string file)
        {
            if (!File.Exists(file))
                return new Records();

            using (StreamReader sr = new StreamReader(file))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Records));
                return (Records)xs.Deserialize(sr);
            }
        }

        public static Records ReadDirectory(string directory)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Records ret = new Records();
            string[] entries;
            if (directory == "" || !Directory.Exists(directory))
            {
                ret._logger.Warning("No directory path is supplied.");
                ret = null;
            }
            else
            {
                entries = Directory.GetFileSystemEntries(directory, "*", SearchOption.AllDirectories);

                foreach (string file in entries)
                {
                    string hash;
                    if (IsDir(file))
                    {
                        hash = "Directory";
                    }
                    else
                    {
                        hash = CalculateMD5(file);
                    }
                    Record record = new Record();
                    FileInfo fi = new FileInfo(file);



                    string dir = Path.GetDirectoryName(file);

                    if (directory != dir)
                    {
                        //record.shortFileName = Path.Combine(dir, fi.Name);
                        string result = dir.Substring(directory.Length).TrimStart(Path.DirectorySeparatorChar);
                        record.shortFileName = Path.Combine(result, fi.Name);
                    }
                    else
                        record.shortFileName = fi.Name;


                    record.Filename = file;
                    record.Hash = hash;
                    record.CreateDate = fi.CreationTime;
                    record.ModificationDate = fi.LastWriteTime;
                    record.Version = 0;
                    ret.Add(record);
                }
                watch.Stop();
                var elapsedTime = watch.ElapsedMilliseconds;
                ret._logger.Debug("ReadDirectory method took: {elapsedTime} miliseconds while reading of {entries.Length}.", elapsedTime, entries?.Length);
            }
            
            return ret;
        }

        private static void EnsureDirectoryExists(string filePath)
        {
            try
            {
                FileInfo fi = new FileInfo(filePath);
                if (!fi.Directory.Exists)
                {
                    System.IO.Directory.CreateDirectory(fi.DirectoryName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EnsureDirectoryExists - Directory creation/access issue occured", ex);
            }
        }

        private static bool IsDir(string entry)
        {
            FileAttributes attr = File.GetAttributes(entry);
            return attr.HasFlag(FileAttributes.Directory);
        }

        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }


    }
}
