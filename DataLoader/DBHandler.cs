﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLoader
{
    class DBHandler
    {
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

                            if (!string.IsNullOrWhiteSpace(row[11]))
                            {
                                decimal amt = decimal.Parse(row[11]);
                                cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = amt;//""
                            }

                            if (!string.IsNullOrWhiteSpace(row[12]))
                            {
                                decimal holdAmt = decimal.Parse(row[12]);
                                cmd.Parameters.Add("@HoldAmt", SqlDbType.Decimal).Value = row[12];
                            }

                            if (!string.IsNullOrWhiteSpace(row[13]))
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

        internal void CallSPAPDuplicateVouchersDaily()
        {
            CallNonQuerySP("SP_APDuplicateVouchersDaily");
        }

        internal void CallNonQuerySP(string sprocName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ERPConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sprocName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
