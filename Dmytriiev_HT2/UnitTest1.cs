using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;
using OpenQA.Selenium.Support.UI;

namespace Dmytriiev_HT2
{
    public class Tests
    {
        ChromeDriver chromeDriver = new ChromeDriver();

        [SetUp]
        public void Setup()
        {
            chromeDriver.Navigate().GoToUrl("https://avic.ua/ua");
        }

        [Test]
        public void Test1() //If header exists
        {
            IWebElement header = chromeDriver.FindElement(By.ClassName("header"));
            if (header != null )
            {
                Assert.Pass();
            }
        }
        [Test]
        public void Test2() //Is link navigates to right page
        {
            chromeDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
            string result = chromeDriver.FindElement(By.XPath("//a[contains(text(), 'Оплата і доставка')]")).GetAttribute("href");
            Assert.That(result, Is.EqualTo("https://avic.ua/ua/pokupka-i-dostavka-tovarov"));
        }
        [Test]
        public void Test3() //Check if logo changes in mobile mode
        {
            IWebElement desktopLogo, mobileLogo;
            bool result1, result2;

            bool Check (bool nonDisplayableVersion, bool displayableVersion)
            {
                if (nonDisplayableVersion == false && displayableVersion == true)
                {
                    return true;
                }
                return false;
            }
         
            chromeDriver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
            desktopLogo = chromeDriver.FindElement(By.XPath("//div[@class = 'header-bottom__logo']"));
            mobileLogo = chromeDriver.FindElement(By.XPath("//div[@class = 'mobile-icon']"));

            result1 = Check(mobileLogo.Displayed, desktopLogo.Displayed);

            chromeDriver.Manage().Window.Size = new System.Drawing.Size(1100, 1080);

            result2 = Check(desktopLogo.Displayed, mobileLogo.Displayed);
            Assert.IsTrue(result1 && result2);
        }
        [TestCleanup]
        public void CloseBrowser()
        {
            chromeDriver.Quit();
        }
    }
}