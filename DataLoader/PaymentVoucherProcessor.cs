using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    class PaymentVoucherProcessor:IDataProcessor
    {
        FixedWidthTextProcessor fixedWidthTextPrc;
        DBHandler dbHandler;
        public PaymentVoucherProcessor()
        {
            fixedWidthTextPrc = new FixedWidthTextProcessor();
            dbHandler = new DBHandler();
        }
        public void LoadData(string sourceDirPath, string DestDirPath)
        {
            throw new NotImplementedException();
        }
        internal void SaveDataIntoDB(IList<string[]> rows)
        {
            dbHandler.InsertIntoAPPaymentVouchers(rows);
        }

        internal void LoadFiles(string dirPath)    //Load all files from a directory
        {
            if (!string.IsNullOrWhiteSpace(dirPath))
            {
                string[] filePaths = Directory.GetFiles(dirPath);
                if (filePaths == null | filePaths.Length == 0)
                {
                    Util.PrintMessage("Empty source directory ...");
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
                            
                            IList<string[]> rows = fixedWidthTextPrc.GetRows(filePath);
                            
                            Util.PrintMessage("File reading completed ...");
                            Util.PrintMessage("Starting to save data into DB ...");
                            
                            SaveDataIntoDB(rows);

                            Util.PrintMessage("Completed data saving into DB ...");
                            Util.PrintMessage("Starting file movement ...");
                            FileHandler.MoveFile(filePath, Path.Combine(ConfigurationManager.AppSettings["DestinationDirectoryPath"], Util.GetDate()));
                            Util.PrintMessage("Ended file movement ...");
                            Util.PrintMessage("******************************************************************************", false);
                        }
                        catch (Exception exMsg)
                        {
                            Util.PrintMessage(string.Format("While processing file {0} is giving error: {1}", filePath, exMsg.Message));
                            string failDirPath = Path.Combine(ConfigurationManager.AppSettings["DestinationDirectoryPath"], "Fail", Util.GetDate());
                            FileHandler.MoveFile(filePath, failDirPath);
                        }
                    }

                    try
                    {
                        Util.PrintMessage("Starting execution of SP_APDuplicateVouchersDaily ...");
                        dbHandler.CallSPAPDuplicateVouchersDaily();
                    }
                    catch (Exception ex)
                    {
                        Util.PrintMessage("Error occurred in SP_APDuplicateVouchersDaily : " + ex.Message);
                    }
                }
            }

        }
    }
}
