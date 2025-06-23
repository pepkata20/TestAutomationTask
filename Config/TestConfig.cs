using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationTask.Config
{
    public static class TestConfig
    {
        public const string BaseUrl = "https://www.saucedemo.com/";
        public const string ValidPassword = "secret_sauce";
        public const string ValidUsername = "standard_user";

        // Error messages
        public const string UsernameRequiredError = "Epic sadface: Username is required";
        public const string PasswordRequiredError = "Epic sadface: Password is required";

        // Expected page title after successful login
        public const string ExpectedDashboardTitle = "Swag Labs";

        // Browser types for parallel execution
        public static readonly string[] BrowserTypes = ["Chrome", "Firefox", "Edge"];
    }
}
