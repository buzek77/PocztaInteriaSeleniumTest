

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using Vulcan.Common2015;
using Vulcan.Common2015.SeleniumLib.Pages;
using Vulcan.Common2015.SeleniumLib;
using Selenium;
using Selenium.Internal;
using Selenium.Internal.SeleniumEmulation;
using System.Collections;
using Keys = OpenQA.Selenium.Keys;


namespace PocztaInteriaPage
{
    public class NowaWiadomosc:PocztaStronaGlowna
    {
        public NowaWiadomosc(IWebDriver webDriver)
            : base(webDriver)
        {
        }


        public PocztaStronaGlowna WyslijMaila(object adresat)
        {   Log("Wysyłam wiadomość testową");
            WebDriver.FindElement(By.XPath("//div[2]/div/div/div[4]/div[3]/div/textarea")).Click();
            WebDriver.FindElement(By.XPath("//div[2]/div/div/div[4]/div[3]/div/textarea")).Clear();
            WebDriver.FindElement(By.XPath("//div[2]/div/div/div[4]/div[3]/div/textarea")).SendKeys(((object[])(adresat))[0].ToString());
            WebDriver.FindElement(By.XPath("//html[@id='ng-app']/body/div/div/div[2]/div/div/div[5]/input")).Clear();
            WebDriver.FindElement(By.XPath("//html[@id='ng-app']/body/div/div/div[2]/div/div/div[5]/input")).SendKeys("test");
            WebDriver.FindElement(By.XPath("//html[@id='ng-app']/body/div/div/div[2]/div/div/div[5]/input")).SendKeys(Keys.Tab);
            SendKeys.SendWait("to jest wiadomosc testowa");
            WebDriver.FindElement(By.CssSelector("div.ng-scope.ng-isolate-scope > div.ng-scope > div.ng-scope.ng-isolate-scope > div.composition-wrapper > div.composition-basic-actions > button.composition-basic-action.composition-basic-action--send")).Click();
            WaitForLoad(400000);
            var powrotDoPocztaStronaGlowna = new PocztaStronaGlowna(WebDriver);
            return powrotDoPocztaStronaGlowna;
        }
        
    }
}
