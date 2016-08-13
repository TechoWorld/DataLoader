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
    public class MailSystem
    {
        DBHandler dbHandler = new DBHandler();
        MailMessage mailMessage;
        SmtpClient smtpClient;
        DataTable dt;

        public MailSystem()
        {
            dbHandler = new DBHandler();
            mailMessage = new MailMessage();
            smtpClient = new SmtpClient();
            dt = new DataTable();
         
        }

         public void  SendEmail(IList<string> emailTo,IList<string> emailToCC ,string subject, string msgBody)
        {
            
            try
            {

                mailMessage.From = new MailAddress("India-fr.helpdesk@ap.averydennison.com");
                
                AssignEmail(mailMessage.To, emailTo);
                AssignEmail(mailMessage.CC, emailToCC);

                mailMessage.Subject = subject;
                mailMessage.Body = msgBody;
                mailMessage.IsBodyHtml = true;

                smtpClient.Host = ConfigurationManager.AppSettings["EmailServerName"];
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

         private void AssignEmail(MailAddressCollection mailCol, IList<string> emailLst)
         { 
            foreach(var email in emailLst)
            {
                mailCol.Add(email);
            }
         }


        public string ConvertDT2HTMLString(DataTable dt)
        {
            string tab = "\t";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine(tab + "<body>");
            sb.AppendLine(tab + tab + "<table>");
            sb.AppendLine(tab + tab + tab + "<thead>");
            sb.AppendLine(tab + tab + tab + tab + "<tr>");

            foreach (DataColumn dc in dt.Columns)
            {

                sb.AppendFormat("<th style=' color:Black;background-color:White;border: thin solid ;border-color:Black;font-weight:bold'>{0}</th>", dc.ColumnName);
            }

            sb.AppendLine(tab + tab + tab + "</thead>");
            sb.AppendLine(tab + tab + tab + tab + "</tr>");

            // data rows
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append(tab + tab + tab + "<tr>");

                foreach (DataColumn dc in dt.Columns)
                {
                    string cellValue = dr[dc] != null ? dr[dc].ToString() : "";
                    sb.AppendFormat("<td>{0}</td>", cellValue);
                }

                sb.AppendLine("</tr>");
            }

            sb.AppendLine(tab + tab + "</table>");
            sb.AppendLine(tab + "</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
        
    }
}
