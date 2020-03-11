using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PrinterMaintainer
{
    public class PrinterMaintainer
    {
        public class Configuration
        {
            public int PrintToPrintTimeDays { get; set; }
            public string PrintCommand { get; set; }
        }

        public Configuration Config { get; }
        public DateTime LastPrintTime { get; }

        public PrinterMaintainer(string configFilename, string lastPrintTimeFilename)
        {
            Config = ParseConfiguration(configFilename);
            LastPrintTime = ParseLastPrintTime(lastPrintTimeFilename);
        }

        private DateTime ParseLastPrintTime(string lastPrintTimeFilename)
        {
            string lastPrintTimeString = File.ReadAllText(lastPrintTimeFilename);
            return DateTime.FromFileTime(long.Parse(lastPrintTimeString));
        }

        private Configuration ParseConfiguration(string configFilename)
        {
            string configYaml = File.ReadAllText(configFilename);
            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance).Build();
            return deserializer.Deserialize<Configuration>(configYaml);
        }

        public bool ShouldPrintTestPage()
        {
            //TODO: Implement.
            return true;
        }

        public void PrintTestPage()
        {
            //TODO: Implement.
            Console.Out.WriteLine("Printing test page...");
        }

        public void DelayPrinting()
        {
            //TODO: Implement.
            Console.Out.WriteLine("Delaying printing...");
        }
    }
}