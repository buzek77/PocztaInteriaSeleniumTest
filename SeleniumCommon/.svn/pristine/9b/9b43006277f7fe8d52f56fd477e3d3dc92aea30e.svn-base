using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Vulcan.Common2015.SeleniumLib.Helpers
{
    public class RecordHelper
    {

        public static bool Recording;
        /// <summary>
        /// Sciezka do katalogu w ktorym zapisywane sa filmy z przebiegu testow
        /// </summary>
        public static string RecordingPath;

        /// <summary>
        /// Nazwa aktualnie wykonywanego testu
        /// </summary>
        public static string CurrentTestName = "Init";
        public static string PreviousTestName = "Init";

        private static bool _directoryCreated;

        public static void StartRecord()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["recordTests"]))
            {
                //Console.WriteLine("nagrywano " + Recording);
                if (!Recording)
                {
                    Console.Write(DateTime.Now);
                    Console.WriteLine(" start recording");
                    var process = new Process();
                    var startInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = ConfigurationManager.AppSettings["FBCmdPath"]
                    };
                    if (!_directoryCreated)
                    {
                        //var recordPath = ConfigurationManager.AppSettings["logsFolder"];

                        var recordPath = DebugHelper.ReportFolderPath;
                        //var sb = new StringBuilder();
                        var sb1 = new StringBuilder();
                        // do wywlaenia jak już beda wszystkie foldery tworzone przez builda
                        //sb.Append(DateTime.Now);
                        //sb.Replace(":", "_");
                        //sb.Replace("-", "_");
                        //sb.Replace(" ", "_");
                        //sb.Append(@"\Filmy");
                        sb1.Append(recordPath);
                        sb1.Append(@"\");
                        sb1.Append(@"Filmy");
                        RecordingPath = sb1.ToString();
                        Console.WriteLine(RecordingPath);
                        Directory.CreateDirectory(RecordingPath);
                        _directoryCreated = true;
                    }
                    startInfo.Arguments = @"/start";
                    //startInfo.Arguments = @"/stop";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    Recording = true;
                }
            }
        }

        public static void StopRecord(bool success = true, string videoFileName = "")
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["recordTests"]))
            {
                //Console.WriteLine("nagrywano " + Recording);
                if (Recording)
                {
                    Console.Write(DateTime.Now);
                    Console.WriteLine("stop recording");
                    var process = new Process();
                    var saveFailsOnly = !Convert.ToBoolean(ConfigurationManager.AppSettings["saveFailVideosOnly"]);
                    var startInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = ConfigurationManager.AppSettings["FBCmdPath"]
                    };
                    if (!success || saveFailsOnly)
                    {
                        var recordPath = RecordingPath;
                        var sb = new StringBuilder();
                        var sb1 = new StringBuilder();
                        sb1.Append(@"/stop /save-fbr ");
                        sb.Append(recordPath);
                        sb.Append(@"\");

                        //var date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        //date = date.Replace(":", "_");
                        //date = date.Replace("-", "_");
                        if (!success) sb.Append("FAIL_");
                        //sb.Append(date);
                        if (CurrentTestName == PreviousTestName)
                        {
                            sb.Append("Init");
                        }
                        else
                        {
                            sb.Append(CurrentTestName.Equals(null) ? "" : CurrentTestName);
                            PreviousTestName = CurrentTestName.ToString();
                        }

                        sb.Replace(" ", "_");
                        sb1.Append(sb);
                        sb1.Append(".fbr");
                        sb1.Append(" /save-swf ");
                        sb1.Append(sb);
                        sb1.Append(".swf");
                        var videoName = sb1.ToString();
                        Console.WriteLine("zapisywanie filmu " + videoName);
                        startInfo.Arguments = videoName;
                        process.StartInfo = startInfo;
                    }
                    else
                    {
                        startInfo.Arguments = @"/stop";
                        process.StartInfo = startInfo;
                        PreviousTestName = CurrentTestName.ToString();
                    }
                    process.Start();
                    process.WaitForExit();
                    Recording = false;

                }
            }
        }
    }
}
