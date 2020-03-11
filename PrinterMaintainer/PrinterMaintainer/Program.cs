using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrinterMaintainer
{
    internal static class Program
    {
        private const string ConfigFilename = "config.yml";
        private const string LastPrintTimeFilename = "lastPrintTime.dat";

        static void Main()
        {
            try
            {
                PrinterMaintainer printerMaintainer = new PrinterMaintainer(ConfigFilename, LastPrintTimeFilename);
                if (printerMaintainer.ShouldPrintTestPage())
                {
                    DialogResult result = MaintainerMessages.ShowQuestion();
                }
            }
            catch (Exception e)
            {
                MaintainerMessages.ShowError(e.ToString());
            }
        }
    }
}