using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace Vulcan.Common2015.SeleniumLib.Pages.Common
{
    public class VSwitchPanel : Page
    {
        protected IWebElement Context;
        protected VGrid LeftGrid { get; set; }
        protected VGrid RightGrid { get; set; }

        public VSwitchPanel( IWebDriver webDriver, string id, bool loadOnStart = true, bool loadNotVisible = true, bool slowLoad = false )
            : base(webDriver)
        {
            Context = WebDriver.FindElement( By.Id( id ) );
            LeftGrid = new VGrid( Context.FindElements( By.XPath( ".//div[contains(@class,'vgrid')]" ) )[0], webDriver, loadOnStart, loadNotVisible, slowLoad );
            RightGrid = new VGrid( Context.FindElements( By.XPath( ".//div[contains(@class,'vgrid')]" ) )[1], webDriver, loadOnStart, loadNotVisible, slowLoad );
        }

        public VSwitchPanel( IWebDriver webDriver, IWebElement vSwitcPanelWebElement, bool loadOnStart = true, bool loadNotVisible = true, bool slowLoad = false )
            : base( webDriver )
        {
            Context = vSwitcPanelWebElement;
            LeftGrid = new VGrid( Context.FindElements( By.XPath( ".//div[contains(@class,'vgrid')]" ) )[0], WebDriver, loadOnStart, loadNotVisible, slowLoad );
            RightGrid = new VGrid( Context.FindElements( By.XPath( ".//div[contains(@class,'vgrid')]" ) )[1], WebDriver, loadOnStart, loadNotVisible, slowLoad );
        }

        public void WybierzZLewegoGrida( string text )
        {
            var tr = LeftGrid.GetTrForGrid( "'" + text + "'" );
            tr.Click( );
            KliknijStrzalke( );
        }

        public void WybierzZPrawegoGrida( string text )
        {
            var tr = RightGrid.GetTrForGrid( "'" + text + "'" );
            tr.Click( );
            KliknijStrzalke( );
        }

        public void KliknijStrzalke( )
        {
            var strzalkaButton = Context.FindElement( By.XPath( string.Format( ".//*[text() = '>' or text() = '<']") ) );
            strzalkaButton.Click( );
        }
    }
}
