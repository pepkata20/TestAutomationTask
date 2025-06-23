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
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        // XPath locators
        private readonly By appLogo = By.XPath("//div[@class='app_logo']");

        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
        }

        public string GetAppLogoText()
        {
            try
            {
                var logoElement = wait.Until(d => d.FindElement(appLogo));
                return logoElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }
    }
}
