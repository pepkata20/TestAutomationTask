using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationTask.PageObjects
{
    public class DashboardPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // XPath locators
        private readonly By _appLogo = By.XPath("//div[@class='app_logo']");

        public DashboardPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public string GetAppLogoText()
        {
            try
            {
                var logoElement = _wait.Until(d => d.FindElement(_appLogo));
                return logoElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }
    }
}
