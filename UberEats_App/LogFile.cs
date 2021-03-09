using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Mail;


namespace UberEats_Upload
           
{
    public static class LogFile
    {
        private static string time_log = DateTime.Now.ToString("yyyyMMdd_HH_mm_ss");
        private static string ErrorLog = Properties.Settings.Default.log_path + time_log + "_error.log";
        private static string AppLog = Properties.Settings.Default.log_path + time_log + "_aplication.log";
        private static string WarningLog = Properties.Settings.Default.log_path + time_log + "_warning.log";
        public static List<string> EmailBody = new List<string>();
        public static string sendEmailFlag = Properties.Settings.Default.sendEmail.ToUpper();
        public static bool Failed = false;


        public static string getTime()
        {
            return time_log;
        }

        public static void SaveAppLog(String log_message)
        {
            Console.WriteLine(log_message);
            File.AppendAllText(AppLog, DateTime.Now + " | " + log_message + Environment.NewLine);
            EmailBody.Add(log_message);
        }

        public static void SaveErrorLog(String log_message)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(log_message);
            File.AppendAllText(ErrorLog, DateTime.Now + " | " + log_message + Environment.NewLine);

            Console.ForegroundColor = ConsoleColor.White;

            Failed = true;
            EmailBody.Add(log_message);
        }

        public static void SaveLogInsert(String tableName, int numberOfInsertedRows)
        {
            File.AppendAllText(AppLog, DateTime.Now + "," + "To table: " + tableName + ", "  + numberOfInsertedRows +" row[s] have been inserted."+ Environment.NewLine);        
        }

        public static void SaveLogDelete(int numberOfDeletedRows, string tableName)
        {
            File.AppendAllText(AppLog, DateTime.Now + "," + "From table: " + tableName + ", " + numberOfDeletedRows.ToString() + " row[s] have been deleted." + Environment.NewLine);
        }

        public static void SaveLogWarning(String log_message)
        {
            Console.WriteLine(log_message);
            File.AppendAllText(WarningLog, DateTime.Now + " | " + log_message + Environment.NewLine);
        }

        public static void SaveSummary (int numberOfProcessedFiles)
        {

            string msg = $"All of {numberOfProcessedFiles} files have been processed successfully.";
 
            SaveAppLog(msg);
          
            EmailBody.Add(msg);
        }


        public static void SendEmail()
        {
            if (sendEmailFlag != "Y" )
                return;

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(Properties.Settings.Default.email_to));
            msg.From = new MailAddress(Properties.Settings.Default.email_from);

            string subject;
            if (!Failed)
            {
                subject = "[App_succeeded]UberEats_File_Upload";
            }
            else
            {
                subject = "[App_failed]UberEats_File_Upload";
            }
            msg.Subject = subject; 

            try
            {
                msg.Body = "UberEats_File_Upload steps:";
                msg.Body += "<br />";
                foreach (var item in EmailBody)
                {
                    msg.Body += item;
                    msg.Body += "<br />";
                }
            }
            catch (Exception ex)
            {
                LogFile.SaveErrorLog(ex.Message);
            }
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.email_username, Properties.Settings.Default.email_password);
            client.Port = 587; // You can use Port 25 if 587 is blocked
            client.Host = "smtp.office365.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            try
            {
                client.Send(msg);

            }
            catch (Exception ex)
            {
                SaveErrorLog($"Failed to send email. Error: {ex.Message}");  
            }
        }
    }
}
 