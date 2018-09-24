using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Vulcan.Common2015.SeleniumLib.Helpers
{
    public static class VReport
    {
        private static string reportFilePath;
        private static string vreportFilePath;
        private static string screenshotName;
        private static int screenshotCnt = 0;

        /// <summary>
        /// Kolejka logów - zrzucana do vraportu przy wystąpieniu błędu. Jest wtedy automatycznie czyszczona.
        /// </summary>
        public static LogQueue LogQueue;

        public static void Setup(string reportFilePath, string vreportFilePath)
        {
            VReport.reportFilePath = reportFilePath;
            VReport.vreportFilePath = vreportFilePath;
            VReport.LogQueue = new LogQueue();

            screenshotName = Path.GetFileNameWithoutExtension(reportFilePath);

            if (File.Exists(vreportFilePath))
                File.Delete(vreportFilePath);

            var doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode root = doc.CreateElement("report");
            doc.AppendChild(root);

            doc.Save(vreportFilePath);
        }

        public static void LogError(string msg, string testName, string screenFile, string type, string zeusId)
        {
            //TODO do zoptymalizowania
            var doc = VXml.Load(vreportFilePath);

            var n = doc.CreateElement("error");

            var cData = doc.CreateCDataSection(VReport.LogQueue.Logs + Environment.NewLine + msg);

            n.AppendChild(cData);

            VXml.AppendAttr(n, "testName", testName);
            VXml.AppendAttr(n, "screenshot", screenFile + ".jpg");
            VXml.AppendAttr(n, "screenshotThumb", screenFile + "_thumb.jpg");
            VXml.AppendAttr(n, "type", type);
            if (!string.IsNullOrEmpty(zeusId))
                VXml.AppendAttr(n, "zeusId", zeusId);

            doc.DocumentElement.AppendChild(n);

            doc.Save(vreportFilePath);

            VReport.LogQueue.Clear();
        }

        public static string Screenshot()
        {
            //Report.Screenshot();
            //moze te nazwy sie da jakos wyciagnac sensownie, ale nie znam sie na ranorexie :)
            screenshotCnt++;
            return screenshotName + "_" + screenshotCnt + "_rxlog";
        }
    }
}
