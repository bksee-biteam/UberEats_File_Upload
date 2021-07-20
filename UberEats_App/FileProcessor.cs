using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;


namespace UberEats_Upload
{
   


    class FileProcessor:ZipFileProcessor
    {
     private string workingFolderPath = Properties.Settings.Default.WorkingDir;
     private string archiveFolderPath = Properties.Settings.Default.ArchiveDir;
     private string originalFilesFromSFTPolderPath = Properties.Settings.Default.FilesFromFTPS;

     private int fileNum = 1;
     private int totalNumberOfFiles = 0;

        public void StartProcessingFiles()
        {
            prepareDataBase();

            DirectoryInfo workingDir = new DirectoryInfo(workingFolderPath);


            unZipFilesIfRequired(originalFilesFromSFTPolderPath, workingFolderPath);

            

            totalNumberOfFiles = workingDir.GetFiles().Count();

            LogFile.SaveAppLog(string.Format($"Number of files: {totalNumberOfFiles}"));




            foreach (FileInfo fi in workingDir.GetFiles())
            {
                try
                {
                    UberEatsSalesFile uberEatsFile = new UberEatsSalesFile(fi);

                    LogFile.SaveAppLog(string.Format(Environment.NewLine + $"**************************Processing file {fi.Name}. File number {fileNum}/{totalNumberOfFiles}"));


                    fileNum += 1;


                    if (!uberEatsFile.fileIgnored)
                    { 
                        uberEatsFile.ProcessFile(); 
                    }
                    else
                    {
                        processIgnoredFile(fi.FullName);
                    }



                }

                catch (Exception e)
                {
                    LogFile.SaveErrorLog($"Application failed processing file {fi.FullName}  Reason for failure : {e.Message}");
                    processIgnoredFile(fi.FullName);
                
                }
 
            }
            postProcessingCleanUp();
           
           

        }

        
        public void prepareDataBase()
        {
            new DataBaseOperator().EmptyStagingTables();
        }
        
        
        public void postProcessingCleanUp()
        {
            ArchiveWorkingDir();
            ClearDirs(); 
        }
        

        public void unZipFilesToWorkingDir(string workingFolderPath)
        {
            unZipFilesIfRequired(originalFilesFromSFTPolderPath,workingFolderPath);
        }



        public void ArchiveWorkingDir()
        {
              zipDirectory(workingFolderPath, Path.Combine(archiveFolderPath,"UberEatsFileUpload_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".ZIP"));
        }


        private void ClearDirs ()
        {
            ClearWorkingDir();
            ClearDirWithOriginalFilesFromSFTP();
        }



        private void ClearWorkingDir()
        {
            new DirectoryInfo(workingFolderPath).Empty();
        }


        private void ClearDirWithOriginalFilesFromSFTP()
        {
            new DirectoryInfo(originalFilesFromSFTPolderPath).Empty(); 
        }


        private void processIgnoredFile(string fullFileName)
        {
            LogFile.SaveAppLog($"Application ignored file {fullFileName}");
            RenameIgnoredFile(fullFileName);

        }
        
        
        private void RenameIgnoredFile(string fullFileName)
        {
            File.Move(fullFileName, fullFileName + "_IGNORED.csv");
        }
  
    }

    public static class ExtensionMethods
    {
        public static void Empty(this DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }

    }

}
