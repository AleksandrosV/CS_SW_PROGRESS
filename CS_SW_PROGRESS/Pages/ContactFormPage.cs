using Bogus;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CS_SW_PROGRESS.Pages
{
    public class ContactFormPage(IWebDriver driver) : BasePage(driver)
    {
        private readonly By productDropdown = By.Id("Dropdown-1");
        private readonly By businessEmailField = By.Id("Email-1");
        private readonly By firstNameField = By.Id("Textbox-1");
        private readonly By lastNameField = By.Id("Textbox-2");
        private readonly By companyField = By.Id("Textbox-3");
        private readonly By iAmDropdown = By.Id("Dropdown-2");
        private readonly By countryDropdown = By.Id("Country-1");
        private readonly By phoneField = By.Id("Textbox-5");
        private readonly By stateDropdown = By.Id("State-1");
        private readonly By messageField = By.Id("Textarea-1");
        private readonly By contactSalesBtn = By.CssSelector("button[type='submit']");
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
            string randomCountry = countryOptions[random.Next(1, countryOptions.Count)];
            SelectDropdownValue(countryDropdown, randomCountry);
        }

        public void SelectRandomProduct()
        {
            var productOptions = GetDropdownOptions(productDropdown);
            Random random = new();
            string randomProduct = productOptions[random.Next(1, Math.Min(7, productOptions.Count))];
            SelectDropdownValue(productDropdown, randomProduct);
        }

        public void SelectRandomCompanyType()
        {
            var companyTypeOptions = GetDropdownOptions(iAmDropdown);
            Random random = new();
            string randomCompanyType = companyTypeOptions[random.Next(1, companyTypeOptions.Count)];
            SelectDropdownValue(iAmDropdown, randomCompanyType);
        }

        public void ClickDisclaimerLink(string linkText)
        {
            By linkLocator = By.LinkText(linkText);
            ClickElement(linkLocator);
        }

        public void SubmitContactForm(string firstName, string lastName, string email, string company, string phone, string message)
        {
            EnterText(firstNameField, firstName);
            EnterText(lastNameField, lastName);
            EnterText(businessEmailField, email);
            EnterText(companyField, company);
            EnterText(phoneField, phone);
            EnterText(messageField, message);
            SelectRandomCompanyType();
            SelectRandomCountry();
            SelectRandomProduct();
            ClickContactSalesBtn();
        }
    }
}