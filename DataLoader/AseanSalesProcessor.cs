using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            LoadFiles(sourceDirPath, destinationDirPath, "AseanSales");
        }
        protected override void SaveDataIntoDB(IList<string[]> allLinesColValues)
        {
            dbHandler.InsertIntoAseanSalesRegister(allLinesColValues);
        }

        protected override IList<string[]> ParseFile(string filePath)
        {
            return fixedWidthFileProcessor.ParseFile(filePath, 5, 3);
        }

        protected override void ProcessAfterSaveIntoDB()
        {
            string fiscalMonth=string.Empty;
            //Needs to pass month in all the procedures--fiscal month dont know.
            try
            {
                Util.PrintMessage("Starting execution of GetFiscalMonth()  ...");
                fiscalMonth = dbHandler.GetFiscalMonth();
                Util.PrintMessage("Completed execution of GetFiscalMonth()  ...");
                ExecuteSproc("ASEAN_UpdateSalesRegisterNew", fiscalMonth);
                ExecuteSproc("ASEAN_Insert_SalesSummaryNew", fiscalMonth);
                ExecuteSproc("ASEAN_UpdatePYAOPSalesSummary", fiscalMonth, "AOPPYMonth");
                ExecuteSproc("ASEANRebateCalc2", fiscalMonth, "RebateMonth");
                ExecuteSproc("ASEAN_LPM_SalesSummaryNew", fiscalMonth);
            }
            catch (Exception ex)
            {
                Util.PrintMessage("Error occurred while executing GetFiscalMonth() . " + ex.Message);
            }
        }

        public void ExecuteSproc(string sprocName, string fiscalMonth, string parameterName="month")
        {
            try
            {
                Util.PrintMessage(string.Format( "Starting execution of {0} ...", sprocName));
                SqlParameter fiscalMonthParam = new SqlParameter() 
                {
                    ParameterName = parameterName,
                    DbType= DbType.String,
                    Value = fiscalMonth
                };
                dbHandler.CallNonQuerySP(sprocName, 60, new List<SqlParameter>() { fiscalMonthParam });
                Util.PrintMessage(string.Format("Completed execution of {0}...", sprocName));

            }
            catch (Exception ex)
            {
                Util.PrintMessage(string.Format("Error occurred while executing {0}. {1}", sprocName, ex.Message));
            }
        }

    }
}
