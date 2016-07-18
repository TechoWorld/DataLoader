using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Configuration;

namespace DataLoader
{

    class PRNFileProcessor
    {
        DBHandler dbHandler;
        List<string> fixedColLength;
        FileHandler fileHandler;
       
        internal PRNFileProcessor()
        {
            dbHandler = new DBHandler();
            fixedColLength = new List<string>();
            fileHandler = new FileHandler();
        }
         
        internal void LoadFiles(string dirPath)    //Load all files from a directory
        {
            if (!string.IsNullOrWhiteSpace(dirPath))
            {
                string[] filePaths = Directory.GetFiles(dirPath);
                if (filePaths == null | filePaths.Length == 0)
                {
                    Util.PrintMessage("No file to process ...");
                    
                }
                else
                {

                    foreach (string filePath in filePaths)  //Get FileName one by one
                    {
                        try
                        { 
                            Util.PrintMessage("******************************************************************************", false);
                            Util.PrintMessage(string.Format("File Name - {0}", filePath));
                            Util.PrintMessage("Starting file reading...");

                            //Extracting separator code from filename(eg. SG001,KE001.....)
                            string[] parts = filePath.Split('-');
                            string separator = "";
                            for (int i = 0; i < parts.Length; i++)
                            {
                                if (parts[i].Contains("."))
                                {
                                    string[] parts2 = parts[i].Split('.');
                                    separator = parts2[0];
                                    //Console.WriteLine("Separator is: " +separator);
                                }
                            }
   
                           //IList<string> singleFileFullData = FilterData(File.ReadAllLines(filePath), separator);
                            IList<string> singleFileFullData = File.ReadAllLines(filePath);
                            if (singleFileFullData == null || singleFileFullData.Count == 0)
                            {
                                Util.PrintMessage("Empty File");
                            }
                            else
                            {
                                singleFileFullData=FilterData(singleFileFullData.ToArray(), separator);
                                Util.PrintMessage("File reading completed ...");
                                Util.PrintMessage("Starting to save data into DB ...");
                                SaveDataIntoDB(singleFileFullData.ToArray());
                                Util.PrintMessage("Completed data saving into DB ...");
                                Util.PrintMessage("Starting file movement ...");
                                fileHandler.MoveFile(filePath, Path.Combine(ConfigurationManager.AppSettings["DestinationDirectoryPath"],Util.GetDate()));
                                Util.PrintMessage("Ended file movement ...");
                                Util.PrintMessage("******************************************************************************", false);
                            }
                        }
                        catch(Exception exMsg)
                        {
                            Util.PrintMessage(string.Format("While processing file {0} is giving error: {1}", filePath, exMsg.Message));
                            string failDirPath = Path.Combine(ConfigurationManager.AppSettings["DestinationDirectoryPath"], "Fail", Util.GetDate());
                            fileHandler.MoveFile(filePath, failDirPath);
                        }
                    }
                    try
                    {
                        dbHandler.CallSPAPDuplicateVouchersDaily();
                    }
                    catch(Exception ex)
                    {
                        Util.PrintMessage(ex.Message);
                    }
                }
            }

        }

        internal IList<string> FilterData(string[] fullData, string separator) //To get only relevant data i.e. data to be inserted
        {
            fixedColLength.Clear();

            IList<string> extractedData = new List<string>();
            //i represents number of lines in a file which is names as fullData.
            for (int i = 0; i < fullData.Length; ++i)
            {

                if (!string.IsNullOrWhiteSpace(fullData[i]))
                {

                    if (fullData[i].Contains("End of Report"))
                        break;

                    if (i == 60)
                        i = 60;

                    int separatorIdx = fullData[i].IndexOf(separator);
                    if (separatorIdx == 0 || separatorIdx == 1) //To consider both KE001 & FFKE001  
                    {
                        i += 5;
                        if (fixedColLength.Count == 0 && fullData.Length >= (i - 1))
                        {
                            fixedColLength.AddRange(fullData[i - 1].Split(' '));
                           
                        }
                    }
                    if (fullData.Length <= i)
                        break;
                    extractedData.Add(fullData[i]);

                    //index = index + 1;
                }
            }
            return extractedData;
        }


        internal void SaveDataIntoDB(string[] allLines)
        {
           
            List<string[]> rows = new List<string[]>();
            foreach (string line in allLines)
            {
                rows.Add(ParseColValues(line));
            }

            dbHandler.InsertIntoAPPaymentVouchers(rows);
        }


        //To parse the data of the columns 
        internal string[] ParseColValues(string lineData)
        {
            string[] colVals = new string[fixedColLength.Count];
            int startIdx = 0;
            for (int idx = 0; idx < fixedColLength.Count; ++idx)//fixedColLength represents the no of coulmns of table
            {
                if (startIdx + fixedColLength[idx].Length > lineData.Length) //lineData represents one row.
                {
                    colVals[idx] = string.Empty;//colVals is value of column of table(1-18 in case of KE001)
                }
                else
                {
                    
                    colVals[idx] = lineData.Substring(startIdx, fixedColLength[idx].Length).Trim();
                }
                startIdx += fixedColLength[idx].Length + 1;
            }
            return colVals;
        }
    }
}
