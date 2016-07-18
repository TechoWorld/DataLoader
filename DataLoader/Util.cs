using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    class Util
    {
        public static void PrintMessage(string message, bool addTimeStamp=true)
        {
            string finalMsg = addTimeStamp ? string.Format("{0} : {1}", Util.GetDateWithTimestamp(), message) : message;
            System.Console.WriteLine(finalMsg);
            LogIntoFile(finalMsg);
        }

        private static void LogIntoFile(string message)
        {
            string dirPath = Path.Combine(ConfigurationManager.AppSettings["SourceDirectoryPath"], "log");
            string logFilePath = Path.Combine(dirPath, string.Format("log-{0}.txt", GetDate()));
            
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            File.AppendAllText(logFilePath, Environment.NewLine + message);
            
        }

        internal static string GetDate()
        {
            return DateTime.Now.ToString("dd-MMM-yyyy");
        }

        internal static string GetDateWithTimestamp()
        {
            return DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss");
        }
    }
}
