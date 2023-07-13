using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;
using Logging;

namespace Release_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ILogger logger = new SerilogClass().logger;

            logger.Information("App just started, logging just began.");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
            

            //TODO: Consider to use async task for compare for better user experience
            //TODO: Implement SeriLog and logging activity for this appliction
            //TODO: Implement settings forms and add user possibility to add new solution to solution compare list
            //TODO: Add path for mandatory files to config file. And implement this in settings form so it can be changed there as well
        }
    }
}
