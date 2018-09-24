using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Vulcan.Common2015.SeleniumLib.Pages.Common
{
    public class HtmlTable : Page
    {
        protected IWebElement Context;
        protected string[][] Rows = null;
        protected string[] Columns = null;
        protected List<List<VField>> InternalTable;
        protected string[] IgnoreCss;
        protected readonly bool LoadNotVisible;
        protected readonly bool SlowLoad;


        public HtmlTable(string id, IWebDriver webDriver, bool loadOnStart = true, bool loadNotVisible = true, bool slowLoad = false)
            : base(webDriver)
        {
            LoadNotVisible = loadNotVisible;
            SlowLoad = slowLoad;
            ParseHtmlTable(WebDriver.FindElement(By.Id(id)), loadOnStart);
        }

        public HtmlTable(IWebElement webElement, IWebDriver webDriver, bool loadOnStart = true, bool loadNotVisible = true, bool slowLoad = false)
            : base(webDriver)
        {
            LoadNotVisible = loadNotVisible;
            SlowLoad = slowLoad;
            ParseHtmlTable(webElement, loadOnStart);
        }

        private void ParseHtmlTable(IWebElement webElement, bool loadOnStart)
        {
            WaitForLoad();
            Context = webElement;
            IgnoreCss = GetIgnoreCssClases();
            if (loadOnStart)
            {
                Load();
            }
        }

        public void Load()
        {
            Columns = GetHeaders();
            Rows = GetRowData();
            InternalTable = CreateVFieldsFromStrings(Columns, Rows);
        }

        internal static IEnumerable<string> GetArrayFromTr(IWebElement webElement)
        {
            List<string> toReturn = new List<string>();

            var hack = webElement.Text.Replace("  ", "[S] [S]");
            var spaceSplit = hack.Split(new[] { "[S]" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in spaceSplit)
            {
                if (String.IsNullOrEmpty(s)) continue;
                if (s == " ")
                    toReturn.Add(s);
                else
                {
                    toReturn.AddRange(s.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).Select(ss => ss.Trim()));
                }
            }
            return toReturn;
        }

        internal static IEnumerable<string> GetArrayFromTrUsingTdFind(IWebElement webElement)
        {
            var tds = webElement.FindElements(By.XPath("./td"));
            return (from td in tds select td.Text).ToList();
        }

        public List<string> GetRowFromTable(int index = 1)
        {
            return GetRowFromTable(index, null);
        }

        protected static string[][] RowData(ReadOnlyCollection<IWebElement> trdata,bool slowLoad = false)
        {
            string[] ignoreRowWithCss = new string[] { "x-grid-wrap-row", "x-grid-group-row", " x-grid-row-summary" };
            List<string[]> toReturn = new List<string[]>();
            foreach (var webElement in trdata)
            {
                var classNames = webElement.GetAttribute("class");
                if(ignoreRowWithCss.Where(classNames.Contains).Any())
                    continue;              
                    List<string> temp = new List<string>();
                temp.AddRange(slowLoad ? GetArrayFromTrUsingTdFind(webElement) : GetArrayFromTr(webElement));

                toReturn.Add(temp.ToArray());
            }
            return toReturn.ToArray();
        }

        public List<string> GetRowFromTable(string nazwa)
        {
           return GetRowFromTable(null, nazwa);
        }

        private List<string> GetRowFromTable(int? index,string nazwa)
        {

            IWebElement tr = index == null ? GetTr(nazwa) : GetTr(((int)index));
            return GetArrayFromTr(tr).ToList();
            //var elements = data.Where(el => !IgnoreCss.Contains(el.GetAttribute("class")));
            //string wynik = "";
            //foreach (var webElement in elements)
            //{
            //    wynik += "\"" + webElement.Text + "\",";
            //}
            
            //return elements.Select(webElement => webElement.Text).ToList();
        }

        protected virtual ReadOnlyCollection<IWebElement> GetOneRowData(int index)
        {
            return Context.FindElements(By.XPath(string.Format(".//tbody/tr[{0}]//*[text() != '']", index)));
        }

        protected virtual ReadOnlyCollection<IWebElement> GetOneRowData(string text)
        {
            return GetTr(text).FindElements(By.XPath(string.Format(".//*[text() != '']")));
        }

        protected virtual IWebElement GetTr(string text)
        {
            return Context.FindElement(By.XPath(string.Format(".//*[text() = {0}]/../..", text)));
        }

        protected virtual IWebElement GetTr(int index)
        {
            return Context.FindElement(By.XPath(string.Format(".//tbody/tr[{0}]", index.ToString())));
        }

        public virtual int GetTrCount(string text)
        {
            return Context.FindElements(By.XPath(string.Format(".//*[text() = {0}]/../..", text))).Count;
        }

        public List<VField> GetAsVField(List<string> rowValues, List<string> fields = null)
        {
            if (Columns == null)
            {
                Columns = GetHeaders();
            }

            //string wynik = "";
            //foreach (var webElement in Columns)
            //{
            //    wynik += "\"" + webElement + "\",";
            //}

            var list = new List<VField>();
            int start = rowValues.Count - Columns.Count();

            for (int i = start; i < rowValues.Count; i++)
            {
                if (fields == null || fields.Contains(Columns[i-start]))
                {
                    list.Add(new VField() { Label = Columns[i-start], Value = rowValues[i] });
                }
            }

            return list;
        }


        protected virtual string[][] GetRowData()
        {
            //var trdata = GetByCss("x-grid-body", Context).FindElements(By.XPath(".//tbody/tr"));
           return Context.FindElements(By.XPath(".//tbody/tr")).Select(GetDataFromTr).ToArray();
            //return RowData(trdata);
           // return Context.FindElements(By.XPath(".//tbody/tr")).Select(GetDataFromTr).ToArray();
        }

        protected virtual string[] GetHeaders()
        {
            return Context.FindElements(By.XPath(".//thead/tr")).Select(GetDataFromThTr).ToArray()[0];
        }

        protected string[] GetDataFromThTr(IWebElement tr)
        {
            var td = tr.FindElements(By.XPath("./th"));
            if(td.Count == 0)
            {
                td = tr.FindElements(By.XPath("./td"));
            }
            return td.Select(GetTextFromTd).Where(s => s != null).ToArray();
        }


        protected string[] GetDataFromTr(IWebElement tr)
        {
            return tr.FindElements(By.XPath("./td")).Select(GetTextFromTd).Where(s => s != null).ToArray();
        }

        private string GetTextFromTd(IWebElement td)
        {
            if (!String.IsNullOrEmpty(td.Text)) return td.Text; // TODO nie jestem pewny czy tak powino byc 
            bool sothingToParse = false; // jesli nie ma nic z tekstem  np przyciski to ignorujemy takie kolumny
            var text = "";
            foreach (
                var el in
                    td.FindElements(By.XPath(".//*[text() != '']")).Where(
                        el => !IgnoreCss.Contains(el.GetAttribute("class"))))
            {
                sothingToParse = true;
                text += el.Text;
            }

            return sothingToParse ? text : null;
        }

        

        protected virtual string[] GetIgnoreCssClases()
        {
            return new string[]
                       {

                       };
        }

        public static List<List<VField>> CreateVFieldsFromStrings(string[] headers, string[][] values)
        {
            List<List<VField>> list =new List<List<VField>>();
            var headerCount = headers.Count();
            
            for (int i = 0; i < values.Count();i++ )
            {
                var list2 = new List<VField>();
                var rowHeaderCount = values[i].Count();
                var startIndex = rowHeaderCount - headerCount;
                
                    for (int j = startIndex; j < rowHeaderCount; j++)
                    {
                        list2.Add(new VField()
                                      {
                                          Label = headers[j-startIndex],
                                          Value = values[i][j]
                                      });
                    }
                    list.Add(list2);
                
            }
            return list;
        }

        

        public void SaveToXml(string fileName)
        {
            if(Columns == null || Rows == null) throw new Exception("Tabelka nie została załadowana zawołaj funkcje load");
            XElement root = new XElement("htmltable",
            new XElement("headers", Columns.Select(x => new XElement("header", x))),
            new XElement("rows", Rows.Select(x => new XElement("row", x.Select(y=>new XElement("value",y)))))
            );
            root.Save(fileName);
        }

        public static List<List<VField>> ReadFromXml(string fileName)
        {
            ; 
            var root  =XElement.Load(TestContext.CurrentContext.TestDirectory + "\\DaneDoTestow\\"+fileName);
            var xmlEl = root.Elements().ToList();

            var headers = xmlEl[0].Elements().Select(s => s.Value).ToArray();
            var values = xmlEl[1].Elements().Select(element => element.Elements().Select(value => value.Value).ToArray()).ToArray();
            return CreateVFieldsFromStrings(headers,values);
        }

        public List<List<VField>> GetToAssert()
        {
            return InternalTable;
        }

        // TO SA FUNKCJE ZROBIONE Z LENISTWA ZEBY NIE PRZEPISYWAC TABELEK
        public string GetToAssertHelper()
        {
           
            string begin = @"string[][] values = new[]
                           {";
            List<string> rowsString = new List<string>();
            foreach (var row in InternalTable)
            {
                List<string> column = row.Select(vfield => vfield.Value.Replace("\"","\\\"")).ToList();
                string wynik = "new [] {" + String.Join(",", column.Select(s => "\"" + s + "\"")) + "}";
                rowsString.Add(wynik);
            }
            
            begin +=String.Join(",",rowsString) +  " };";
            return begin;
        }

        public string GetToHeaderAssertHelper()
        {

            return "new string[] {" + String.Join(",", Columns.Select(s => "\"" + s + "\"")) + "}" ;
        }

        public int PobierzIloscWierszy()
        {
            return Rows.Count();
        }
    }

}
