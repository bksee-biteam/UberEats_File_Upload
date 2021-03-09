using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;

namespace UberEats_Upload
{
    public class MyGlobals
    {
        public static string DatabaseConnectionStr = "Data Source=" + Properties.Settings.Default.dba_hostname + ";Initial Catalog=" + Properties.Settings.Default.dba_name + ";Persist Security Info=True;User ID=" + Properties.Settings.Default.dba_username + ";Password=" + Properties.Settings.Default.dba_password;

    }

    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo customCulture = new CultureInfo("us-EN");
            Thread.CurrentThread.CurrentCulture = customCulture;

            const string appName = "UberEats_Files_Upload";
            bool createdNew;
            Mutex mutex = new Mutex(true, appName, out createdNew);
            string userAnswer = "";

            if (!createdNew)
            {
                Console.WriteLine(appName + " is already running! Exit the application.");
                Thread.Sleep(1000 * 20);
                return;
            }
            if (args.Length == 0)
            {
                Console.WriteLine("***************  UberEats FILE UPLOAD **********************");
               
                Console.WriteLine("Do you want to upload data? [Y/N]");
                userAnswer = Console.ReadLine();
            }
            else
            {
                userAnswer = args[0];
            }

            if (userAnswer.ToUpper() == "Y") 
            {


                try
                {

                   new FtpsFileProcessor().DownloadFiles();
                   new FileProcessor().StartProcessingFiles();

                }
                catch (Exception e)
                {
                    LogFile.SaveErrorLog("Error loading ...........csv, detalis:");
                    LogFile.SaveErrorLog(e.Message);
                    Console.ReadKey();
                    LogFile.Failed = true;
                }


                            
                LogFile.SaveAppLog("EXIT");
                Console.WriteLine(DateTime.Now);

                LogFile.SendEmail();
                Thread.Sleep(1000 * 10);

            }
        }       
    }
}
