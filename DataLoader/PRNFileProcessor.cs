﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Data;
using System.Text;

namespace DataLoader
{

    class PRNFileProcessor
    {
        MailSystem mailSystem;
        class PaymentConflictVoucherMailHelper
        {

            internal static string ConvertDT2HTMLString2(DataTable dt)
            {
                string tab = "\t";

                StringBuilder sb = new StringBuilder();
                // , , , ,, ,,, , , ConflictType 
                sb.AppendLine("<html>");
                sb.AppendLine(tab + "<body>");
                sb.AppendLine(tab + tab + "<table>");
                sb.AppendLine(tab + tab + tab + "<thead>");
                sb.AppendLine(tab + tab + tab + tab + "<tr>");
               
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>DupSeq</th>");
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>VoucherDate</th>");
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Reference</th>");
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Invoice</th>");
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Currency</th>");
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Amount</th>");
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Country_Code</th>");
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Supplier</th>");
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>SupplierName</th>");
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>EffDate</th>");
                //sb.AppendLine("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>ConflictType</th>");



                foreach (DataColumn dc in dt.Columns)
                {

                    sb.AppendFormat("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>{0}</th>", dc.ColumnName);
                }

                sb.AppendLine(tab + tab + tab + "</thead>");
                sb.AppendLine(tab + tab + tab + tab + "</tr>");

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
            mailSystem = new MailSystem();
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

                    DataTable dt = null;
                    try
                    {
                        Util.PrintMessage("Starting execution of SP_APDuplicateVouchersDaily ...");
                        int maxVoucherId=dbHandler.CallSPAPDuplicateVouchersDaily();
                        Util.PrintMessage("Completed execution of SP_APDuplicateVouchersDaily ...");
                        dt= dbHandler.GetPaymentVoucherConflictData(maxVoucherId);
                    }
                    catch(Exception ex)
                    {
                        Util.PrintMessage("Error occurred while fetching payment conflich voucher details. " + ex.Message);
                    }

                    try
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Util.PrintMessage("Preparing to send emails");
                            mailSystem.SendEmail("niharika.aranya@gmail.com", "Details of Today's Duplicate Vouchers", PaymentConflictVoucherMailHelper.ConvertDT2HTMLString2(dt));
                            Util.PrintMessage("Emails Send!!!!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Util.PrintMessage("Error occurred while sending email : " + ex.Message);
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
