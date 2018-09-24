using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Vulcan.Common2015.SeleniumLib.Pages.Common
{
    public class VGrid : HtmlTable
    {

        public VGrid(IWebElement webElement, IWebDriver webDriver, bool loadOnStart = true,bool loadNotVisible = true,bool slowLoad = false)
            : base(webElement, webDriver, loadOnStart, loadNotVisible, slowLoad)
        {
            
        }

        public VGrid(string id, IWebDriver webDriver, bool loadOnStart = true, bool loadNotVisible = true, bool slowLoad = false)
            : base(id, webDriver, loadOnStart, loadNotVisible, slowLoad)
        {
            
        }

        protected override string[] GetHeaders()
        {
            return GetByCssAll("x-column-header-text", Context).Where(el => !el.GetAttribute("id").Contains("actioncolumn")).Select(SelectForBuffor).Where(s => s!= "").ToArray();
        }

        protected override string[][] GetRowData()
        {
            var gridsTBody = GetByCssAll("x-grid-body", Context);
            //if(gridsTBody.Count > 1) throw new Exception("ERRORs");
            var trdata = GetByCss("x-grid-body", Context).FindElements(By.XPath(".//tbody/tr"));
            return RowData(trdata,SlowLoad);
            //return GetByCss("x-grid-body", Context).FindElements(By.XPath(".//tbody/tr")).Select(GetDataFromTr).ToArray();
        }

        public void ZaznaczWiersz(string id)
        {
           

            var checker =
                Context.FindElement(
                    By.XPath(string.Format(".//div[text()={0}]/../.././/div[@class='x-grid-row-checker']", id)));
            checker.Click();
        }
        


        protected override ReadOnlyCollection<IWebElement> GetOneRowData(int index)
        {
            return GetByCss("x-grid-body", Context).FindElements(By.XPath(string.Format(".//tbody/tr[{0}]//*[text() != '']", index)));

        }

        protected override IWebElement GetTr(string text)
        {
            return GetByCss("x-grid-body", Context).FindElement(By.XPath(string.Format(".//*[contains(text(),'{0}')]/../..", text)));
        }

        protected override IWebElement GetTr(int index)
        {
            return GetByCss("x-grid-body", Context).FindElement(By.XPath(string.Format(".//tbody/tr[{0}]", index.ToString())));
        }

        internal  IWebElement GetTrForGrid(string text)
        {
            foreach (var grid in  GetByCssAll("x-grid-body", Context))
            {
              var result = grid.FindElements(By.XPath(string.Format(".//*[text() = {0}]/../..", text)));
              if (result.Count > 0)
                  return result[0];
               
            }
            return null;

        }

         

        protected IEnumerable<IWebElement> GetTdFromTr(string text)
        {
            var trs = Context.FindElements(By.XPath(string.Format(".//*[@data-recordid='{0}']",text)));

            List<IWebElement> toReturn = new List<IWebElement>();
            foreach (var tr in trs)
            {
                toReturn.AddRange(tr.FindElements(By.XPath("./td")).Where(s => !s.GetAttribute("class").Contains("actioncolumn")));
            }
            return toReturn;
        }

        public void InsertValue(string tekst,string kolumna,string value,string type = "textbox")
        {
            if (Columns == null)
            {
                Columns = GetHeaders();
            }
            WaitForLoad();
            var tds = GetTdFromTr(tekst);
            var index = -1;
            for (int i = 0; i < Columns.Count();i++)
            {
                if (!Columns[i].Contains(kolumna)) continue;
                index = i;
                break;
            }
            var td = tds.ToList()[index+tds.Count()-Columns.Count()];
            switch (type)
            {
                case "textbox":
                    var inputs = Context.FindElements(By.XPath(".//input")).Where(s => s.Displayed).Select(s=>s.GetAttribute("id")).ToList();
                    Actions action = new Actions(WebDriver);
                    action.DoubleClick(td);
                    action.Perform();
                    var inputsFind = Context.FindElements(By.XPath(".//input"));
                    int i = 5;
                    while (inputsFind.Count == 0 &&  inputsFind.Count(s => s.Displayed && !inputs.Contains(s.GetAttribute("id"))) == 0 || i < 5)
                    {
                        inputsFind = Context.FindElements(By.XPath(".//input"));  
                        i++;
                        Log("While podejscie : " + i);
                    }
                    if (inputsFind.Count(s => s.Displayed && !inputs.Contains(s.GetAttribute("id"))) == 0)
                    {
                        Log("Nie znalezionio poprawnie inputa do wprowadzenia danych uzywamy starej techniki");
                        action = new Actions(WebDriver);

                        action.SendKeys(Keys.Backspace);
                        action.SendKeys(value);
                        action.SendKeys(Keys.Enter);
                        action.Perform();
                    }
                    else
                    {


                        var inputsAfter =
                            inputsFind.First(
                                s => s.Displayed && !inputs.Contains(s.GetAttribute("id")));

                        inputsAfter.Clear();
                        inputsAfter.SendKeys(value);
                        inputsAfter.SendKeys(Keys.Enter);
                    }

                    break;
                case "combobox":
                    Actions actionCombo = new Actions(WebDriver);
                    actionCombo.DoubleClick(td);
                    actionCombo.Perform();    
                    SelectFromCombo(td, value,true);
                    break;
                
            }


            
           
        }

        public string[] GetColumns(bool forceReload = false)
        {
            if (forceReload)
                Columns = GetHeaders();
            if(Columns == null) throw  new Exception("Brak kolumn do pobrania");
            return Columns;
        }

        public override int GetTrCount(string text)
        {
            return GetByCss("x-grid-body", Context).FindElements(By.XPath(string.Format(".//*[text() = {0}]/../..", text))).Count;
        }

        private string SelectForBuffor(IWebElement a)
        {
            if (!a.Displayed && LoadNotVisible) // W gridach  buforowanych buforowane są takze kolumny 
            {
                var parent = a.FindElement(By.XPath("./.."));

                    return parent.GetAttribute("data-qtip");

            }
                
            return a.Text;
        }

        protected override string[] GetIgnoreCssClases()
        {
            return new[]
                       {
                           "x-grid-row-checker"
                       };
        }

        public virtual VEditor Dodaj()
        {
            ButtonClick("Dodaj",Context);
            return new VEditor(WebDriver);
        }

        public virtual VEditor Edytuj(string text)
        {
            EditIconClick(text);
            return new VEditor(WebDriver);
        }

        public void EditIconClick(string text)
        {
            WaitForLoad();
            var tr = GetTr(text);
            tr.Click();
            GetByCss("icon-edit", tr).Click();
            WaitForLoad();
        }

        public VGridEditor Zmien(string nazwaPrzycisku = "Zmień")
        {
            ButtonClick(nazwaPrzycisku, Context);
            WaitForLoad();
            return new VGridEditor(WebDriver);
        }
    }
}
