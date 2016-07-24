using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.IO;

namespace DataLoader
{
    class MailSystem
    {
        MailMessage mailMessage;
        SmtpClient smtpClient;
        DataTable dt;

        public MailSystem()
        {
            mailMessage = new MailMessage();
            smtpClient = new SmtpClient();
            dt = new DataTable();
        }

        //internal void FetchDataTable()
        //{
        //    string MsgBody;
        //     string name = " Manish";
        //    string mailto = "";
        //    string mailcc = "";
        //    string subject = "ASEAN Daily Sales Report - MTD ";
        //    string traderepeat = "N";
        //    string netsalesrepeat = "N";
        //     MsgBody = "";
        //    MsgBody = MsgBody + "Dear Sir," + "<BR><BR>";
        //   // MsgBody = MsgBody + "Please find below LPM Daliy Sales Summary Report for " + (ddlformonth1.SelectedItem).Text + "<BR><BR>"; 
        //    //DateTime.Today.ToString("dd-mm-yyyy") +"<BR><BR>";



        //    //MsgBody = MsgBody + "<table  style='width: 1000px;font-family: Vrinda;font-size: 13px;text-align: justify;border: thin solid #56150C;margin-left: 20px;margin-top: 20px; color: #56150C;cellspacinf:3;cellpadding:3;table-layout:auto; border-collapse: collapse; empty-cells: show;'>";
        //    MsgBody = MsgBody + "<table  style='width: 1000px;font-family: Arial;font-size: 13px;text-align: justify;border: thin solid #56150C;margin-left: 20px;margin-top: 20px; color: #56150C;table-layout:auto; border-collapse: collapse; empty-cells: show;'>";
        //    MsgBody = MsgBody + ("<thead><tr line-height:60px; align='center'>");
        //    MsgBody = MsgBody + "<th colspan='1' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Business</th>");
        //    MsgBody = MsgBody + "<th colspan='1' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Country</th>";
        //    MsgBody = MsgBody + "<th colspan='3' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Actual (Invoiced)</th>";
        //    MsgBody = MsgBody + "<th colspan='3' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Pending (Orders)</th>";
        //    MsgBody = MsgBody + "<th colspan='4' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Total (Actual+Pending)</th>";
        //    MsgBody = MsgBody + "<th colspan='4' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Plan  </th>";
        //    MsgBody = MsgBody + "<th colspan='4' style=' color:Black;background-color:#FFFF00;border-color:Black;border: thin solid ;font-weight:bold'>Previous Year  </th>";


        //    MsgBody = MsgBody + "</tr><thead>";


        //    MsgBody = MsgBody + ("<thead><tr align='center'>");
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>LPM</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'></th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Qty</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Sales</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Qty</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Sales</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Qty</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Sales</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP%</th>";

        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Qty</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>Sales</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>TP%</th>";


        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid; border-color:Black;font-weight:bold'>Qty</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid; border-color:Black;font-weight:bold'>Sales</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid; border-color:Black;font-weight:bold'>TP</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid; border-color:Black;font-weight:bold'>TP%</th>"; 

        //    MsgBody = MsgBody + "</tr ><thead>";
            
        //    //Third Header Line

        //    MsgBody = MsgBody + ("<thead><tr align='center'>");
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'></th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'></th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>M2'000s</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000s</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000s</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>M2'000s</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>M2'000s</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>%</th>";

        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>M2'000s</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>%</th>";

        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>M2'000s</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>US$'000</th>";
        //    MsgBody = MsgBody + "<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>%</th>";

            
        //    MsgBody = MsgBody + "</tr ><thead>";
        //    for (int j = 0; j < dts.Rows.Count; j++)
        //    {
        //        try
        //        {

        //            string Bussiness = dts.Rows[j]["Business"].ToString().Trim();
        //            string Country = dts.Rows[j]["Country"].ToString().Trim();

        //            string ActualQty = dts.Rows[j]["MTD_Actual_Qty"].ToString().Trim();
        //            string ActualSales = dts.Rows[j]["MTD_Actual_Sales"].ToString().Trim();
        //            string ActualTP = dts.Rows[j]["MTD_Actual_TP"].ToString().Trim();


        //            string PendingQty = dts.Rows[j]["MTD_Pending_Qty"].ToString().Trim();
        //            string PendingSales = dts.Rows[j]["MTD_Pending_Sales"].ToString().Trim();
        //            string PendingTP = dts.Rows[j]["MTD_Pending_TP"].ToString().Trim();

        //            string TotalQty = dts.Rows[j]["MTD_Total_Qty"].ToString().Trim(); 
        //            string TotalSales = dts.Rows[j]["MTD_Total_Sales"].ToString().Trim();
        //            string TotalTP = dts.Rows[j]["MTD_Total_TP"].ToString().Trim();
        //            string TotalTPercentage = dts.Rows[j]["MTD_Total_TPPer"].ToString().Trim();

        //            string AOPQty = dts.Rows[j]["AOP_Qty"].ToString().Trim();
        //            string AOPSales = dts.Rows[j]["AOP_SalesUSD"].ToString().Trim();
        //            string AOPTP = dts.Rows[j]["AOP_TPUSD"].ToString().Trim();
        //            string AOPTPercentage = dts.Rows[j]["AOP_TPUSDPer"].ToString().Trim();


        //            string PYQty = dts.Rows[j]["PY_Qty"].ToString().Trim();
        //            string PYSales = dts.Rows[j]["PY_SalesUSD"].ToString().Trim();
        //            string PYTP = dts.Rows[j]["PY_TPUSD"].ToString().Trim();
        //            string PYTPercentage = dts.Rows[j]["PY_TPUSDPer"].ToString().Trim();


        //            if (Bussiness == "Trade" && traderepeat == "Y")
        //            {
        //                Bussiness = "";
        //            }

        //            if (Bussiness == "Net Sales" && netsalesrepeat == "Y")
        //            {
        //                Bussiness = "";
        //            }

        //            if (Bussiness == "Trade" && traderepeat == "N")
        //            {
        //                traderepeat = "Y";
        //            }

        //            if (Bussiness == "Net Sales" && netsalesrepeat == "N")
        //            {
        //                netsalesrepeat = "Y";
        //            }


        //            if (Bussiness == "LPM Trade Total" || Bussiness == "Intercompany Sales" || Bussiness == "R&A and Rebate" || Bussiness == "IntraAsean Sales" || Bussiness == "GRPH"  )
        //            {
        //                MsgBody = MsgBody + "<tr style=' background-color:#CFDBC5;line-height: 10px'>";
        //            }

        //            else if (Bussiness == "LPM Total" || Bussiness == "MAT Total" || Bussiness == "Net Sales Total")
        //            {
        //                MsgBody = MsgBody + "<tr style='background-color:#FFFF00;line-height: 10px'>";
        //            }
        //            else
        //            {

        //                MsgBody = MsgBody + "<tr style='line-height: 10px'>";
        //            }
        //            //Increase Width of Business Column Width?
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;'>" + Bussiness + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial; text-align:center'>" + Country + "</td>";

        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;text-align:right'>" + ActualQty + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;text-align:right'>" + ActualSales + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;text-align:right'>" + ActualTP + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;text-align:right'>" + PendingQty + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;text-align:right'>" + PendingSales + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;text-align:right'>" + PendingTP + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;text-align:right'>" + TotalQty + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;text-align:right'>" + TotalSales + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;text-align:right'>" + TotalTP + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000;font-family:Arial;text-align:right'>" + TotalTPercentage + "% " + "</td>";

        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000; font-family:Arial;text-align:right'>" + AOPQty + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000; font-family:Arial;text-align:right'>" + AOPSales + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000; font-family:Arial;text-align:right'>" + AOPTP + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000; font-family:Arial;text-align:right'>" + AOPTPercentage + "% " + "</td>";


        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000; font-family:Arial;text-align:right'>" + PYQty + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000; font-family:Arial;text-align:right'>" + PYSales + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000; font-family:Arial;text-align:right'>" + PYTP + " " + "</td>";
        //            MsgBody = MsgBody + "<td style=' border: thin solid #000000; font-family:Arial;text-align:right'>" + PYTPercentage + "% " + "</td>";

        //            MsgBody = MsgBody + "</tr>";
        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //        finally
        //        {

        //        }
        //    }
        //    if (dts.Rows.Count > 0)
        //    {
        //        MsgBody = MsgBody + "</table>";

        //        mailto = mailto.Substring(0, mailto.Length - 1);
        //        mailcc = mailcc.Substring(0, mailcc.Length - 1);
        //        mailto = "manishkumargupt@gmail.com";
        //        mailcc = "manish@gyansetu.in";


        //        SendEmail("niharika.april@gmail.com", "Testing Dataloader Mail", "Just for testing purpose");
            
        //    }
        //}

    



       
        public void  SendEmail(string emailTo, string subject, string msgBody)
        {
            
            try
            {
                
                mailMessage.From = new MailAddress("info@aranyaproject.com");
                mailMessage.To.Add(emailTo);
                mailMessage.Subject = subject;

                mailMessage.Body = msgBody;
                //Console.WriteLine(msgBody);
                //string path = @"C:\Users\SAHIL\Desktop\Txt Files\1.html";
                //using (StreamWriter sw = File.CreateText(path))
                //{
                //    sw.WriteLine(msgBody);

                //}	
                mailMessage.IsBodyHtml = true;

                smtpClient.Host = "relay-hosting.secureserver.net";
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                mailMessage = null;
                mailMessage = null;
            }
        }


        
    }
}
