using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    interface IDataProcessor
    {
       void LoadData(string sourceDirPath, string DestDirPath);
    }
}
