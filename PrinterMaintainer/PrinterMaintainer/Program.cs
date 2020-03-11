using System;
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
                    AskForPrinting(printerMaintainer);
            }
            catch (Exception e)
            {
                MaintainerMessages.ShowError(e.ToString());
            }
        }

        private static void AskForPrinting(PrinterMaintainer printerMaintainer)
        {
            DialogResult result = MaintainerMessages.ShowQuestion();
            if (result == DialogResult.Yes)
                printerMaintainer.PrintTestPage();
            else if (result == DialogResult.No)
                printerMaintainer.DelayPrinting();
        }
    }
}