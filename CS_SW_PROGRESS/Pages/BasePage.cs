using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CS_SW_PROGRESS.Pages
{
    public abstract class BasePage(IWebDriver driver)
    {
        protected IWebDriver Driver = driver;

        protected void ClickElement(By locator)
        {
            Driver.FindElement(locator).Click();
        }

        protected void EnterText(By locator, string text)
        {
            Driver.FindElement(locator).SendKeys(text);
        }

        protected string GetText(By locator)
        {
            return Driver.FindElement(locator).Text;
        }

        protected bool IsElementDisplayed(By locator)
        {
            return Driver.FindElement(locator).Displayed;
        }

        protected void RefreshPage()
        {
            Driver.Navigate().Refresh();
        }

        protected void WaitForElementVisible(By locator, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(driver => driver.FindElement(locator).Displayed);
        }
    }
}
