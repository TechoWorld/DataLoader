using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace DataLoader
{
    class FileHandler
    {
        internal void MoveFile(string sourceFilePath, string destinationDirPath)
        {
            FileInfo fileInfo = new FileInfo(sourceFilePath);
            if (!fileInfo.Exists)
                throw new FileNotFoundException(string.Format("Source File {0} does not exist",sourceFilePath));
 
            if (!Directory.Exists(destinationDirPath))
                Directory.CreateDirectory(destinationDirPath);

            string appendName=File.Exists(Path.Combine(destinationDirPath,fileInfo.Name))? Util.GetDateWithTimestamp() : string.Empty;

            string temppath = Path.Combine(destinationDirPath, (string.IsNullOrWhiteSpace(appendName)? fileInfo.Name : GetDestFileName(fileInfo, appendName)));
            fileInfo.MoveTo(temppath);
            
        }

        internal string GetDestFileName(FileInfo srcFileInfo,string appendName)
        {
            return string.Format("{0}-{1}{2}", srcFileInfo.Name.Replace(srcFileInfo.Extension, string.Empty),appendName,srcFileInfo.Extension);
        }


    }
}
