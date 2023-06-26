using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Logging
{
    public class SerilogClass
    {
        public readonly ILogger logger = new LoggerConfiguration()
              .ReadFrom.AppSettings()
              .CreateLogger();

    }
}
