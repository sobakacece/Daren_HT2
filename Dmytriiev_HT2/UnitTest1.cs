using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using PageFactoryCore;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Internal;

namespace Dmytriiev_HT2
{
    public class Tests
    {
        ChromeDriver chromeDriver = new ChromeDriver();
        [SetUp]
        public void Setup()
        {
            //chromeDriver.Navigate().GoToUrl("https://avic.ua/ua");
        }

        [Test]
        public void Test1() //If header exists
        {
            Header header = new Header(chromeDriver);
            header.GoTo();
            try
            {
                header.WaitAndFindElement(By.ClassName("header"));
            }
            catch (NoSuchElementException)
            {
                Assert.Fail();
            }
            finally
            {
                Assert.Pass();
            }
            header.CloseBrowser();
           
        }
        [Test]
        public void Test2() //Is link navigates to right page
        {
            DeliveryLink linkPage = new DeliveryLink(chromeDriver);
            linkPage.GoTo();
            Assert.That(linkPage.Expected, Is.EqualTo(linkPage.GetLink()));
            linkPage.CloseBrowser();
        }
        [Test]
        public void Test3() //Check if logo changes in mobile mode
        {
            MobileSize browser = new MobileSize(chromeDriver);
            browser.GoTo();

            browser.ChangeWindowSize(1920, 1080);
            browser.DesktopLogoCheck();

            browser.ChangeWindowSize(1200, 1080);
            browser.MobileLogoCheck();

            Assert.IsTrue(browser.MyMobileResult && browser.MyDesktopResult);
            browser.CloseBrowser();
        }
        [TestCleanup]
        //public void CloseBrowser()
        //{
        //    chromeDriver.Quit();
        //}
    }
    public abstract class WebPage
    {
        protected IWebDriver MyDriver { get; }
        protected WebDriverWait WebDriverWait { get; }
        public abstract string MyUrl { get;  }

        private const int timeout = 30;
        
        public WebPage(ChromeDriver browser)
        {
            this.MyDriver = browser;
            WebDriverWait = new WebDriverWait(browser, TimeSpan.FromSeconds(timeout));
        }
        public void GoTo()
        {
            MyDriver.Navigate().GoToUrl(MyUrl);            
        }
        public virtual IWebElement WaitAndFindElement(By locator)
        {
          return  WebDriverWait.Until(chromeDriver => chromeDriver.FindElement(locator));
        }
        public void CloseBrowser()
        {
            MyDriver.Quit();
        }

    }
    public class Header : WebPage
    {
        public Header(ChromeDriver browser) : base(browser)
        {
            
        }
        public override string MyUrl
        {
            get
            {
                return "https://avic.ua/ua";
            }
        }
        

    }
    public class DeliveryLink : WebPage
    {
        public DeliveryLink(ChromeDriver browser) : base(browser)
        {

        }
        public string Expected
        {
            get
            {
                return "https://avic.ua/ua/pokupka-i-dostavka-tovarov";
            }
        }

        public override string MyUrl
        {
            get
            {
                return "https://avic.ua/ua";
            }
        }
        public string GetLink()
        {
            return WaitAndFindElement(By.XPath("//a[contains(text(), 'Оплата і доставка')]")).GetAttribute("href");
        }


    }




}