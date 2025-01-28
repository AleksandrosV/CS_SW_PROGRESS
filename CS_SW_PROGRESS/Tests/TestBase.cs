using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CS_SW_PROGRESS.Tests
{
    public class TestBase
    {
        protected IWebDriver Driver;
        protected const string ContactPageUrl = "https://www.progress.com/company/contact";

        [SetUp]
        public void SetUp()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized"); // Open browser in maximized mode
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.cookies", 2); // Block cookies
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