using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Vulcan.Common2015.SeleniumLib.Pages.Common
{
    public class MainRibbonPage : Page
    {
        public MainRibbonPage(IWebDriver webDriver )
            : base(webDriver)
        {

        }

        public MainRibbonPage()
        {
            
        }

        protected void WybierzZakladkeNaRibbonie(string tekst)
        {
            //WaitForLoad();
            //Keyboard.Press(Keys.Escape);
            //DebugLog("Wybieram zakładke : " + tekst);
            GetSpangTagOnRibbon(tekst).Click();
        }

        protected void WybierzPrzyciskNaRibbonie(string tekst)
        {
            GetSpangTagOnRibbon(tekst).Click();
            //WaitForLoad();
        }

        protected void WybierzNaDrzewku(string tekst)
        {
            WaitForLoad();
            var element = PobierzElementDrzewka(tekst);
            element.Click();
            element.Click();
            WaitForLoad();
        }

        protected void RozwinNaDrzewku(string tekst)
        {
            var treeItem = PobierzElementDrzewka(tekst).FindElement(By.XPath("./..")); //.FindElement(By.XPath("./../img[2]")).Click();
            GetByCss("x-tree-expander",treeItem).Click();
            WaitForLoad();
        }
        
        private IWebElement PobierzElementDrzewka(string nazwa)
        {
            WaitForLoad();
            string path = ".//span[contains(@class,'x-tree-node-text') and text() ='{0}']";
            return GetWithWait(By.XPath(string.Format(path,nazwa)));
        }

        protected void KliknijDodaj()
        {
            ButtonClick("Dodaj");
        }

        

        private IWebElement GetSpangTagOnRibbon(string tekst)
        {
            return GetSpanTagByDivAndText("ribbon-view", tekst);
        }
    }
}
