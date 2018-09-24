using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using PocztaInteriaPage;
using Selenium;
using Vulcan.Common2015;
using Vulcan.Common2015.SeleniumLib;


namespace PocztaInteriaSeleniumTest
{
  

public class PocztaInteriaSeleniumTest:BaseTest

{
    protected override void OpenBrowser()
    {
        MainPage = new StronaLogowania(Url);
    }

    private StronaLogowania InteriaLogin
    {
        get { return (StronaLogowania)MainPage; }
    }

    [SetUp]
    public void SetupTest()
    {
        var stronaLogowania = InteriaLogin;
        stronaLogowania.Zaloguj(Settings.uzytkownik, new FunkcjeSQL().PobierzDane(ZapytaniaSQL.pobierzHaslo));
      
    }

[TearDown]

    public void TeardownTest()
{
    var stronaglowna = new PocztaStronaGlowna(this.InteriaLogin.openWeb());
    stronaglowna.Wyloguj();
    Wyloguj();

}

[Test, TestName("wysylanie maila")]
public void PocztaInteriaTest()
{   
    var stronaGlowna = new PocztaStronaGlowna(this.InteriaLogin.openWeb());
    var wiadomosc = stronaGlowna.NowaWiadomosc();
    stronaGlowna=wiadomosc.WyslijMaila(new FunkcjeSQL().PobierzDane(ZapytaniaSQL.pobierzAdresata));
    Assert.AreEqual("Ja", stronaGlowna.SprawdzWyslanaWiadomosc(1));
    Assert.AreEqual("test", stronaGlowna.SprawdzWyslanaWiadomosc(2));
    stronaGlowna.UsunWiadomosc(1);
}
    
[Test, TestName("wysylanie maila")]  
    public void ZakladkiTest()
{
    var stronaGlowna = new PocztaStronaGlowna(this.InteriaLogin.openWeb());
    var kosz = stronaGlowna.PrzejdzDoKosza();
    kosz.OproznijKosz();
    Assert.AreEqual("Ten folder jest pusty",kosz.SprawdzKosz());

}
}
}



