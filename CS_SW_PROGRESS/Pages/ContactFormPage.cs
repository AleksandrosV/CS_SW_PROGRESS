using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CS_SW_PROGRESS.Pages
{
    public class ContactFormPage(IWebDriver driver) : BasePage(driver)
    {
        private By productDropdown = By.Id("Dropdown-1");
        private By businessEmailField = By.Id("Email-1");
        private By firstNameField = By.Id("Textbox-1");
        private By lastNameField = By.Id("Textbox-2");
        private By companyField = By.Id("Textbox-3");
        private By iAmDropdown = By.Id("Dropdown-2");
        private By countryDropdown = By.Id("Country-1");
        private By phoneField = By.Id("Textbox-5");
        private By stateDropdown = By.Id("State-1");
        private By messageField = By.Id("Textarea-1");
        private By contactSalesBtn = By.CssSelector("button[type='submit']");
        private readonly By headerText = By.CssSelector("h1.-mb2.-tac");

        public string GetHeaderText()
        {
            return GetText(headerText);
        }

        public string GetLabelText(string forAttribute)
        {
            var labelLocator = By.CssSelector($"label[for='{forAttribute}']");
            return GetText(labelLocator);
        }

        public bool IsLabelRequired(string forAttribute)
        {
            var labelLocator = By.CssSelector($"label[for='{forAttribute}']");
            var labelElement = Driver.FindElement(labelLocator);
            return labelElement.GetAttribute("class").Contains("required");
        }

        public void ClickContactSalesBtn()
        {
            ClickElement(contactSalesBtn);
        }

        public string GetErrorMessage(string forAttribute)
        {
            var errorMessageLocator = By.XPath($"//p[@data-sf-role='error-message' and text()='{forAttribute}']");
            return GetText(errorMessageLocator);
        }

        public string GetSelectedDropdownValue(By dropdownLocator)
        {
            var dropdownElement = Driver.FindElement(dropdownLocator);
            var selectElement = new SelectElement(dropdownElement);
            return selectElement.SelectedOption.Text;
        }

        public List<string> GetDropdownOptions(By dropdownLocator)
        {
            var dropdownElement = Driver.FindElement(dropdownLocator);
            var selectElement = new SelectElement(dropdownElement);
            return selectElement.Options.Select(option => option.Text).ToList();
        }

        public void SelectDropdownValue(By dropdownLocator, string value)
        {
            var dropdownElement = Driver.FindElement(dropdownLocator);
            var selectElement = new SelectElement(dropdownElement);
            selectElement.SelectByText(value);
        }

        public void SelectCountry(string country)
        {
            SelectDropdownValue(countryDropdown, country);
        }

        public bool IsStateDropdownDisplayed()
        {
            return IsElementDisplayed(stateDropdown);
        }

        public string GetPhoneNumberCode()
        {
            return Driver.FindElement(phoneField).GetAttribute("value");
        }

        public void SelectRandomCountry()
        {
            var countryOptions = GetDropdownOptions(countryDropdown);
            Random random = new();
            string randomCountry = countryOptions[random.Next(countryOptions.Count)];
            SelectDropdownValue(countryDropdown, randomCountry);
        }

        public void ClickDisclaimerLink(string linkText)
        {
            By linkLocator = By.LinkText(linkText);
            ClickElement(linkLocator);
        }
    }
}