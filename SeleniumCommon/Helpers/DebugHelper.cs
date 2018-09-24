using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Vulcan.Common2015.SeleniumLib.Helpers
{
    public class DebugHelper
    {
        private static string _reportFolderPath;
        public static string ReportFolderPath
        {
            get { return _reportFolderPath; }
        }


        public static string NameDll;

        public static List<Type> NunitTestAtrributsTypes; 

        public static ILog Logger;

        public DebugHelper()
        {
            NunitTestAtrributsTypes = new List<Type>();
            NunitTestAtrributsTypes.Add(typeof(TestAttribute));
            NunitTestAtrributsTypes.Add(typeof(TestCaseSourceAttribute));

            var onlyLogsFolder = ConfigurationManager.AppSettings["onlyLogsFolder"] != null && bool.Parse(ConfigurationManager.AppSettings["onlyLogsFolder"]);
            _reportFolderPath = onlyLogsFolder ? ConfigurationManager.AppSettings["logsFolder"] : Path.Combine(ConfigurationManager.AppSettings["logsFolder"], NameDll != null ? string.Format("{0}", NameDll) : string.Format(@"{0:yyyy_MM_dd_HH_mm_ss}", System.DateTime.Now));

            RecordHelper.RecordingPath = _reportFolderPath;

            string xmlReportLevel = ConfigurationManager.AppSettings["xmlReportLevel"];
            Console.WriteLine("Zapisuje w folderze: " + _reportFolderPath);

            if (!Directory.Exists(_reportFolderPath))
                Directory.CreateDirectory(_reportFolderPath);

            string reportFilePath = Path.Combine(_reportFolderPath, "at_report.xml");

            string vreportFilePath = Path.Combine(_reportFolderPath, "vat_report.xml");
            //switch (xmlReportLevel)
            //{
            //    case "Debug":
            //        xmlLevel = ReportLevel.Debug;
            //        break;
            //    case "Info":
            //        xmlLevel = ReportLevel.Info;
            //        break;
            //    case "Warn":
            //        xmlLevel = ReportLevel.Warn;
            //        break;
            //    case "Error":
            //        xmlLevel = ReportLevel.Error;
            //        break;
            //    case "Success":
            //        xmlLevel = ReportLevel.Success;
            //        break;
            //    case "Failure":
            //        xmlLevel = ReportLevel.Failure;
            //        break;
            //    default:
            //        xmlLevel = ReportLevel.Debug;
            //        break;
            //}

            VReport.Setup(reportFilePath, vreportFilePath);
            SetupLog4Net(reportFilePath);
            //Report.Setup(xmlLevel, reportFilePath, false, true);
        }

        public static void SetupLog4Net(string path)
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = path;
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "1GB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;            
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;

            Logger = LogManager.GetLogger(typeof(DebugHelper));
        }

    }
}
