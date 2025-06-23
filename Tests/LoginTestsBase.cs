using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Communication;
using TestAutomationTask.Config;
using TestAutomationTask.Drivers;
using TestAutomationTask.PageObjects;
using Xunit.Abstractions;

namespace TestAutomationTask.Tests
{
    public abstract class LoginTestsBase : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly LoginPage loginPage;
        private readonly DashboardPage dashboardPage;
        private readonly ITestOutputHelper output;
        private bool disposed = false;
        private readonly string browser;

        protected LoginTestsBase(ITestOutputHelper output, string browser)
        {
            this.output = output;
            this.browser = browser;
            driver = WebDriverFactory.Instance.GetDriver(this.browser);
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
        }

        [Fact]
        public void UC1_TestLoginWithEmptyCredentials()
        {
            output.WriteLine($"[{browser}] Starting UC-1: Test Login form with empty credentials");
            output.WriteLine($"[{browser}] Running on thread: {Environment.CurrentManagedThreadId}");

            // Navigate to login page
            loginPage.NavigateToLoginPage();
            output.WriteLine("Navigated to SauceDemo login page");

            // Type any credentials
            loginPage.EnterUsername("test_user");
            loginPage.EnterPassword("test_password");
            output.WriteLine("Entered test credentials");

            // Clear the inputs
            loginPage.ClearUsername();
            loginPage.ClearPassword();
            output.WriteLine("Cleared both username and password fields");

            // Click Login button
            loginPage.ClickLoginButton();
            output.WriteLine("Clicked Login button");

            // Verify error message
            var errorMessage = loginPage.GetErrorMessage();
            output.WriteLine($"Error message displayed: {errorMessage}");

            errorMessage.Should().Be(TestConfig.UsernameRequiredError);
            output.WriteLine("UC-1 test completed successfully");
        }

        [Fact]
        public void UC2_TestLoginWithUsernameOnly()
        {
            output.WriteLine($"[{browser}] Starting UC-2: Test Login form with username only");
            output.WriteLine($"[{browser}] Running on thread: {Environment.CurrentManagedThreadId}");

            // Navigate to login page
            loginPage.NavigateToLoginPage();
            output.WriteLine("Navigated to SauceDemo login page");

            // Enter username
            loginPage.EnterUsername("standard_user");
            output.WriteLine("Entered username: standard_user");

            // Enter password
            loginPage.EnterPassword("test_password");
            output.WriteLine("Entered test password");

            // Clear password field
            loginPage.ClearPassword();
            output.WriteLine("Cleared password field");

            // Click Login button
            loginPage.ClickLoginButton();
            output.WriteLine("Clicked Login button");

            // Verify error message
            var errorMessage = loginPage.GetErrorMessage();
            output.WriteLine($"Error message displayed: {errorMessage}");

            errorMessage.Should().Be(TestConfig.PasswordRequiredError);
            output.WriteLine("UC-2 test completed successfully");
        }

        [Fact]
        public void UC3_TestLoginWithValidCredentials()
        {
            output.WriteLine($"[{browser}] Starting UC-3: Test Login form with valid credentials");
            output.WriteLine($"[{browser}] Running on thread: {Environment.CurrentManagedThreadId}");

            // Navigate to login page
            loginPage.NavigateToLoginPage();
            output.WriteLine("Navigated to SauceDemo login page");

            // Login with valid credentials
            loginPage.Login(TestConfig.ValidUsername, TestConfig.ValidPassword);
            output.WriteLine($"Logged in with username: standard_user: {TestConfig.ValidUsername} and password: {TestConfig.ValidPassword}");

            // Verify app logo shows "Swag Labs"
            var appLogoText = dashboardPage.GetAppLogoText();
            output.WriteLine($"App logo text: {appLogoText}");

            appLogoText.Should().Be(TestConfig.ExpectedDashboardTitle);
            output.WriteLine("UC-3 test completed successfully");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                output.WriteLine("Disposing WebDriver");
                WebDriverFactory.Instance.QuitDriver();
            }

            disposed = true;
        }
    }
}
