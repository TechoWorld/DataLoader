using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Configuration;
//using System.Web.UI.WebControls;

namespace DataLoader
{

    class FixedWidthTextProcessor
    {
        DBHandler dbHandler;
        List<string> fixedColLength;
        FileHandler fileHandler;
       
        internal FixedWidthTextProcessor()
        {
            dbHandler = new DBHandler();
            fixedColLength = new List<string>();
            fileHandler = new FileHandler();
        }


        //To get all the data row by row
        internal IList<string[]> GetRows(string filePath)
        {
            
            IList<string> allLines = File.ReadAllLines(filePath).ToList();//Read the whole file and convert the array of lines into the list
            IList<string[]> rows = new List<string[]>();
            if (allLines == null || allLines.Count == 0)
            {
                Util.PrintMessage("File is empty...");
            }
            else
            {
                allLines = FilterData(allLines, GetSeparatorCode(filePath));
                foreach (string line in allLines)
                {
                    rows.Add(ParseColValues(line));
                }
            }
            return rows;
        }
       internal string GetSeparatorCode(string filePath)
        {
            //Extracting separator code from filename(eg. SG001,KE001.....)
            string[] parts = filePath.Split('-');
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Contains("."))
                {
                    string[] parts2 = parts[i].Split('.');
                    return parts2[0];
                }
            }
            return string.Empty;
        }

 

        private IList<string> FilterData(IList<string> allLines, string separator) //To get only relevant data i.e. data to be inserted
        {
            fixedColLength.Clear();//SO that when next file is processed,then we can clear all the data of previous file that it was holding

            IList<string> extractedData = new List<string>();
            //i represents number of lines in a file which is names as fullData.
            for (int i = 0; i < allLines.Count; ++i)
            {

                if (!string.IsNullOrWhiteSpace(allLines[i]))
                {

                    if (allLines[i].Contains("End of Report"))
                        break;

                    int separatorIdx = allLines[i].IndexOf(separator);
                    if (separatorIdx == 0 || separatorIdx == 1) //To consider both KE001 & FFKE001  
                    {
                        i += 5;
                        // get the fixed column pattern ...
                        if (fixedColLength.Count == 0 && allLines.Count >= (i - 1))
                        {
                            fixedColLength.AddRange(allLines[i - 1].Split(' '));
                        }
                    }
                    if (allLines.Count <= i)
                        break;
                    extractedData.Add(allLines[i]);
                }
            }
            return extractedData;
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
