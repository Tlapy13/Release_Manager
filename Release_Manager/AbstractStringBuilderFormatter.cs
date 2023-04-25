using FileScanner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Release_Manager
{
    public abstract class AbstractStringBuilderFormatter : TextWriter
    {
        protected StringBuilder _sb;
        public StringBuilder SB { get { return _sb; } }

        public AbstractStringBuilderFormatter()
        {
            _sb = new StringBuilder();
        }

        public void Write(char value)
        {
            _sb.Append(value);
        }
        public void WriteLine(string value)
        {
            _sb.AppendLine(value);
        }
        public void WriteEmptyLine()
        {
            _sb.AppendLine("");
        }
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
        public StringBuilder Save()
        {
            return _sb;
        }

    }
}
