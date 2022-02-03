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
           
            string pathOrigem = @"E:\Ronis";
            string pathCopy = @"E:\C_Ronis";
            CopyDirectory(pathOrigem, pathCopy, true);
            ZiparEnviarPasta();
        }

        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {

            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");
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

        static void ZiparEnviarPasta()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            DateTime localDate = DateTime.Now;
            string dataQuaseFormatada = localDate.ToString("s");
            string dataFormatada = dataQuaseFormatada.Substring(0, dataQuaseFormatada.IndexOf('T'));

            string zip = @$"E:\Ronis-{dataFormatada}.zip";
            string pathCopy = @"E:\C_Ronis";
            string pathDestino = @$"C:\Users\ronal\OneDrive\AutobackUp-{dataFormatada}.zip";

            ZipFile.CreateFromDirectory(pathCopy, zip);
            Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(zip, pathDestino);


        }

        public void Stop()
            {
                
            }
            
    }


}








