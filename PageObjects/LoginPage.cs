using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationTask.Config;

namespace TestAutomationTask.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        // XPath locators
        private readonly By usernameField = By.XPath("//input[@id='user-name']");
        private readonly By passwordField = By.XPath("//input[@id='password']");
        private readonly By loginButton = By.XPath("//input[@id='login-button']");
        private readonly By errorMessage = By.XPath("//h3[@data-test='error']");

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToLoginPage()
        {
            driver.Navigate().GoToUrl(TestConfig.BaseUrl);
        }

        public void EnterUsername(string username)
        {
            var usernameElement = wait.Until(d => d.FindElement(usernameField));
            usernameElement.Clear();
            usernameElement.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            var passwordElement = wait.Until(d => d.FindElement(passwordField));
            passwordElement.Clear();
            passwordElement.SendKeys(password);
        }

        public void ClearUsername()
        {
            var usernameElement = wait.Until(d => d.FindElement(usernameField));
            ClearFieldReliably(usernameElement);
        }

        public void ClearPassword()
        {
            var passwordElement = wait.Until(d => d.FindElement(passwordField));
            ClearFieldReliably(passwordElement);
        }

        private static void ClearFieldReliably(IWebElement element)
        {
            element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }

        public void ClickLoginButton()
        {
            var loginButtonElement = wait.Until(d => d.FindElement(loginButton));
            loginButtonElement.Click();
        }

        public string GetErrorMessage()
        {
            try
            {
                var errorElement = wait.Until(d => d.FindElement(errorMessage));
                return errorElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }

        public bool IsErrorMessageDisplayed()
        {
            try
            {
                var errorElement = wait.Until(d => d.FindElement(errorMessage));
                return errorElement.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }
    }
}
