using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Herhaling_Pakjesdienst.DAL
{
    public static class Logger
    {
        // logfile exists in Herhaling_Pakjesdienst.WPF/bin/Debug/netcoreapp3.1/pakjesdienst.log.txt
        private const string _logFileName = "pakjesdienst.log.txt";
        public static void LogReadError(string message, string logFileName = _logFileName)
        {
            string logMsg = new string('=', 69) + '\n' +
                $"Tijdstip: {DateTime.Now}\n" +
                $"Type:     File does not exist\n" +
                $"Fout:     {message}\n";

            // if file exists, appends message
            // if not, creates it
            File.AppendAllText(logFileName, logMsg);
        }

        public static void LogDataError(string message, int line, string logFileName = _logFileName)
        {
            string logMsg = new string('=', 69) + '\n' +
                $"Tijdstip: {DateTime.Now}\n" +
                $"Type:     Bad data\n" +
                $"Lijn:     {line}\n" +
                $"Fout:     {message}\n";

            File.AppendAllText(logFileName, logMsg);
        }
    }
}
