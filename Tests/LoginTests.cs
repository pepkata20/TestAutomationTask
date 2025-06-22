using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationTask.Config;
using TestAutomationTask.Drivers;
using TestAutomationTask.PageObjects;
using Xunit.Abstractions;

namespace TestAutomationTask.Tests
{
    public class LoginTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;
        private readonly DashboardPage _dashboardPage;
        private readonly ITestOutputHelper _output;

        public LoginTests(ITestOutputHelper output)
        {
            _output = output;
            _driver = WebDriverFactory.Instance.GetDriver("Chrome");
            _loginPage = new LoginPage(_driver);
            _dashboardPage = new DashboardPage(_driver);
        }

        [Fact]
        public void UC1_TestLoginWithEmptyCredentials()
        {
            _output.WriteLine("Starting UC-1: Test Login form with empty credentials");

            // Navigate to login page
            _loginPage.NavigateToLoginPage();
            _output.WriteLine("Navigated to SauceDemo login page");

            // Type any credentials
            _loginPage.EnterUsername("test_user");
            _loginPage.EnterPassword("test_password");
            _output.WriteLine("Entered test credentials");

            // Clear the inputs
            _loginPage.ClearUsername();
            _loginPage.ClearPassword();
            _output.WriteLine("Cleared both username and password fields");

            // Click Login button
            _loginPage.ClickLoginButton();
            _output.WriteLine("Clicked Login button");

            // Verify error message
            var errorMessage = _loginPage.GetErrorMessage();
            _output.WriteLine($"Error message displayed: {errorMessage}");

            errorMessage.Should().Be(TestConfig.UsernameRequiredError);
            _output.WriteLine("UC-1 test completed successfully");
        }

        [Fact]
        public void UC2_TestLoginWithUsernameOnly()
        {
            _output.WriteLine("Starting UC-2: Test Login form with username only");

            // Navigate to login page
            _loginPage.NavigateToLoginPage();
            _output.WriteLine("Navigated to SauceDemo login page");

            // Enter username
            _loginPage.EnterUsername("standard_user");
            _output.WriteLine("Entered username: standard_user");

            // Enter password
            _loginPage.EnterPassword("test_password");
            _output.WriteLine("Entered test password");

            // Clear password field
            _loginPage.ClearPassword();
            _output.WriteLine("Cleared password field");

            // Click Login button
            _loginPage.ClickLoginButton();
            _output.WriteLine("Clicked Login button");

            // Verify error message
            var errorMessage = _loginPage.GetErrorMessage();
            _output.WriteLine($"Error message displayed: {errorMessage}");

            errorMessage.Should().Be(TestConfig.PasswordRequiredError);
            _output.WriteLine("UC-2 test completed successfully");
        }

        [Fact]
        public void UC3_TestLoginWithValidCredentials()
        {
            _output.WriteLine("Starting UC-3: Test Login form with valid credentials");

            // Navigate to login page
            _loginPage.NavigateToLoginPage();
            _output.WriteLine("Navigated to SauceDemo login page");

            // Login with valid credentials
            _loginPage.Login(TestConfig.ValidUsername, TestConfig.ValidPassword);
            _output.WriteLine($"Logged in with username: standard_user: {TestConfig.ValidUsername} and password: {TestConfig.ValidPassword}");

            // Verify app logo shows "Swag Labs"
            var appLogoText = _dashboardPage.GetAppLogoText();
            _output.WriteLine($"App logo text: {appLogoText}");

            appLogoText.Should().Be(TestConfig.ExpectedDashboardTitle);
            _output.WriteLine("UC-3 test completed successfully");
        }

        public void Dispose()
        {
            _output.WriteLine("Disposing WebDriver");
            WebDriverFactory.Instance.QuitDriver();
        }
    }
}
