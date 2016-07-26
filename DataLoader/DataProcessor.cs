using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DataLoader
{
    public abstract class DataProcessor : IDataProcessor
    {
        protected internal FileHandler fileHandler;
        protected DBHandler dbHandler;
        protected MailSystem mailSystem;
        protected FixedWidthFileProcessor fixedWidthFileProcessor;
        public DataProcessor()
        {
            fileHandler = new FileHandler();
            dbHandler = new DBHandler();
            mailSystem = new MailSystem();
            fixedWidthFileProcessor = new FixedWidthFileProcessor();
        }

        #region Start abstract method
        protected abstract void SaveDataIntoDB(IList<string[]> allLinesColValues);
        protected abstract void ProcessAfterSaveIntoDB();
        public abstract void ProcessData();
        //public abstract void LogIntoFile(string message);

        #endregion End abstract method

        internal void LoadFiles(string sourceDirPath, string destinationDirPath)    //Load all files from a directory
        {
            if (!string.IsNullOrEmpty(sourceDirPath))
            {
                string[] filePaths = Directory.GetFiles(sourceDirPath);
                if (filePaths == null | filePaths.Length == 0)
                {
                    Util.PrintMessage("No file to process ...");

                }
                else
                {

                    foreach (string filePath in filePaths)  //Get FileName one by one
                    {
                        string finalDestinationDirPath = Util.CombinePath(destinationDirPath, Util.GetDate());
                        try
                        {
                            Util.PrintMessage("******************************************************************************", false);
                            Util.PrintMessage(string.Format("File Name - {0}", filePath));
                            Util.PrintMessage("Starting file reading...");

                            SaveDataIntoDB(fixedWidthFileProcessor.ParseFile(filePath));
                        }
                        catch (Exception exMsg)
                        {
                            Util.PrintMessage(string.Format("While processing file {0} is giving error: {1}", filePath, exMsg.Message));
                            finalDestinationDirPath = Util.CombinePath(destinationDirPath, "Fail", Util.GetDate());
                        }
                        finally
                        {
                            fileHandler.MoveFile(filePath, finalDestinationDirPath);
                        }
                    }

                    ProcessAfterSaveIntoDB();
                }
            }

        }

    
    }
}
