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
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // XPath locators
        private readonly By _usernameField = By.XPath("//input[@id='user-name']");
        private readonly By _passwordField = By.XPath("//input[@id='password']");
        private readonly By _loginButton = By.XPath("//input[@id='login-button']");
        private readonly By _errorMessage = By.XPath("//h3[@data-test='error']");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToLoginPage()
        {
            _driver.Navigate().GoToUrl(TestConfig.BaseUrl);
        }

        public void EnterUsername(string username)
        {
            var usernameElement = _wait.Until(d => d.FindElement(_usernameField));
            usernameElement.Clear();
            usernameElement.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            var passwordElement = _wait.Until(d => d.FindElement(_passwordField));
            passwordElement.Clear();
            passwordElement.SendKeys(password);
        }

        public void ClearUsername()
        {
            var usernameElement = _wait.Until(d => d.FindElement(_usernameField));
            ClearFieldReliably(usernameElement);
        }

        public void ClearPassword()
        {
            var passwordElement = _wait.Until(d => d.FindElement(_passwordField));
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
            var loginButtonElement = _wait.Until(d => d.FindElement(_loginButton));
            loginButtonElement.Click();
        }

        public string GetErrorMessage()
        {
            try
            {
                var errorElement = _wait.Until(d => d.FindElement(_errorMessage));
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
                var errorElement = _wait.Until(d => d.FindElement(_errorMessage));
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
