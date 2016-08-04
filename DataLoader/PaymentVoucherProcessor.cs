using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace DataLoader
{
    public class PaymentVoucherProcessor : DataProcessor
    {
        public override void ProcessData()
        {
            string sourceDirPath = ConfigurationManager.AppSettings["PaymentSourceDirectoryPath"];
            string destinationDirPath = ConfigurationManager.AppSettings["PaymentDestinationDirectoryPath"];
            LoadFiles(sourceDirPath, destinationDirPath,"Payment");
        }

        protected override void ProcessAfterSaveIntoDB()
        {
            DataTable dt = null;
            try
            {
                Util.PrintMessage("Starting execution of SP_APDuplicateVouchersDaily ...");
                int maxVoucherId = dbHandler.CallSPAPDuplicateVouchersDaily();
                Util.PrintMessage("Completed execution of SP_APDuplicateVouchersDaily ...");
                dt = dbHandler.GetPaymentVoucherConflictData(maxVoucherId);
            }
            catch (Exception ex)
            {
                Util.PrintMessage("Error occurred while fetching payment conflich voucher details. " + ex.Message);
            }

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    IList<string> emailTo =new List<string>();
                    IList<string> emailToCC= new List<string>();
                   // Util.PrintMessage("Preparing to send emails");
                    DataTable emailDt=dbHandler.GetEmailId("APDupPayment");
                    foreach(DataRow row in emailDt.Rows)
                    {
                        if (row["ToCC"].Equals("To"))
                        {
                            emailTo.Add(row["EmailId"].ToString());
                        }
                        else
                        {
                            emailToCC.Add(row["EmailId"].ToString());
                        }
                    }
                   mailSystem.SendEmail(emailTo,emailToCC, "Details of Today's Duplicate Vouchers", mailSystem.ConvertDT2HTMLString(dt));
                   Util.PrintMessage("Emails Send!!!!");
                }
            }
            catch (Exception ex)
            {
                Util.PrintMessage("Error occurred while sending email : " + ex.Message);
            }
        }
        protected override void SaveDataIntoDB(IList<string[]> allLinesColValues)
        {
            dbHandler.InsertIntoAPPaymentVouchers(allLinesColValues);
        }

        protected override IList<string[]> ParseFile(string filePath)
        {
            return fixedWidthFileProcessor.ParseFile(filePath, 5, 5);
        }
    }
}
