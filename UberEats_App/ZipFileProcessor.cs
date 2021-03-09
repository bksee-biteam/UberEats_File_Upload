using System;
using System.IO.Compression;
using System.IO;

namespace UberEats_Upload
{
    class ZipFileProcessor

    {
        private string InValidArchivesFolderPath = Properties.Settings.Default.ErrorDir;

        public void unZipFilesIfRequired(string compressedFilesDirPath,   string targetDirPath)
        {
            DirectoryInfo directory = new DirectoryInfo(compressedFilesDirPath);


            foreach (FileInfo fi in directory.GetFiles())
            {

               if( fi.Extension  == ".ZIP")
                {
                    if (!unzipFile(fi.FullName, targetDirPath))
                    {
                        moveInValidZipArchiveToErrorDir(fi);
                    }



                }
                else
                {
                    File.Copy(fi.FullName,Path.Combine(targetDirPath, fi.Name));
                }
               
            }


        }



        public bool unzipFile(string zipFileName, string extractPath)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipFileName, extractPath);
                return true;
            }
             
            catch(Exception e)
            {
                LogFile.SaveErrorLog($"Problem with File ${zipFileName}. File has been ignored. Error Message: " + e.Message);
                return false;
            }
        }
        
        public void zipDirectory(string dirToCompressPath, string zipPath)
        {
            ZipFile.CreateFromDirectory(dirToCompressPath, zipPath);
        }

        public void moveInValidZipArchiveToErrorDir(FileInfo fi)
        {
            string dir = createSubDirForInValidZipFiles();

            File.Move(fi.FullName, Path.Combine(dir, fi.Name));
        }

        private string createSubDirForInValidZipFiles()
        {
            string targetPath = Path.Combine(InValidArchivesFolderPath, "" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"));

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            return targetPath;

        }






    }
}
