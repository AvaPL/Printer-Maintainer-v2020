using System;
using System.Diagnostics;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PrinterMaintainer
{
    public class PrinterMaintainer
    {
        private readonly Configuration config;
        private readonly DateTime lastPrintTime;
        private readonly string lastPrintTimeFilename;

        public PrinterMaintainer(string configFilename, string lastPrintTimeFilename)
        {
            this.lastPrintTimeFilename = lastPrintTimeFilename;
            config = ParseConfiguration(configFilename);
            lastPrintTime = ParseLastPrintTime(lastPrintTimeFilename);
        }

        private Configuration ParseConfiguration(string filename)
        {
            string configYaml = File.ReadAllText(filename);
            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance).Build();
            return deserializer.Deserialize<Configuration>(configYaml);
        }

        private DateTime ParseLastPrintTime(string filename)
        {
            string lastPrintTimeString = File.ReadAllText(filename);
            return DateTime.FromFileTime(long.Parse(lastPrintTimeString));
        }


        public bool ShouldPrintTestPage()
        {
            TimeSpan timeSinceLastPrinting = DateTime.Now.Subtract(lastPrintTime);
            return timeSinceLastPrinting.TotalDays > config.PrintToPrintTimeDays;
        }

        public void PrintTestPage()
        {
            SavePrintTime();
            ExecutePrintCommand();
        }

        private void SavePrintTime()
        {
            string nowString = DateTime.Now.ToFileTime().ToString();
            File.WriteAllText(lastPrintTimeFilename, nowString);
        }

        private void ExecutePrintCommand()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C " + config.PrintCommand
                }
            };
            process.Start();
        }

        public void DelayPrinting()
        {
            DateTime artificialPrintTime = DateTime.Now.AddDays(-config.PrintToPrintTimeDays / 2.0);
            string artificialPrintTimeString = artificialPrintTime.ToFileTime().ToString();
            File.WriteAllText(lastPrintTimeFilename, artificialPrintTimeString);
        }

        private class Configuration
        {
            public int PrintToPrintTimeDays { get; set; }
            public string PrintCommand { get; set; }
        }
    }
}