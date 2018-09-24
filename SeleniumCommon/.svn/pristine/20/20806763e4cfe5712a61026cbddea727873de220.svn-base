using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Vulcan.Common2015.SeleniumLib.Pages.Common
{
    
    public class VEditor : VWindow
    {
        private readonly string[] _knownInputHtmlTypes = new[] {"input", "textarea"};
        private readonly Dictionary<string, List<IWebElement>> _inputs = new Dictionary<string, List<IWebElement>>();
        private readonly IWebDriver _webDriver;
        private readonly string _labelCSSClass;
        private Dictionary<string, Func<IWebElement, string>> _pobierzWartosciFunctions = null;

        public VEditor(IWebDriver webDriver, string windowName = "", string labelCSSClass = "v-modelform-field-label")
            : base(webDriver, windowName)
        {
            WaitForLoad(); // na wypadek dotyczytywania storow z serwera

            _webDriver = webDriver;
            _labelCSSClass = labelCSSClass;
            GetEditorFields(webDriver, labelCSSClass);
        }

        public void Refresh()
        {
            WaitForLoad();
            var openWindows = GetByCssAll("x-window-closable").Where(s => s.Displayed).ToList();
            if (openWindows.Count == 1)
            {
                Window = openWindows[0];
                GetEditorFields(_webDriver, _labelCSSClass);
            }
        }

        protected void GetEditorFields(IWebDriver webDriver, string labelCSSClass = "v-modelform-field-label")
        {
            var labes = GetByCssAll(labelCSSClass, Window);

            foreach (var webElement in labes)
            {
                var text = webElement.Text;
                if (webElement.Text != "")
                {
                    var tempList = new List<IWebElement>();
                    var forInput = webElement.GetAttribute("for");
                    if (!String.IsNullOrEmpty(forInput))
                        // ulatwiamy sobie zycie jesli jest jawinie podony for to z niego korzystamy
                    {
                        var input = webDriver.FindElement(By.Id(forInput));
                        tempList.Add(input);
                    }
                    else
                    {
                        foreach (var type in _knownInputHtmlTypes)
                        {
                            var fields = webElement.FindElements(By.XPath(string.Format("./../..//{0}", type)));
                            tempList.AddRange(fields);
                        }
                    }
                    _inputs[text] = tempList;
                }
            }
        }

        public void WprowadzWartosc(string label, string wartosc, bool clear = true)
        {
            var inputList = _inputs[label];
            var type = inputList[0].GetAttribute("role");
            switch (type)
            {
                case "checkbox":
                        inputList[0].Click();
                        break;
                case "textbox":
                        if(clear)
                            inputList[0].Clear();
                        inputList[0].SendKeys(wartosc);
                        inputList[0].Click();
                        break;
                case "spinbutton": // pola dla numerów w exjts (w zeusie bez strzalek ale pewnie moga miec)
                        inputList[0].Clear();
                        inputList[0].SendKeys(wartosc);
                        break;
                case "combobox":
                        inputList[0].Click();
                        SelectFromCombo(inputList[0], wartosc);
                        break;
                case "radio":
                    foreach (var input in from input in inputList let radioText = input.FindElement(By.XPath("./../label")).Text where radioText == wartosc select input)
                    {
                        input.Click();
                        break;
                    }
                    break;  
            }
        }

        public string PobierzWartosc(string label)
        {
            if (_pobierzWartosciFunctions == null) 
                _pobierzWartosciFunctions = PobierzWartoscFunctions();

            var inputList = _inputs[label];
            var type = inputList[0].GetAttribute("role");
            switch (type)
            {
                case "textbox":
                    {
                        return _pobierzWartosciFunctions.ContainsKey(type) ? _pobierzWartosciFunctions[type](inputList[0]) : inputList[0].GetAttribute("value");
                    }
            }
            return "";
        }

        public virtual Dictionary<string, Func<IWebElement, string>> PobierzWartoscFunctions() // Tu mozna podaac swoje implemetacje oblusgi pobieraniwa wartosci z typu html
        {
            return new Dictionary<string, Func<IWebElement, string>>();
        }

        public VEditor Dalej()
        {
            WaitForLoad();
            ButtonClick("Dalej", Window);
            return new VEditor(WebDriver);
  
        }

        public VGridEditor GetGrid()
        {
            return new VGridEditor(WebDriver,"Edycja");
        }

        public void WprowadzWartosciHurtowo(VField[] pola)
        {
            foreach (var vField in pola)
            {
                WprowadzWartosc(vField.Label,vField.Value);
            }
        }

    }

   
}
