using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PocztaInteriaPage
{
    public class KoszPage:PocztaStronaGlowna
    {
        public KoszPage(IWebDriver webDriver)
            : base(webDriver)
        {
           
        }

        public void OproznijKosz()
        {
            Log("oprozniam kosz");
            WaitForLoad(4000);
            GetWithWait(By.XPath("//html[@id='ng-app']/body/section[4]/div/div/section/div/span/a")).Click();
            GetWithWait(By.XPath("//div/fieldset/button"));
            WebDriver.FindElement(By.XPath("//div/fieldset/button")).Click();
        }
        public string SprawdzKosz()
        {
            Log("sprawdzam kosz");
            GetWithWait(By.XPath("//html[@id='ng-app']/body/section[4]/div[2]/div/section/div/div[2]/h2"));
           string wynik =
                WebDriver.FindElement(By.XPath("//html[@id='ng-app']/body/section[4]/div[2]/div/section/div/div[2]/h2"))
                    .Text;
            return wynik;
        }

    }

}
