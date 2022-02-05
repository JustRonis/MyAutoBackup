using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AutoBackup
{
    public class ServicoAutoBackup
    {
        public void Start()
        {
           
            string pathOrigem = @"C:\Users\ronal\Desktop\MyFolder\Ronis";
            string pathCopy = @"C:\Users\ronal\Desktop\MyFolder\C_Ronis";
            DirectoryInfo parentDirectoryInfo = new DirectoryInfo(@"C:\Users\ronal\Desktop\MyFolder");

            ClearReadOnly(parentDirectoryInfo);
            var pathCopyInfo = new DirectoryInfo(pathCopy);
            if (pathCopyInfo.Exists)
            {
                DeleteFolder(pathCopy);
            }       
            CopyDirectory(pathOrigem, pathCopy, true);
            ZipAndSend();
        }

        private void ClearReadOnly(DirectoryInfo parentDirectory)
        {
            if (parentDirectory != null)
            {
                parentDirectory.Attributes = FileAttributes.Normal;
                foreach (FileInfo fi in parentDirectory.GetFiles())
                {
                    fi.Attributes = FileAttributes.Normal;
                }
                foreach (DirectoryInfo di in parentDirectory.GetDirectories())
                {
                    ClearReadOnly(di);
                }
            }
        }
        static void DeleteFolder(string pathCopy)
        {
            Directory.Delete(pathCopy, true);
            System.Threading.Thread.Sleep(10000);
        }

        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {

            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Arquivo de origem não existe: {dir.FullName}");
            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destinationDir);
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }

            
        }

        static void ZipAndSend()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            DateTime localDate = DateTime.Now;
            string dataQuaseFormatada = localDate.ToString("s");
            string dataFormatada = dataQuaseFormatada.Substring(0, dataQuaseFormatada.IndexOf('T'));

            //string zip = @$"E:\Ronis-{dataFormatada}.zip";
            string zip = @$"C:\Users\ronal\Desktop\MyFolder\Ronis-{dataFormatada}.zip";
            string pathCopy = @"C:\Users\ronal\Desktop\MyFolder\C_Ronis";
            string pathDestino = @$"C:\Users\ronal\OneDrive\Autobackup-{dataFormatada}.zip";

            ZipFile.CreateFromDirectory(pathCopy, zip);
            Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(zip, pathDestino);
            



        }

        public void Stop()
            {
            //a
            }
            
    }


}








