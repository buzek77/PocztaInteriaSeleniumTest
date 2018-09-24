using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Drawing.Imaging;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Vulcan.Common2015.SeleniumLib.Helpers;

namespace Vulcan.Common2015.SeleniumLib.Pages
{
    public class Page
    {
        protected IWebDriver WebDriver;
        private readonly bool duplicateIds = false;

        public static string GetUrlWithoutHttp(string url)
        {
            string[] paths = url.Replace("http://", "").Replace("https://", "").Replace("http:\\","").Replace("https:\\","").Replace("\\","").Split('/');
            string pageUrl = paths[0];
            return pageUrl;
        }

        public static int ImgCount = 1;

        public static IWebDriver CurrentWebDriver = null;

        public Page(IWebDriver webDriver)
        {
            WebDriver = webDriver;
            IJavaScriptExecutor executor = WebDriver as IJavaScriptExecutor;
            if (ConfigurationManager.AppSettings.Get("duplicateIds") != null && bool.Parse(ConfigurationManager.AppSettings.Get("duplicateIds")))
            {
                var result = executor.ExecuteScript(" return findDuplicateIds()");
                if (result != null)
                {
                    throw new Exception(result.ToString());
                }
            }
        }

        public Page()
        {
            WebDriver = null;
        }

        public bool IsBrowserOpen()
        {
            return WebDriver != null;
        }


        public void OpenBrowser(Dictionary<string,string> preference)
        {
            //string pathToCurrentUserProfiles = Environment.ExpandEnvironmentVariables("%APPDATA%") + @"\Mozilla\Firefox\Profiles"; // Path to profile
            DebugHelper.Logger.Info("Otwieranie przeglodarki");

                FirefoxProfile profile = new FirefoxProfile();// (pathsToProfiles[0]);
                profile.SetPreference("browser.tabs.loadInBackground", false); // set preferences you need
         
                if (preference != null)
                {
                    foreach (var pref in preference)
                    {
                        profile.SetPreference(pref.Key, pref.Value);
                    }
                }
                DebugHelper.Logger.Info("Koniec tworzenia profilu");
                //WebDriver = new PhantomJSDriver();
                try
                {
                    //Thread.Sleep(1000); // TODO Testujemy czy to to rozwiaze problem z wieszaniem sie firefoxa przy starcie
                    WebDriver = new FirefoxDriver(new FirefoxBinary(),profile);
                
                }
                catch (Exception)
                {
                    DebugHelper.Logger.Info("ERROR IN OPEN");
                    WebDriver = new FirefoxDriver(new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"), profile);
                }
                DebugHelper.Logger.Info("Init webdrivwer");
                WebDriver.Manage().Window.Maximize();
                // WebDriver = new InternetExplorerDriver();
                //DesiredCapabilities capabilities = DesiredCapabilities.Firefox(); // DesiredCapabilities.Firefox());

                //capabilities.SetCapability(FirefoxDriver.ProfileCapabilityName, profile.ToBase64String());
                //WebDriver = new RemoteWebDriver(new Uri("http://pro-test:4444/wd/hub"),DesiredCapabilities.InternetExplorer());

            // MEGA FAJNY RZECZ do debugowania !!!!!! tu sie pomysli o inncyh ciekawych zastosowaniach 
            //System.Uri uri = new System.Uri("http://localhost:7055/hub");
            //WebDriver = new RemoteWebDriver(uri, DesiredCapabilities.Firefox());

            //DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
            //capabilities.SetCapability(FirefoxDriver.ProfileCapabilityName,  prof);
            //driver = new RemoteWebDriver(new URL("http://localhost:4444/wd/hub"), capabilities);

            CurrentWebDriver = WebDriver;
        }

        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        public void OpenBrowser(string url, Dictionary<string, string> preference = null)
        {
            OpenBrowser(preference);
            GoToUrl(url);
        }

        public void CloseBrowser()
        {
            if (WebDriver != null)
            {
                WebDriver.Quit();
                WebDriver = null;
            }
        }

        public void GoToUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        protected void WaitForLoad(int waitingTime = 10000)
        {
            int i = 0;
            var path = ".//div[contains(@class,'x-mask-loading')]";
            var waits = WebDriver.FindElements(By.XPath(path));
            if (waits.Count <= 0) return;
            while (waits[0].Displayed)
            {
                System.Threading.Thread.Sleep(1000);
                i++;
                if (i > 30) throw new Exception("Proszę czekać jest za długo");
            }
        }

        protected IWebElement GetById(string id)
        {
            return WebDriver.FindElement(By.Id(id));
        }

        public void DoubleClick(IWebElement element)
        {
            Actions builder = new Actions(WebDriver);

            IAction doubleClick = builder.DoubleClick(element).Build();

            doubleClick.Perform();
        }

        protected IWebElement GetByText(string text,IWebElement context = null)
        {
            string path = string.Format(".//*[text()='{0}']", text);
            return context == null ? WebDriver.FindElement(By.XPath(path)) : context.FindElement(By.XPath(path));
        }

        protected ReadOnlyCollection<IWebElement> GetByTextAll(string text, IWebElement context = null)
        {
            string path = string.Format(".//*[text()='{0}']", text);
            return context == null ? WebDriver.FindElements(By.XPath(path)) : context.FindElements(By.XPath(path));
        }

        protected string PathForText(string text)
        {
            return string.Format(".//*[text()='{0}']",text);
        }

        protected IWebElement GetByTextAndCss(string text,string css, IWebElement context = null)
        {
            string path = string.Format(".//*[text()='{0}' and contains(@class,'{1}')]", text,css);
            return context == null ? WebDriver.FindElement(By.XPath(path)) : context.FindElement(By.XPath(path));
        }

        protected IWebElement GetByCss(string id, string type = "*")
        {
            var path = PathForCss(id, type);
            return WebDriver.FindElement(By.XPath(path));
        }

        protected ReadOnlyCollection<IWebElement> GetByCssAll(string id, string type = "*")
        {
            var path = PathForCss(id, type);
            return WebDriver.FindElements(By.XPath(path));
        }

        protected IWebElement GetByCss(string id, IWebElement context, string type = "*")
        {
            WaitForLoad();
            var path = PathForCss(id, type);
            return context.FindElement(By.XPath(path));
        }

        protected IWebElement GetWithWait(By by)
        {
          return  (new WebDriverWait(WebDriver, new TimeSpan(0, 0, 0, 15)))
                .Until(ExpectedConditions.ElementIsVisible(by));
        }

        protected IWebElement GetWithWaitExist(By by)
        {
            return (new WebDriverWait(WebDriver, new TimeSpan(0, 0, 0, 15)))
                  .Until(ExpectedConditions.ElementExists(by));
        }

        protected ReadOnlyCollection<IWebElement> GetByCssAll(string id, IWebElement context, string type = "*")
        {
            var path = PathForCss(id, type);
            return context.FindElements(By.XPath(path));
        }

        public static string PathForCss(string id, string type)
        {
            return ContainsPath("class", id, type);
        }

        public static string ContainsPath(string property,string value,string type = "*")
        {
            var path = string.Format(".//{0}[contains(@{1},'{2}')]", type, property,value);
            return path;
        }

        public IWebElement GetByContainsId(string id)
        {
           return WebDriver.FindElement(By.XPath(ContainsPath("id",id)));
        }

        public ReadOnlyCollection<IWebElement> GetByContainsIdAll(string id,string type = "*")
        {
            return WebDriver.FindElements(By.XPath(ContainsPath("id", id, type)));
        }

        protected IWebElement GetSpanTagByDivAndText(string div, string tekst)
        {
            WaitForLoad();
            return WebDriver.FindElement(By.XPath(string.Format(".//*[@id='{0}']/.//*[text() ='{1}']", div, tekst)));
        }

        protected void ButtonClick(string tekst, IWebElement webElement = null)
        {
            string path = ".//span[contains(@class,'x-btn') and text() ='{0}']";
            string fullPath = string.Format(path, tekst);
            if(webElement != null)
            {
                webElement.FindElement(By.XPath(fullPath)).Click();
            }
            else
            {
                WebDriver.FindElement(By.XPath(fullPath)).Click();
            }
        }

        protected bool GetValueFromExjstHackedCheckbox(IWebElement checkbox) // Trzeba podac element na który sie ustawia klasa css (table w którym jest input)
        {
           return checkbox.GetAttribute("class").Contains("cb-checked");
        }

        protected void SelectFromCombo(IWebElement input, string value,bool isClicked = false)
        {
            if (!isClicked)
            {
                input.Click();
            }
            WaitForLoad();
            input.SendKeys(Keys.ArrowDown);
            WaitForLoad();
            var comboValues = WebDriver.FindElements(By.XPath(string.Format(".//li[text()='{0}']", value)));
            var el = comboValues.First(comboValue => comboValue.Displayed);
            ClickByJS(el);
        }

        public void ClickByJS(IWebElement el)
        {
            IJavaScriptExecutor executor = WebDriver as IJavaScriptExecutor;
            executor.ExecuteScript("arguments[0].click();", el);
        }

        public void TakeScreenshot()
        {
            Screenshot ss = ((ITakesScreenshot)WebDriver).GetScreenshot();
            ss.SaveAsFile(DebugHelper.ReportFolderPath + "\\"+ ImgCount +".png", ImageFormat.Png);
            ImgCount++;
        }

        protected void WybierzZDatePicera(string date, string id = "date-selector-")
        {

            // DDRZALA Jesli tu coś nie działa sprawdz najpierw czym masz wersje Firefoxa zgodna z wersja Selenium 
            string[] months = { "Sty", "Lut", "Mar", "Kwi", "Maj", "Cze", "Lip", "Sie", "Wrz", "Paź", "Lis", "Gru" };
            string[] parseDate = date.Split('.');

            var button = GetWithWait(By.XPath(ContainsPath("id", id, "input")));

            button.Click();

            var dpElementy = GetByCssAll("x-datepicker");
            IWebElement dp = GetVisibleElementFromList(dpElementy);

            var monthElementy = GetByCssAll("x-datepicker-month");
            IWebElement month = GetVisibleElementFromList(monthElementy);

            var a = month.FindElement(By.XPath("./a"));
            a.Click();
            
            var rokElementy = WebDriver.FindElements(By.XPath(string.Format(".//*[text() = '{0}']", parseDate[2])));
            IWebElement rok = rokElementy.Count == 1 ? rokElementy.First() : GetVisibleElementFromList(rokElementy); 
   

            if (rok.Text != parseDate[2])
            {
                GetWithWait(By.XPath(string.Format(".//*[text() = '{0}']", parseDate[2]))).Click();
            }
            else
            {
                rok.Click();
                rok.Click();
            }
           

            var miesiacElementy = WebDriver.FindElements(By.XPath(string.Format(".//*[text() = '{0}']", months[int.Parse(parseDate[1]) - 1])));
            var miesiac =  miesiacElementy.Count == 1 ? miesiacElementy.First() : GetVisibleElementFromList(miesiacElementy); 
            miesiac.Click();

            dp.FindElement(By.XPath(".//*[contains(text(),'OK')]")).Click();

            var dzienElementy = WebDriver.FindElements(By.XPath(string.Format(".//*[text()='{0}' and contains(@class,'{1}')]", parseDate[0], "x-datepicker-date")));
            GetVisibleElementFromList(dzienElementy).Click();
            WaitForLoad();
        }


        private IWebElement GetVisibleElementFromList(IEnumerable<IWebElement> elements)
        {
            return elements.FirstOrDefault(webEl => webEl.Displayed);
        }
    }
}
