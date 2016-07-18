using System.Configuration;

namespace DataLoader
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Program program = new Program();

            Util.PrintMessage("******************************************************************************", false);
            Util.PrintMessage("Starting file loader program ...");
            string sourceDirPath = ConfigurationManager.AppSettings["SourceDirectoryPath"];
           
            PRNFileProcessor prnFileProcessor = new PRNFileProcessor();
          
            prnFileProcessor.LoadFiles(sourceDirPath);
            Util.PrintMessage("Stopping file loader program....");
            Util.PrintMessage("******************************************************************************", false);
           // Console.ReadKey();

        }

    }
}
