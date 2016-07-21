using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Configuration;

namespace DataLoader
{

    class PRNFileProcessor
    {
        class PaymentConflictVoucherMailHelper
        {

            internal static string ConvertDT2HTMLString2(DataTable dt)
            {
                string tab = "\t";

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("<html>");
                sb.AppendLine(tab + "<body>");
                sb.AppendLine(tab + tab + "<table>");
                sb.AppendLine("<table  style='width: 1000px;font-family: Arial;font-size: 13px;text-align: justify;border: thin solid #56150C;margin-left: 20px;margin-top: 20px; color: #56150C;table-layout:auto; border-collapse: collapse; empty-cells: show;'>");
                sb.AppendLine("<thead><tr line-height:60px; align='center'>");
                sb.AppendLine("<th colspan='1' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Business</th>");
                sb.AppendLine("<th colspan='1' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Country</th>");
                sb.AppendLine("<th colspan='3' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Actual (Invoiced)</th>");
                sb.AppendLine("<th colspan='3' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Pending (Orders)</th>");
                sb.AppendLine("<th colspan='4' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Total (Actual+Pending)</th>");
                sb.AppendLine("<th colspan='4' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Plan  </th>");
                sb.AppendLine("<th colspan='4' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Previous Year  </th>");


                sb.AppendLine("</tr><thead>");


                sb.AppendLine("<thead><tr align='center'>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>LPM</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'></th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Qty</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Sales</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Qty</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Sales</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Qty</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Sales</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP%</th>");

                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Qty</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Sales</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP%</th>");


                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid; border-color:Black;font-weight:bold'>Qty</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid; border-color:Black;font-weight:bold'>Sales</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid; border-color:Black;font-weight:bold'>TP</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid; border-color:Black;font-weight:bold'>TP%</th>");

                sb.AppendLine("</tr ><thead>");

                //Third Header Line

                sb.AppendLine("<thead><tr align='center'>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'></th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'></th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>M2'000s</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000s</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000s</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>M2'000s</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>M2'000s</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>%</th>");

                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>M2'000s</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>%</th>");

                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>M2'000s</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>");
                sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>%</th>");


                sb.AppendLine("</tr ><thead>");
                // headers.
                sb.Append(tab + tab + tab + "<tr>");

                foreach (DataColumn dc in dt.Columns)
                {
                    sb.AppendFormat("<td>{0}</td>", dc.ColumnName);
                }

                sb.AppendLine("</tr>");

                // data rows
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append(tab + tab + tab + "<tr>");

                    foreach (DataColumn dc in dt.Columns)
                    {
                        string cellValue = dr[dc] != null ? dr[dc].ToString() : "";
                        sb.AppendFormat("<td>{0}</td>", cellValue);
                    }

                    sb.AppendLine("</tr>");
                }

                sb.AppendLine(tab + tab + "</table>");
                sb.AppendLine(tab + "</body>");
                sb.AppendLine("</html>");

                return sb.ToString();
            }

        }



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
            if (!string.IsNullOrEmpty(dirPath))
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
                            string failDirPath = Util.CombinePath(ConfigurationManager.AppSettings["DestinationDirectoryPath"], "Fail", Util.GetDate());
                            fileHandler.MoveFile(filePath, failDirPath);
                        }
                    }
                    try
                    {
                        Util.PrintMessage("Starting execution of SP_APDuplicateVouchersDaily ...");
                        int maxVoucherId=dbHandler.CallSPAPDuplicateVouchersDaily();
                        Util.PrintMessage("Completed execution of SP_APDuplicateVouchersDaily ...");

                    }
                    catch(Exception ex)
                    {
                        Util.PrintMessage("Error occurred while executing - SP_APDuplicateVouchersDaily : " + ex.Message);
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

                if (!string.IsNullOrEmpty(fullData[i]))
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
