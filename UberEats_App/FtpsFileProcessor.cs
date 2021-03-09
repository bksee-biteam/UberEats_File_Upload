using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Net;
using WinSCP;

namespace UberEats_Upload
{
    class FtpsFileProcessor
    {

        private string host = Properties.Settings.Default.FTPS_Host;
        private string username = Properties.Settings.Default.FTPS_Login;
        private bool removeFiles  =  Properties.Settings.Default.RemoveFilesFromFTPS_AfterDownload;



        public void DownloadFiles()
        {


            string password = Properties.Settings.Default.FTPS_Password;

            string targetLocalDirectory = Properties.Settings.Default.FilesFromFTPS;



            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = host,
                UserName = username,
                Password = password,
                FtpSecure = FtpSecure.Implicit
            };


            using (Session session = new Session())
            {
                LogFile.SaveAppLog("Connecting to: " + host + " as " + username);


                try
                {
                    session.Open(sessionOptions);
                    LogFile.SaveAppLog("Connected!");


                    RemoteDirectoryInfo directoryInfo = session.ListDirectory("/");


                    LogFile.SaveAppLog(string.Format("Files in number of {0} will be downloaded from FTPS", directoryInfo.Files.Count));

                    LogFile.SaveAppLog("Downloading Files...");
                  
                   
                    session.GetFilesToDirectory("/", targetLocalDirectory, remove: removeFiles);

                    LogFile.SaveAppLog("Files have been downloaded");


                }


                catch (Exception e)
                {
                    LogFile.SaveErrorLog($"Problem with FTPS connection. Error Message: " + e.Message);
                }

            }
        }


    }
}
