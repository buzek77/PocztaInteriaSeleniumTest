using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace Vulcan.Common2015.SeleniumLib.Pages.Common
{
    public class VModelSection : Page
    {
        List<VField> fields = new List<VField>();
        private IWebElement context;

        public VModelSection(IWebDriver webDriver,int index = 0)
            : base(webDriver)
        {
            var vmodels = GetByContainsIdAll("vmodelsection","div");

            if(vmodels.Count == 0) throw new Exception("Brak VMODELSECTION");
            
            context = vmodels[index];
            ParseVModel(context);
        }

        private void ParseVModel(IWebElement vmodel)
        {
            var parseVModel = HtmlTable.GetArrayFromTr(vmodel).Where(s => s != "Zmień" && s != "").ToArray();
            for (int i = 0; i < parseVModel.Count(); i += 2)
            {
                fields.Add(new VField()
                               {
                                   Label = parseVModel[i],
                                   Value = parseVModel[i + 1]
                               });
            }
        }

        public VEditor Zmien()
        {
            GetByText("Zmień", context).Click();
            return new VEditor(WebDriver);
        }

        public string GetValue(string label)
        {
            return fields.First(s => s.Label == label).Value;
        }

        public override string ToString()
        {
            return fields.Aggregate("", (current, vField) => current + (vField.ToString() + "\n"));
        }

        public string ToStringPattern()
        {
            return fields.Aggregate("", (current, s) => current + ("\"" + s + "\","));
        }


        public string[] ToArray()
        {
            return fields.Select(vField => vField.ToString()).ToArray();
        }
    }
}
