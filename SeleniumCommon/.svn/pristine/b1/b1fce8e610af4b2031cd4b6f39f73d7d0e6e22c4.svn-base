using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace Vulcan.Common2015.SeleniumLib.Pages.Common
{
    public class VWindow : Page
    {
        protected IWebElement Window;
        public VWindow(IWebDriver webDriver,string windowName = "",string cssClasName = "x-window-closable") : base(webDriver)
        {
            WaitForLoad();
            var openWindows = GetByCssAll(cssClasName).Where(s=>s.Displayed).ToList();
            if(openWindows.Count == 1)
            {
                Window = openWindows[0];
            }
            else
            {
                Log("Wiecej niz jedna okienko");
                foreach (var openWindow in openWindows)
                {
                    var label = GetByCss("x-window-header-text", openWindow).Text; // pobieramy heder okienka
                    if (label != windowName) continue;
                    Window = openWindow;
                    break;
                }
            }
            if(Window == null)
                throw new Exception("Nie ma otwartego okna");

        }

        


        public void IsError()
        {
            var openWidows = GetByCssAll("x-window-closable").Where(s=>s.Displayed).ToList();
            if (openWidows.Count == 0) return;
            if (openWidows.SelectMany(webElement => GetByCssAll("x-form-display-field",webElement)).Any(openWidow => openWidow.Text.Contains("UNKOWN_ERROR")))
            {
                throw new Exception("UNKOWN_ERROR");
            }
            throw new Exception("Nie zostały zamkniete okienka");
        }

        public VWindow Zapisz(bool validateSave = true,string expectedWindowTitle = "" )
        {
            ButtonClick("Zapisz",Window);
            WaitForLoad();
            if (validateSave)
            {
                IsError();
                return null;
            }
            return expectedWindowTitle != "" ? new VWindow(WebDriver,expectedWindowTitle) : null;
        }

        public void Anuluj()
        {
            ButtonClick("Anuluj", Window);
            WaitForLoad();
        }
        
        public VWindow KliknijUsun()
        {
            WaitForLoad();
            ButtonClick("Usuń", Window);
            return new VWindow(WebDriver, "", "x-window x-message-box");
        }

        public void Usun()
        {
            WaitForLoad();
            ButtonClick("Usuń", Window);
            var pot = new VWindow(WebDriver, "Potwierdzenie").Window;
            ButtonClick("Tak",pot);
            WaitForLoad();
        }

        public void Tak()
        {
            ButtonClick("Tak", Window);
            WaitForLoad();
        }

        public void Ok()
        {
            ButtonClick("OK", Window);
            WaitForLoad();
        }

        public void Nie()
        {
            ButtonClick("Nie", Window);
            WaitForLoad();
        }

        public void ZamknijX()
        {
            GetByCss("x-tool-close",Window).Click();         
        }

        public string PobierzKomunikat()
        {
            var texbok = Window.FindElements(By.XPath("././/div[@role='textbox']"));
            if(texbok.Count()!=1)
            {
                throw new Exception("To nie jest mesabox exjts");
            }
            return texbok.First().Text;
        }
        

    }
}
