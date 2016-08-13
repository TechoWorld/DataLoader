using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLoader
{
    public class DBHandler
    {
        private const string FiscalMonthQuery = "select dbo.FiscalMonth() As FiscalMonth;";
        internal void InsertIntoAseanSalesRegister(IList<string[]> rows)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ERPConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ASEAN_Sales_RegisterInsert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int lineNo = 1;
                    foreach (string[] row in rows)
                    {
                        try
                        {
                            cmd.Parameters.Clear();
                           // cmd.Parameters.Add("@SalesRegisterId", SqlDbType.Int).Value = 0;

                           // cmd.Parameters.Add("@Month", SqlDbType.VarChar).Value = row[0];
                            cmd.Parameters.Add("@Enty", SqlDbType.VarChar).Value = row[0];
                            cmd.Parameters.Add("@Line", SqlDbType.VarChar).Value = row[1];
                            cmd.Parameters.Add("@Site", SqlDbType.VarChar).Value = row[2];
                            cmd.Parameters.Add("@GroupA", SqlDbType.VarChar).Value = row[3];
                            cmd.Parameters.Add("@Region", SqlDbType.VarChar).Value = row[4];
                            cmd.Parameters.Add("@County", SqlDbType.VarChar).Value = row[5];
                            cmd.Parameters.Add("@Customer", SqlDbType.VarChar).Value = row[6];
                            cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = row[7];
                            cmd.Parameters.Add("@SO", SqlDbType.VarChar).Value = row[8];
                            cmd.Parameters.Add("@LN", SqlDbType.VarChar).Value = row[9];
                            cmd.Parameters.Add("@ItemNumber", SqlDbType.VarChar).Value = row[10];
                            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = row[11];
                            cmd.Parameters.Add("@UM", SqlDbType.VarChar).Value = row[12];
                            if (!string.IsNullOrEmpty(row[13]))
                            {
                                decimal SOQtyPerUM = decimal.Parse(row[13]);
                                cmd.Parameters.Add("@SOQtyPerUM", SqlDbType.Decimal).Value = SOQtyPerUM;//""
                            }

                            if (!string.IsNullOrEmpty(row[14]))
                            {
                                decimal SOQtyM2 = decimal.Parse(row[14]);
                                cmd.Parameters.Add("@SOQtyM2", SqlDbType.Decimal).Value = SOQtyM2;
                            }

                            cmd.Parameters.Add("@OrdDate", SqlDbType.VarChar).Value = row[15];
                            cmd.Parameters.Add("@OrdTime", SqlDbType.VarChar).Value = row[16];
                            cmd.Parameters.Add("@RequiredDate", SqlDbType.VarChar).Value = row[17];
                            cmd.Parameters.Add("@PromiseDate", SqlDbType.VarChar).Value = row[18];
                            cmd.Parameters.Add("@DueDate", SqlDbType.VarChar).Value = row[19];
                            cmd.Parameters.Add("@RelDate", SqlDbType.VarChar).Value = row[20];
                            cmd.Parameters.Add("@RelTime", SqlDbType.VarChar).Value = row[21];
                            cmd.Parameters.Add("@ShpDate", SqlDbType.VarChar).Value = row[22];
                            cmd.Parameters.Add("@ShpTime", SqlDbType.VarChar).Value = row[23];
                            cmd.Parameters.Add("@InvoiceCode", SqlDbType.VarChar).Value = row[24];
                            cmd.Parameters.Add("@InvDate", SqlDbType.VarChar).Value = row[25];
                     
                            if (!string.IsNullOrEmpty(row[26]))
                            {
                                decimal INVQtyasPerUM = decimal.Parse(row[26]);
                                cmd.Parameters.Add("@INVQtyasPerUM", SqlDbType.Decimal).Value = INVQtyasPerUM;
                            }

                            if (!string.IsNullOrEmpty(row[27]))
                            {
                                decimal INVQtyinM2 = decimal.Parse(row[27]);
                                cmd.Parameters.Add("@INVQtyinM2", SqlDbType.Decimal).Value = INVQtyinM2;
                            }

                            cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = row[28];
                            if (!string.IsNullOrEmpty(row[29]))
                            {
                                decimal CurrPrice = decimal.Parse(row[29]);
                                cmd.Parameters.Add("@CurrPrice", SqlDbType.Decimal).Value = CurrPrice;
                            }
                            if (!string.IsNullOrEmpty(row[30]))
                            {
                                decimal IDR_Price = decimal.Parse(row[30]);
                                cmd.Parameters.Add("@IDR_Price", SqlDbType.Decimal).Value = IDR_Price;
                            }
                            if (!string.IsNullOrEmpty(row[31]))
                            {
                                decimal BaseAmt = decimal.Parse(row[31]);
                                cmd.Parameters.Add("@BaseAmt", SqlDbType.Decimal).Value = BaseAmt;
                            }
                            if (!string.IsNullOrEmpty(row[32]))
                            {
                                decimal InvoiceAmount = decimal.Parse(row[32]);
                                cmd.Parameters.Add("@InvoiceAmount", SqlDbType.Decimal).Value = InvoiceAmount;
                            }
                            if (!string.IsNullOrEmpty(row[33]))
                            {
                                decimal Cost = decimal.Parse(row[33]);
                                cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = Cost;
                            }
                            if (!string.IsNullOrEmpty(row[34]))
                            {
                                //decimal Mput = decimal.Parse(row[34])/100;
                                cmd.Parameters.Add("@Mput", SqlDbType.Decimal).Value = null;
                            }
                            if (!string.IsNullOrEmpty(row[35]))
                            {
                                decimal AllocatedQtyM2 = decimal.Parse(row[35]);
                                cmd.Parameters.Add("@AllocatedQtyM2", SqlDbType.Decimal).Value = AllocatedQtyM2;
                            }
                            if (!string.IsNullOrEmpty(row[36]))
                            {
                                decimal QtytoInvoiceM2 = decimal.Parse(row[36]);
                                cmd.Parameters.Add("@QtytoInvoiceM2", SqlDbType.Decimal).Value = QtytoInvoiceM2;
                            }
                            if (!string.IsNullOrEmpty(row[37]))
                            {
                                decimal PendingQtyPerUM = decimal.Parse(row[37]);
                                cmd.Parameters.Add("@PendingQtyPerUM", SqlDbType.Decimal).Value = PendingQtyPerUM;
                            }
                            if (!string.IsNullOrEmpty(row[38]))
                            {
                                decimal PendingQtyM2 = decimal.Parse(row[38]);
                                cmd.Parameters.Add("@PendingQtyM2", SqlDbType.Decimal).Value = PendingQtyM2;
                            }
                            if (!string.IsNullOrEmpty(row[39]))
                            {
                                decimal PendingValIDR = decimal.Parse(row[39]);
                                cmd.Parameters.Add("@PendingValIDR", SqlDbType.Decimal).Value = PendingValIDR;
                            }
                            cmd.Parameters.Add("@StatusA", SqlDbType.VarChar).Value = row[40];
                            cmd.Parameters.Add("@ReqDateRsn", SqlDbType.VarChar).Value = row[41];

                            cmd.Parameters.Add("@PromDateRsn", SqlDbType.VarChar).Value = row[42];
                            cmd.Parameters.Add("@Channel", SqlDbType.VarChar).Value = row[43];
                            if (!string.IsNullOrEmpty(row[44]))
                            {
                                decimal Sales = decimal.Parse(row[44]);
                                cmd.Parameters.Add("@Sales", SqlDbType.Decimal).Value = Sales;
                            }
                            cmd.Parameters.Add("@SubAcct", SqlDbType.VarChar).Value = row[45];
                            cmd.Parameters.Add("@ItemType", SqlDbType.VarChar).Value = row[46];
                            cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = row[47];
                            cmd.Parameters.Add("@ShipTo", SqlDbType.VarChar).Value = row[48];
                            cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = row[49];
                            cmd.Parameters.Add("@CustomerType", SqlDbType.VarChar).Value = row[50];
                            cmd.Parameters.Add("@Salespsn", SqlDbType.VarChar).Value = row[51];
                            cmd.Parameters.Add("@SalespsnName", SqlDbType.VarChar).Value = row[52];
                            cmd.Parameters.Add("@PurchaseOrder", SqlDbType.VarChar).Value = row[53];
                            cmd.Parameters.Add("@ListPriceRsn", SqlDbType.VarChar).Value = row[54];
                            cmd.Parameters.Add("@EntryBy", SqlDbType.VarChar).Value = row[55];
                            cmd.Parameters.Add("@Remark", SqlDbType.VarChar).Value = row[56];
                            cmd.Parameters.Add("@VAT", SqlDbType.VarChar).Value = row[57];
                            //cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = row[58];
                            //cmd.Parameters.Add("@Product", SqlDbType.VarChar).Value = row[59];
                            //cmd.Parameters.Add("@Business", SqlDbType.VarChar).Value = row[60];
                            //cmd.Parameters.Add("@Process_Status", SqlDbType.VarChar).Value = row[61];
                            //if (!string.IsNullOrEmpty(row[62]))
                            //{
                            //    decimal PendingCostUSD = decimal.Parse(row[62]);
                            //    cmd.Parameters.Add("@PendingCostUSD", SqlDbType.Decimal).Value = PendingCostUSD;
                            //}
                            //if (!string.IsNullOrEmpty(row[63]))
                            //{
                            //    decimal BaseAmtUSD = decimal.Parse(row[63]);
                            //    cmd.Parameters.Add("@BaseAmtUSD", SqlDbType.Decimal).Value = BaseAmtUSD;
                            //}
                            //if (!string.IsNullOrEmpty(row[64]))
                            //{
                            //    decimal ActualCostUSD = decimal.Parse(row[64]);
                            //    cmd.Parameters.Add("@ActualCostUSD", SqlDbType.Decimal).Value = ActualCostUSD;
                            //}
                            //if (!string.IsNullOrEmpty(row[65]))
                            //{
                            //    decimal PendingAmtUSD = decimal.Parse(row[65]);
                            //    cmd.Parameters.Add("@PendingAmtUSD", SqlDbType.Decimal).Value = PendingAmtUSD;
                            //}
                            //cmd.Parameters.Add("@localCurrencyCode", SqlDbType.VarChar).Value = row[66];
                            //cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = row[67];
                            //cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = row[68];

                            //DateTime apDate;
                            //cmd.Parameters.Add("@CreationDate", SqlDbType.VarChar).Value = DateTime.TryParse(row[14], out apDate) ? apDate.ToString() : row[69];

                            //cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = row[70];
                            
                            //DateTime dueDate;
                            //cmd.Parameters.Add("@UpdationDate", SqlDbType.VarChar).Value = DateTime.TryParse(row[16], out dueDate) ? dueDate.ToString() : row[71];

                            cmd.ExecuteNonQuery();
                            ++lineNo;
                        }
                        catch (Exception ex)
                        {
                            string errMsg = string.Format("Issue with line#{0}. {1}", lineNo, ex.Message);
                            throw new Exception(errMsg, ex);
                        }
                    }

                }
            }

        }
        internal void InsertIntoAPPaymentVouchers(IList<string[]> rows)
        {
            
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ERPConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertAP_PaymentVoucherExcell", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int lineNo = 1;
                    foreach (string[] row in rows)
                    {
                        try
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("@Voucher_Id", SqlDbType.Int).Value = 0;

                            cmd.Parameters.Add("@Enty", SqlDbType.VarChar).Value = row[0];
                            cmd.Parameters.Add("@GLReference", SqlDbType.VarChar).Value = row[1];
                            cmd.Parameters.Add("@Batch", SqlDbType.VarChar).Value = row[2];
                            cmd.Parameters.Add("@Reference", SqlDbType.VarChar).Value = row[3];

                            cmd.Parameters.Add("@Invoice", SqlDbType.VarChar).Value = row[4];
                            cmd.Parameters.Add("@AssignedTo", SqlDbType.VarChar).Value = row[5];

                            cmd.Parameters.Add("@Cnf", SqlDbType.VarChar).Value = row[6];
                            cmd.Parameters.Add("@ConfBy", SqlDbType.VarChar).Value = row[7];
                            cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = row[8];
                            cmd.Parameters.Add("@SortName", SqlDbType.VarChar).Value = row[9];
                            cmd.Parameters.Add("@Cur", SqlDbType.VarChar).Value = row[10];

                            if (!string.IsNullOrEmpty(row[11]))
                            {
                                decimal amt = decimal.Parse(row[11]);
                                cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = amt;//""
                            }

                            if (!string.IsNullOrEmpty(row[12]))
                            {
                                decimal holdAmt = decimal.Parse(row[12]);
                                cmd.Parameters.Add("@HoldAmt", SqlDbType.Decimal).Value = row[12];
                            }

                            if (!string.IsNullOrEmpty(row[13]))
                            {
                                decimal amtApplied = decimal.Parse(row[13]);
                                cmd.Parameters.Add("@AmountApplied", SqlDbType.Decimal).Value = amtApplied;
                            }

                            DateTime apDate;
                            cmd.Parameters.Add("@APDate", SqlDbType.VarChar).Value = DateTime.TryParse(row[14], out apDate) ? apDate.ToString() : row[14];


                            cmd.Parameters.Add("@EffDate", SqlDbType.VarChar).Value = row[15];

                            DateTime dueDate;
                            cmd.Parameters.Add("@Due", SqlDbType.VarChar).Value = DateTime.TryParse(row[16], out dueDate) ? dueDate.ToString() : row[16];

                            cmd.Parameters.Add("@bankref", SqlDbType.VarChar).Value = row[17];
                            cmd.ExecuteNonQuery();
                            ++lineNo;
                        }
                        catch (Exception ex)
                        {
                           string errMsg = string.Format("Issue with line#{0}. {1}",lineNo, ex.Message);
                           throw new Exception(errMsg, ex);
                        }
                    }

                }
            }

        }

        internal int CallSPAPDuplicateVouchersDaily()
        {
            //return 0;
            int maxConflictno = GetMaxPaymentVoucherConflictID();
            CallNonQuerySP("SP_APDuplicateVouchersDaily", 60);
            return maxConflictno;
            
        }

        internal void CallNonQuerySP(string sprocName, int commandTimeOut = 30,IList<SqlParameter> sqlParams=null)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ERPConnectionString"].ConnectionString))
            
            {
                using (SqlCommand cmd = new SqlCommand(sprocName, con))
                {
                    if (sqlParams != null)
                    {
                        foreach (SqlParameter param in sqlParams)
                            cmd.Parameters.Add(param);
                    }
                    cmd.CommandTimeout = commandTimeOut;
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal string GetFiscalMonth()
        {
            return ExecuteQuery(FiscalMonthQuery).Rows[0][0].ToString();
        }

        internal int GetMaxPaymentVoucherConflictID()
        {
            
            string query = "select MAX(VoucherConflictId) from  AP_PaymentVoucherConflicts";
            DataTable dt = ExecuteQuery(query);
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
            //return 0;
        }

        internal DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ERPConnectionString"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        //adp.SelectCommand = cmd;
                        adp.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetPaymentVoucherConflictData(int voucherId)
        {
            string query = string.Format("select DupSeq, VoucherDate, Reference, Invoice,Currency, Amount,Country_Code,Supplier, SupplierName, EffDate, ConflictType  from AP_PaymentVoucherConflicts where VoucherConflictId>{0} ORDER BY ConflictType,DupSeq,Country_Code; ", voucherId);
            return ExecuteQuery(query);
        }

        internal DataTable GetEmailId(string groupName)
        {
            string query = string.Format(ConfigurationManager.AppSettings["RetrieveEmailQuery"], groupName);
            DataTable emailDataTable = ExecuteQuery(query);
            return emailDataTable;
        }

       

    }
}
