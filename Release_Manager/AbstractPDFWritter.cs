using iText.Kernel.Pdf;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Layout;
using iText.Layout.Element;
using ServiceStack.Text.Common;

namespace Release_Manager
{
    public abstract class AbstractPDFWritter<T> : IDisposable
    {
        public Document _document;
        public PdfWriter _writer;
        public PdfDocument _pdf;
        public string _header;
        protected AbstractStringBuilderFormatter _sbw;
        protected readonly T _data;

        public AbstractPDFWritter(T data)
        {
            _data = data;
            InitPDFWritter();

            using (_document = new Document(_pdf))
            {
                WritteData();
                _document.Close();
            }
        }

        public abstract void InitPDFWritter();

        public abstract void WritteData();


        public void WriteHeader(string text, float? fontSize = null, TextAlignment? alignment = null)
        {
            iText.Layout.Element.Paragraph p = new iText.Layout.Element.Paragraph(text);
            if (fontSize.HasValue)
            {
                p.SetFontSize(fontSize.Value);
            }
            if (alignment.HasValue)
            {
                p.SetTextAlignment(alignment.Value);
            }
            _document.Add(p);
        }

        public void WriteSB(StringBuilder sb)
        {
            _document.Add(new iText.Layout.Element.Paragraph(sb.ToString()));
        }
        public void WriteLine(string text)
        {
            iText.Layout.Element.Paragraph p = new iText.Layout.Element.Paragraph(text);
            _document.Add(p);
        }
        public void WriteEmptyLine()
        {
            _document.Add(new iText.Layout.Element.Paragraph(" "));
        }

        public void Dispose()
        {
            _document.Close();
            _writer.Close();
        }
    }



}
