using System;
using System.Configuration;

namespace DataLoader
{
    class Program
    {
        
        static void Main(string[] args)
        {
            args = new string[] { "Payment" };
            Program program = new Program();

            Util.PrintMessage("******************************************************************************", false);
            Util.PrintMessage("Starting file loader program ...");

            if(args!=null && args.Length>0 && !string.IsNullOrEmpty(args[0]))
            {
                IDataProcessor dataProcessor=GetProcessor(args[0]);
                dataProcessor.ProcessData();
            }
            else
            {
                Util.PrintMessage("No argument");
            }

            Util.PrintMessage("Stopping file loader program....");
            Util.PrintMessage("******************************************************************************", false);

           Console.ReadKey();

        }
        private static IDataProcessor GetProcessor(string arg)
        {
            if(arg.Equals("Payment",StringComparison.InvariantCultureIgnoreCase))
            {
                return new PaymentVoucherProcessor();
            }
            else if (arg.Equals("AseanSales", StringComparison.InvariantCultureIgnoreCase))
            {
                return new AseanSalesProcessor();
            }
            else
            {
                throw new ArgumentException(string.Format("For argument-{0} no processor implemented.Currently supported features are:Payment and AseanSales.",arg));
            }
        }
    }
}
