using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocztaInteriaSeleniumTest
{
   public class ZapytaniaSQL
   {
       public static readonly string pobierzHaslo =
           String.Format("SELECT haslo FROM uzytkownicy WHERE uzytkownik='" + Settings.uzytkownik+"'");

       public static readonly string pobierzAdresata = String.Format("SELECT adresat FROM adresaci WHERE id=1");
   }
}
