using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationTask.Drivers
{
    public class WebDriverFactory
    {
        private static WebDriverFactory? _instance;
        private static readonly object _lock = new object();
        private readonly ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();

        private WebDriverFactory() { }

        public static WebDriverFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new WebDriverFactory();
                    }
                }
                return _instance;
            }
        }

        public IWebDriver GetDriver(string browserType = "Chrome")
        {
            if (_driver.Value == null)
            {
                _driver.Value = CreateDriver(browserType);
            }
            return _driver.Value;
        }

        private static IWebDriver CreateDriver(string browserType)
        {
            return browserType.ToLower() switch
            {
                "chrome" => CreateChromeDriver(),
                "firefox" => CreateFirefoxDriver(),
                _ => throw new ArgumentException($"Browser type '{browserType}' is not supported")
            };
        }

        private static IWebDriver CreateChromeDriver()
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

        private static IWebDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();
            options.AddArguments("--start-maximized");

            var driver = new FirefoxDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }

        public void QuitDriver()
        {
            if (_driver.Value != null)
            {
                _driver.Value.Quit();
                _driver.Value = null;
            }
        }

        public void DisposeAll()
        {
            if (_driver.Value != null)
            {
                _driver.Value.Quit();
                _driver.Dispose();
            }
        }
    }
}
