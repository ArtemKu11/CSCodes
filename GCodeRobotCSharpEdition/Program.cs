using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GCodeRobotCSharpEdition.LoggerPackage;

namespace GCodeRobotCSharpEdition
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var appendableMainLogger = LoggerFactory.GetAppendableLogger("MainLogger");
            var rootLogger = LoggerFactory.GetExistingOrCreateNewLogger("RootLogger");
            var startDateTime = DateTime.Now;
            appendableMainLogger.Log("");
            appendableMainLogger.LogWithTime("Запуск приложения");
            rootLogger.LogWithTime("Main start");
            try
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            }
            catch (Exception e)
            {
                appendableMainLogger.LogException(e);
                rootLogger.LogException(e);
                throw;
            }
            
            var endDateTime = DateTime.Now;
            appendableMainLogger.LogWithTime("Конец приложения");
            rootLogger.LogWithTime("Main stop");
        }
    }
}
