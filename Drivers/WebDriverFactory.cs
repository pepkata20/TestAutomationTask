using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationTask.Drivers
{
    public class WebDriverFactory
    {
        private static WebDriverFactory? instance;
        private static readonly object lockObject = new();
        private readonly ThreadLocal<IWebDriver> driver = new();

        private WebDriverFactory() { }

        public static WebDriverFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        instance ??= new WebDriverFactory();
                    }
                }
                return instance;
            }
        }

        public IWebDriver GetDriver(string browserType)
        {
            driver.Value ??= CreateDriver(browserType);
            return driver.Value;
        }

        private static IWebDriver CreateDriver(string browserType)
        {
            return browserType.ToLower() switch
            {
                "chrome" => CreateChromeDriver(),
                "firefox" => CreateFirefoxDriver(),
                "edge" => CreateEdgeDriver(),
                _ => throw new ArgumentException($"Browser type '{browserType}' is not supported")
            };
        }

        private static ChromeDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("--start-maximized");
            options.AddArguments("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);

            var driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }

        private static FirefoxDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();
            options.AddArguments("--start-maximized");

            var driver = new FirefoxDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }

        private static EdgeDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");

            var driver = new EdgeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }

        public void QuitDriver()
        {
            if (driver.Value != null)
            {
                driver.Value.Quit();
                driver.Value = null!; // Use null-forgiving operator to suppress CS8625
            }
        }
    }
}
