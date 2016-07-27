﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace DataLoader
{
    public class FixedWidthFileProcessor
    {
        List<string> fixedColLength;
        FileHandler fileHandler;

        internal FixedWidthFileProcessor()
        {
            fileHandler = new FileHandler();
            fixedColLength = new List<string>();
        }
        internal IList<string[]> ParseFile(string filePath)    //Load all files from a directory
        {
            IList<string[]> allLinesColValues = new List<string[]>();//Filtered and ParsedColVlaues data that needs to be entered into db--each line
            try
            {
                
                //Extracting separator code from filename(eg. SG001,KE001.....)
                string[] parts = filePath.Split('.');
                string separator = "";
                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i].Contains("-"))
                    {
                        string[] parts2 = parts[i].Split('-');
                        separator = parts2[1];
                        break;
                        //Console.WriteLine("Separator is: " +separator);
                    }

                    else
                    {
                        int j = 0;
                        string[] parts3 = parts[i].Split('\\');
                        for ( j = 0; j < parts3.Length-1; j++) ;
                        separator = parts3[j];
                        break;
                    }
                }

                //for (int i = 0; i < parts.Length; i++)
                //{
                //    if (parts[i].Contains("."))
                //    {
                //        string[] parts2 = parts[i].Split('.');
                //        separator = parts2[0];
                //        //Console.WriteLine("Separator is: " +separator);
                //    }
                //}

                IList<string> singleFileFullData = File.ReadAllLines(filePath);//singleFileFullData is
                if (singleFileFullData == null || singleFileFullData.Count == 0)
                {
                    Util.PrintMessage("Empty File");
                }
                else
                {
                  //filterFileLines is name of the whole data be entered into the DB that is separated bt fixedColWidth
                    //-----,-----,----,------  --> Example of the filterFIleLines
                    IList<string> filterFileLines = FilterData(singleFileFullData.ToArray(), separator);
                    Util.PrintMessage("File reading completed ...");
                    if (filterFileLines.Count > 0)
                    {
                        foreach (var line in filterFileLines)
                        {
                            
                            allLinesColValues.Add(ParseColValues(line));
                        }
                    }
                }
            }
            catch (Exception exMsg)
            {
                Util.PrintMessage(string.Format("While processing file {0} is giving error: {1}", filePath, exMsg.Message));
                string failDirPath = Util.CombinePath(ConfigurationManager.AppSettings["DestinationDirectoryPath"], "Fail", Util.GetDate());
                fileHandler.MoveFile(filePath, failDirPath);
            }
            return allLinesColValues;
        }


        private IList<string> FilterData(string[] fullData, string separator) //To get only relevant data i.e. data to be inserted
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

                    int separatorIdx = fullData[i].IndexOf(separator);
                    if (separatorIdx == 0 || separatorIdx == 1) //To consider both KE001 & FFKE001  
                    {
                        i += 5;
                        if (fixedColLength.Count == 0 && fullData.Length >= (i - 1))
                        {
                            //fixedColLength.AddRange(fullData[i - 1].Split(' '));
                            fixedColLength.AddRange(fullData[i - 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

                        }
                    }
                    if (fullData.Length <= i)
                        break;

                    if (!string.IsNullOrEmpty(fullData[i].Trim()))
                        extractedData.Add(fullData[i]);
                }
            }
            return extractedData;
        }

        private string[] ParseColValues(string lineData)
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
