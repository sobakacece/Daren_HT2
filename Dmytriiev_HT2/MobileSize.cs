using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V102.SystemInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmytriiev_HT2
{
    internal class MobileSize : WebPage
    {
        public MobileSize(ChromeDriver browser) : base(browser)
        {

        }

        public override string MyUrl => "https://avic.ua/ua";
        protected IWebElement mobileLogo => WaitAndFindElement(By.XPath("//div[@class = 'mobile-icon']"));
        protected IWebElement desktopLogo => WaitAndFindElement(By.XPath("//div[@class = 'header-bottom__logo']"));
        public bool MyDesktopResult { get; set; }
        public bool MyMobileResult { get; set; }


        //public bool OneIsVisibleAndOtherNot(IWebElement VisibleElement, IWebElement InvisibleElement)
        //{
        //    return VisibleElement.Displayed && !InvisibleElement.Displayed;
        //}
        public void ChangeWindowSize(int width, int height)
        {
            MyDriver.Manage().Window.Size = new System.Drawing.Size(width, height);
        }
        public void DesktopLogoCheck()
        {
            MyDesktopResult = desktopLogo.Displayed && !mobileLogo.Displayed ? true : false;
        }
        public void MobileLogoCheck()
        {
            MyMobileResult = !desktopLogo.Displayed && mobileLogo.Displayed ? true : false;
        }
    }
}
