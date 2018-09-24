using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace Vulcan.Common2015.SeleniumLib.Pages.Common
{
    public class VGridEditor : VWindow
    {
        public VGrid gridEdycja;
        public VGridEditor(IWebDriver webDriver, string windowName = "") : base(webDriver, windowName)
        {
            gridEdycja = new VGrid(Window,WebDriver,false);
        }

        public void WprowadzWartosc(string wiersz, string kolumna, string wartosc, string type = "textbox")
        {
            gridEdycja.InsertValue(wiersz,kolumna,wartosc,type);
        }

        public string[] PobierzNazwyKolumn()
        {
           return gridEdycja.GetColumns(true);
        }
    }
}
