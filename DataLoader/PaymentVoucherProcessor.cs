using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Configuration;

namespace DataLoader
{
    public class PaymentVoucherProcessor : DataProcessor
    {
        public override void ProcessData()
        {
            string sourceDirPath = ConfigurationManager.AppSettings["PaymentSourceDirectoryPath"];
            string destinationDirPath = ConfigurationManager.AppSettings["PaymentDestinationDirectoryPath"];
            LoadFiles(sourceDirPath, destinationDirPath);
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
                    Util.PrintMessage("Preparing to send emails");
                    mailSystem.SendEmail("niharika.aranya@gmail.com", "Details of Today's Duplicate Vouchers", mailSystem.ConvertDT2HTMLString(dt));
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

        //public override void LogIntoFile(string message)
        //{
        //    string dirPath = Path.Combine(ConfigurationManager.AppSettings["PaymentSourceDirectoryPath"], "log");
        //    string logFilePath = Path.Combine(dirPath, string.Format("log-{0}.txt", Util.GetDate()));

        //    if (!Directory.Exists(dirPath))
        //        Directory.CreateDirectory(dirPath);

        //    File.AppendAllText(logFilePath, Environment.NewLine + message);
        //}
    }
}
