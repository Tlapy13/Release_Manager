using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Nodes;
using Logging;
using Serilog;
using ServiceStack.Text.Common;

namespace Release_Manager
{
    public class PdfWriterCls : AbstractPDFWritter<StringBuilderData>
    {
        private readonly ILogger _logger = new SerilogClass().logger;

        public PdfWriterCls(StringBuilderData data) : base(data)
        {

        }
        public override void InitPDFWritter()
        {
            try
            {
                _writer = new PdfWriter(_data.FileName);
                _pdf = new PdfDocument(_writer);
                _header = _data.Header;
                _sbw = new StringBuilderFormatter(_data);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error encountered while creating PDF: {ex}");
            }


        }
        public override void WritteData()
        {
            WriteHeader(_header, 20, TextAlignment.CENTER);
            WriteEmptyLine();
            WriteEmptyLine();
            WriteSB(_sbw.SB);
            _document.Close();
        }


    }

 }
