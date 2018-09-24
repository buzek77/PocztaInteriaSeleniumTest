using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PocztaInteriaSeleniumTest
{
   public class Settings
    {
       public static string uzytkownik = ConfigurationManager.AppSettings["uzytkownik"];
       public static string connetionString = ConfigurationManager.AppSettings["connetionString"];
       public string backupFolder = ConfigurationManager.AppSettings["backupFolder"];
    }
}
