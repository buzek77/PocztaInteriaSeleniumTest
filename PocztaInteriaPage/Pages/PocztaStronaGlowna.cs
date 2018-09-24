using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using Vulcan.Common2015;
using Vulcan.Common2015.SeleniumLib.Pages;
using Vulcan.Common2015.SeleniumLib;
using Selenium;
using Selenium.Internal;
using Selenium.Internal.SeleniumEmulation;
using NUnit.Framework;


namespace PocztaInteriaPage
{
    public class PocztaStronaGlowna:StronaLogowania
    {
        public PocztaStronaGlowna(IWebDriver webDriver)
            : base(webDriver)
        {
           
        }
        public NowaWiadomosc NowaWiadomosc()
        {   Log("Klikam w nowa wiadomość");
            WebDriver.FindElement(By.LinkText("Nowa")).Click();
            var przejdzDoNowejWiadomosci = new NowaWiadomosc(WebDriver);
            return przejdzDoNowejWiadomosci;
        }
        public void Wyloguj()
        {   Log("Wyloguj");
            WebDriver.FindElement(By.XPath("//html[@id='ng-app']/body/section/div/div/div[2]/ul/li")).Click();
            WebDriver.FindElement(By.CssSelector("a.link.icon-logout > span")).Click();
        }
        public string SprawdzWyslanaWiadomosc(int i)
        {
            GetWithWait(By.XPath("//html[@id='ng-app']/body/section[4]/section/div/ul/li/div/div[2]"));
            Log("sprawdzam wiadomość");
           if(i==1)
           { 
               
               string wynik =
                WebDriver.FindElement(
                    By.XPath("//html[@id='ng-app']/body/section[4]/div/div/section/ul/li/div[2]/div/h2/span")).Text;
               return wynik;
           }
            if (i==2)
            {
                string wynik =
                 WebDriver.FindElement(
                     By.XPath("//html[@id='ng-app']/body/section[4]/div/div/section/ul/li/div[2]/div/div/span")).Text;
                return wynik;
            }
            return null;
        }
        public void UsunWiadomosc(int i)
        {
            Log("usuwam wiadomosc");
            WebDriver.FindElement(By.XPath("//html[@id='ng-app']/body/section[4]/div/div/section/ul/li["+i+"]/div/label")).Click();
            WebDriver.FindElement(By.XPath("//html[@id='ng-app']/body/section[4]/div/div/header/section[3]/div[3]/span")).Click();
            GetWithWait(By.XPath("//html[@id='ng-app']/body/div[2]/div/ul/li/div/span"));
            WaitForLoad(4000);
        }

        public KoszPage PrzejdzDoKosza()
        {
            WebDriver.FindElement(By.LinkText("Kosz")).Click();
            var przejdzDoKosza = new KoszPage(WebDriver);
            return przejdzDoKosza;
        }

    }

}
