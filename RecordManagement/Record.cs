using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileScanner
{
  [Serializable]
  public class Record
  {
    public string Hash;
    public string Filename;
    public string shortFileName;
    public int Version;
    public DateTime CreateDate;
    public DateTime ModificationDate;

    [XmlIgnore]
    public bool Changed = false;

    public override bool Equals(object obj)
    {
      if(obj is Record r)
      {
        return r.Filename == Filename;
      }
      return false;
    }

    public override int GetHashCode()
    {
      int hashCode = -2079975871;
      hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Filename);
      return hashCode;
    }

        public override string ToString()
        {
            return $"File: {shortFileName}, Date Created: {CreateDate}, Date Modified: {ModificationDate}, Hash: {Hash}";
        }
    }
}
