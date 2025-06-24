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
        public static IEnumerable<object[]> ValidLoginData =>
            TestConfig.ValidUsernames.Select(username => new object[] { username, TestConfig.ValidPassword });

        protected LoginTestsBase(ITestOutputHelper output, string browser)
        {
            this.output = output;
            this.browser = browser;
            driver = WebDriverFactory.Instance.GetDriver(this.browser);
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
        }

        [Theory]
        [InlineData("", "", TestConfig.UsernameRequiredError, "Empty credentials")]
        [InlineData("test_user", "", TestConfig.PasswordRequiredError, "Username only")]
        [InlineData("locked_out_user", TestConfig.ValidPassword, TestConfig.LockedUserError, "Locked user")]
        public void TestLogin(string username, string password, string expectedError, string scenario)
        {
            output.WriteLine($"[{browser}] Starting: {scenario}");
            output.WriteLine($"[{browser}] Running on thread: {Environment.CurrentManagedThreadId}");

            // Navigate to login page
            loginPage.NavigateToLoginPage();
            output.WriteLine("Navigated to SauceDemo login page");

            // Type any credentials
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            output.WriteLine("Entered test credentials");

            // Clear the inputs
            if (username == "")
            {
                loginPage.ClearUsername();
            }
            if (password == "")
            {
                loginPage.ClearPassword();
            }

            // Click Login button
            loginPage.ClickLoginButton();
            output.WriteLine("Clicked Login button");

            // Verify error message
            var errorMessage = loginPage.GetErrorMessage();
            output.WriteLine($"Error message displayed: {errorMessage}");

            errorMessage.Should().Be(expectedError  );
            output.WriteLine($"{scenario} completed successfully");
        }

        [Theory]
        [MemberData(nameof(ValidLoginData))]
        public void TestLoginWithValidCredentials(string username, string password)
        {
            output.WriteLine($"[{browser}] Starting UC-3: Test Login form with valid credentials");
            output.WriteLine($"[{browser}] Running on thread: {Environment.CurrentManagedThreadId}");

            // Navigate to login page
            loginPage.NavigateToLoginPage();
            output.WriteLine("Navigated to SauceDemo login page");

            // Login with valid credentials
            loginPage.Login(username, password);
            output.WriteLine($"Logged in with username: {username} and password: {password}");

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
