using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Vulcan.Common2015;
using Vulcan.Common2015.SeleniumLib.Pages;
using Vulcan.Common2015.SeleniumLib;
using Selenium;
using Selenium.Internal;
using Selenium.Internal.SeleniumEmulation;
using System.Configuration;

namespace PocztaInteriaPage

{
    public class StronaLogowania:Page
    {
        public StronaLogowania(string url)
        {
            OpenBrowser(url); //czeka na załadowanie dokumentu 
            WaitForLoad();
            Log("Rozpoczynam test");

        }
        public StronaLogowania(IWebDriver webDriver)
            : base(webDriver)
        {
            
        }
        public IWebDriver openWeb()
        {
            return WebDriver;
        }

        public PocztaStronaGlowna Zaloguj(string uzytkownik, object haslo)
        {
           Log("Loguje się jako "+uzytkownik);
           WebDriver.FindElement(By.Id("formEmail")).SendKeys(uzytkownik);
           WebDriver.FindElement(By.Id("formPassword")).SendKeys(((object[])(haslo))[0].ToString());
           WebDriver.FindElement(By.Id("formSubmit")).Click();
           var przejdzDoPocztaStronaGlowna = new PocztaStronaGlowna(WebDriver);
           return przejdzDoPocztaStronaGlowna;
        }

        
    }
}
