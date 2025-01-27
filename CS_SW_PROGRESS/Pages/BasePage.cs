using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CS_SW_PROGRESS.Pages
{
    public abstract class BasePage(IWebDriver driver)
    {
        protected IWebDriver Driver = driver;

        protected void ClickElement(By locator)
        {
            WaitForElement(locator);
            Driver.FindElement(locator).Click();
        }

        protected void EnterText(By locator, string text)
        {
            WaitForElement(locator);
            Driver.FindElement(locator).Clear();
            Driver.FindElement(locator).SendKeys(text);
        }

        protected string GetText(By locator)
        {
            WaitForElement(locator);
            return Driver.FindElement(locator).Text;
        }

        protected void WaitForElement(By locator, int timeout = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            wait.Until(drv => drv.FindElement(locator).Displayed);
        }

        protected bool IsElementDisplayed(By locator)
        {
            try
            {
                return Driver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}