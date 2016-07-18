using System;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace DataLoader
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Program program = new Program();
            PaymentVoucherProcessor paymentVoucherProcessor = new PaymentVoucherProcessor();

            Util.PrintMessage("******************************************************************************", false);
            Util.PrintMessage("Starting file loader program ...");
            string sourceDirPath = ConfigurationManager.AppSettings["SourceDirectoryPath"];
           
            FixedWidthTextProcessor prnFileProcessor = new FixedWidthTextProcessor();

            paymentVoucherProcessor.LoadFiles(sourceDirPath);
            Util.PrintMessage("Stopping file loader program....");
            Util.PrintMessage("******************************************************************************", false);
           // Console.ReadKey();

        }

    }
}
