using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CS_SW_PROGRESS.Pages
{
    public class TestBase
    {
        protected IWebDriver Driver;
        protected const string ContactPageUrl = "https://www.progress.com/company/contact";

        [SetUp]
        public void SetUp()
        {
            // Initialize ChromeDriver with default options
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized"); // Open browser in maximized mode
            chromeOptions.AddArgument("--disable-extensions"); // Disable browser extensions
            chromeOptions.AddArgument("--disable-popup-blocking"); // Disable popups
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled"); // Disable automation control (cookie banner)

            Driver = new ChromeDriver(chromeOptions);

            // Set an implicit wait for all elements
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
            }
        }
    }
}
