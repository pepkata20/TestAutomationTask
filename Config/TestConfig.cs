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
        public static string[] ValidUsernames =
        {
            "standard_user",
            "problem_user",
            "performance_glitch_user",
            "error_user",
            "visual_user"
        };

        // Error messages
        public const string UsernameRequiredError = "Epic sadface: Username is required";
        public const string PasswordRequiredError = "Epic sadface: Password is required";
        public const string LockedUserError = "Epic sadface: Sorry, this user has been locked out.";

        // Expected page title after successful login
        public const string ExpectedDashboardTitle = "Swag Labs";

        // Browser types for parallel execution
        public static readonly string[] BrowserTypes = ["Chrome", "Firefox", "Edge"];
    }
}
