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
using System.Security.Policy;

namespace FileScanner
{
    [Serializable]
    public class Records : HashSet<Record>, ISerializable
    {
        private static readonly ILogger _logger = new SerilogClass().logger;

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
            Records ret = new Records();

            if (directory == "" || !Directory.Exists(directory))
                _logger.Warning("No directory path is supplied.");
            else
            {
                string[] entries = Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories).ToArray();
                Record record;
                FileInfo fi;
                string hash;

                foreach (string file in entries)
                {
                    try
                    {
                        record = new Record();
                        fi = new FileInfo(file);

                        if (new Records().IsDir(file))
                            hash = "Directory";
                        else
                            hash = CalculateMD5(file);

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
                    catch (Exception ex)
                    {
                        _logger.Error($"Unable to process file due to file name length exceeding 255 characters." +
                            $"\r\nFile name contains {file.Length} characters." +
                            $"\r\nSee also error: \r\n{ex.Message}");
                        return null;
                    }
                }
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

        public bool IsDir(string entry)
        {
            bool result;
            try
            {
                FileAttributes attr = File.GetAttributes(entry);
                result = attr.HasFlag(FileAttributes.Directory);
            }
            catch (Exception e) 
            {
                _logger.Error($"Path name is probably too long ({entry.Length} characters). Error: {e.Message}");
                return false;
            }
            return result;
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
