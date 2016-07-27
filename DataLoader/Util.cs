using System;
using System.Configuration;
using System.IO;

namespace DataLoader
{
    class Util
    {
        public static EnvironmentInfo EnvironmentInfo{get;set;}
        public static string CombinePath(params string[] paths)
        {
            string finalPath = "";
            if (paths != null)
            {
                 for (int i = 0; i < paths.Length; ++i)
                {
                    finalPath = i == 0 ? paths[i] : Path.Combine(finalPath, paths[i]);
                }
            }
            return finalPath;
        }
     public static void PrintMessage(string message, bool addTimeStamp=true,string srcDirPath=null)
        {
            string finalMsg = addTimeStamp ? string.Format("{0} : {1}", Util.GetDateWithTimestamp(), message) : message;
            System.Console.WriteLine(finalMsg);
            LogIntoFile(finalMsg);
        }

     private static void LogIntoFile(string message)
     {
         string dirPath = Path.Combine(EnvironmentInfo.SourceDirPath, "log");
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
