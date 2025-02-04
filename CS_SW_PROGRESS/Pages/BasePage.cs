﻿using OpenQA.Selenium;
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

        protected List<string> GetDropdownOptions(By dropdownLocator)
        {
            var dropdownElement = Driver.FindElement(dropdownLocator);
            var selectElement = new SelectElement(dropdownElement);
            return selectElement.Options.Select(option => option.Text).ToList();
        }

        protected string GetDefaultDropdownOption(By dropdownLocator)
        {
            var selectElement = new SelectElement(Driver.FindElement(dropdownLocator));
            return selectElement.SelectedOption.Text;
        }

        protected void SelectDropdownValue(By dropdownLocator, string value)
        {
            var selectElement = new SelectElement(Driver.FindElement(dropdownLocator));
            selectElement.SelectByText(value);
        }

        protected void SelectRandomDropdownValue(By dropdownLocator, int startIndex = 1)
        {
            var options = GetDropdownOptions(dropdownLocator);
            if (options.Count > startIndex)
            {
                Random random = new();
                string randomValue = options[random.Next(startIndex, options.Count)];
                SelectDropdownValue(dropdownLocator, randomValue);
            }
        }
    }
}