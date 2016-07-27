using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLoader
{
    public class AseanSalesProcessor : DataProcessor
    {
        public override void ProcessData()
        {
            string sourceDirPath = ConfigurationManager.AppSettings["AseanSalesSourceDirectoryPath"];
            string destinationDirPath = ConfigurationManager.AppSettings["AseanSalesDestinationDirectoryPath"];
            LoadFiles(sourceDirPath, destinationDirPath);
        }
        protected override void SaveDataIntoDB(IList<string[]> allLinesColValues)
        {
            dbHandler.InsertIntoAseanSalesRegister(allLinesColValues);
        }

        

        protected override void ProcessAfterSaveIntoDB()
        {
            DataTable dt = null;

            //Needs to pass month in all the procedures--fiscal month dont know.
            try
            {
                Util.PrintMessage("Starting execution of GetFiscalMonth()  ...");
               // dbHandler.CallNonQuerySP("GetFiscalMonth",60);
                Util.PrintMessage("Completed execution of GetFiscalMonth()  ...");

            }
            catch (Exception ex)
            {
                Util.PrintMessage("Error occurred while executing GetFiscalMonth() . " + ex.Message);
            }


            //try
            //{
            //    Util.PrintMessage("Starting execution of ASEAN_UpdateSalesRegister()  ...");
            //    dbHandler.CallNonQuerySP("ASEAN_UpdateSalesRegister", 60);
            //    Util.PrintMessage("Completed execution of ASEAN_UpdateSalesRegister()  ...");

            //}
            //catch (Exception ex)
            //{
            //    Util.PrintMessage("Error occurred while executing ASEAN_UpdateSalesRegister() . " + ex.Message);
            //}


            //try
            //{
            //    Util.PrintMessage("Starting execution of ASEAN_Insert_SalesSummary  ...");
            //    dbHandler.CallNonQuerySP("ASEAN_Insert_SalesSummary", 60);
            //    Util.PrintMessage("Completed execution of ASEAN_Insert_SalesSummary  ...");

            //}
            //catch (Exception ex)
            //{
            //    Util.PrintMessage("Error occurred while executing ASEAN_Insert_SalesSummary. " + ex.Message);
            //}


            //try
            //{
            //    Util.PrintMessage("Starting execution of ASEAN_UpdatePYAOPSalesSummary  ...");
            //    dbHandler.CallNonQuerySP("ASEAN_UpdatePYAOPSalesSummary", 60);
            //    Util.PrintMessage("Completed execution of ASEAN_UpdatePYAOPSalesSummary ...");

            //}
            //catch (Exception ex)
            //{
            //    Util.PrintMessage("Error occurred while executing ASEAN_UpdatePYAOPSalesSummary . " + ex.Message);
            //}


            //try
            //{
            //    Util.PrintMessage("Starting execution of ASEANRebateCalc2  ...");
            //    dbHandler.CallNonQuerySP("ASEANRebateCalc2", 60);
            //    Util.PrintMessage("Completed execution of ASEANRebateCalc2 ...");

            //}
            //catch (Exception ex)
            //{
            //    Util.PrintMessage("Error occurred while executing ASEANRebateCalc2 . " + ex.Message);
            //}



            //try
            //{
            //    Util.PrintMessage("Starting execution of ASEAN_LPM_SalesSummary	  ...");
            //    dbHandler.CallNonQuerySP("ASEAN_LPM_SalesSummary", 60);
            //    Util.PrintMessage("Completed execution of ASEAN_LPM_SalesSummary	 ...");

            //}
            //catch (Exception ex)
            //{
            //    Util.PrintMessage("Error occurred while executing ASEAN_LPM_SalesSummary	. " + ex.Message);
            //}
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    Util.PrintMessage("Preparing to send emails");
                    mailSystem.SendEmail("niharika.aranya@gmail.com", "Details of ASEAN_SalesAccount where Business Type = “Unknown”", mailSystem.ConvertDT2HTMLString(dt));
                    Util.PrintMessage("Emails Send!!!!");
                }
            }
            catch (Exception ex)
            {
                Util.PrintMessage("Error occurred while sending email : " + ex.Message);
            }
        }

        //public override void LogIntoFile(string message)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
