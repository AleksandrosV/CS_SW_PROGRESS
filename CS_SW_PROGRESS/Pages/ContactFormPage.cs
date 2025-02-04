using CS_SW_PROGRESS.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CS_SW_PROGRESS.Pages
{
    public class ContactFormPage(IWebDriver driver) : BasePage(driver)
    {
        private readonly By ProductDropdown = By.Id("Dropdown-1");
        private readonly By BusinessEmailField = By.Id("Email-1");
        private readonly By FirstNameField = By.Id("Textbox-1");
        private readonly By LastNameField = By.Id("Textbox-2");
        private readonly By CompanyField = By.Id("Textbox-3");
        private readonly By IAmDropdown = By.Id("Dropdown-2");
        private readonly By CountryDropdown = By.Id("Country-1");
        private readonly By PhoneField = By.Id("Textbox-5");
        private readonly By StateDropdown = By.Id("State-1");
        private readonly By MessageField = By.Id("Textarea-1");
        private readonly By ContactSalesBtn = By.CssSelector("button[type='submit']");
        private readonly By ContactHeaderText = By.CssSelector("h1.-mb2.-tac");
        private readonly By IndustryDropdown = By.Id("TaxonomiesListField-1");
        private readonly By JobFunctionDropdown = By.Id("Dropdown-3");
        private readonly By OthersField = By.Id("Textbox-4");
        private readonly By IAgreeCheckbox = By.XPath("//input[@name='ElectricMessageOptOut']");
        private readonly By EmailIvalidErrorMessage = By.XPath("//p[@data-sf-role='error-message' and text()='Invalid email format']");
        private readonly By FistLastNameErrorMessage = By.XPath("//p[@data-sf-role='error-message' and text()='Invalid format']");

        public void ClickContactSalesBtn()
        {
            ClickElement(ContactSalesBtn);
        }

        public void ClickDisclaimerLink(string linkText)
        {
            ClickElement(By.LinkText(linkText));
        }

        public int GetMessageFieldCharacterCount()
        {
            var counterElement = Driver.FindElement(By.CssSelector(".TxtCounter-Number"));
            return int.Parse(counterElement.Text);
        }

        public string GetPhoneNumberCode()
        {
            return Driver.FindElement(PhoneField).GetAttribute("value");
        }

        public string GetHeaderText()
        {
            return GetText(ContactHeaderText);
        }

        public string GetOtherFieldPlaceholder()
        {
            return Driver.FindElement(OthersField).GetAttribute("placeholder");
        }

        public string GetDefaultProductDropdownOption()
        {
            return GetDefaultDropdownOption(ProductDropdown);
        }

        public string GetDefaultCompanyTypeDropdownOption()
        {
            return GetDefaultDropdownOption(IAmDropdown);
        }

        public string GetDefaultCountryDropdownOption()
        {
            return GetDefaultDropdownOption(CountryDropdown);
        }

        public List<string> GetProductDropdownOptions()
        {
            return GetDropdownOptions(ProductDropdown);
        }

        public List<string> GetCompanyTypeDropdownOptions()
        {
            return GetDropdownOptions(IAmDropdown);
        }

        public List<string> GetCountryDropdownOptions()
        {
            return GetDropdownOptions(CountryDropdown);
        }

        public List<string> GetStateDropdownOptions()
        {
            return GetDropdownOptions(StateDropdown);
        }

        public bool IsErrorMessageDisplayed(string errorText)
        {
            var errorLocator = By.XPath($"//p[@data-sf-role='error-message' and text()='{errorText}']");
            return IsElementDisplayed(errorLocator);
        }

        public bool IsEmailErrorMessageDisplayed()
        {
            return IsElementDisplayed(EmailIvalidErrorMessage);
        }

        public bool IsFirstLastNameErrorMessageDisplayed()
        {
            return IsElementDisplayed(FistLastNameErrorMessage);
        }

        public bool IsStateDropdownDisplayed()
        {
            return IsElementDisplayed(StateDropdown);
        }

        public bool IsIAgreeCheckboxChecked()
        {
            return Driver.FindElement(IAgreeCheckbox).Selected;
        }

        public void SelectProductType(string product)
        {
            SelectDropdownValue(ProductDropdown, product);
        }

        public void SelectJobFunctionType(string job)
        {
            SelectDropdownValue(JobFunctionDropdown, job);
        }

        public void SelectCountry(string country)
        {
            SelectDropdownValue(CountryDropdown, country);
        }

        public void SelectRandomCountry()
        {
            SelectRandomDropdownValue(CountryDropdown, 3);
        }

        public void SelectRandomCompanyType()
        {
            SelectRandomDropdownValue(IAmDropdown, 1);
        }

        public void SelectRandomIndustryType()
        {
            SelectRandomDropdownValue(IndustryDropdown, 1);
        }

        public void FillContactForm(string firstName, string lastName, string email, string company, string phone, string message)
        {
            EnterText(FirstNameField, firstName);
            EnterText(LastNameField, lastName);
            EnterText(BusinessEmailField, email);
            EnterText(CompanyField, company);
            EnterText(PhoneField, phone);
            EnterText(MessageField, message);
        }

        public void SubmitForm(bool waitForThankYouPage = true)
        {
            ClickContactSalesBtn();
            if (waitForThankYouPage) WaitForThankYouPage();
        }

        private void WaitForThankYouPage(int timeout = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            wait.Until(drv => drv.Url == TestData.ThankYouPageUrl);
        }

        public void FillOthersField(string text)
        {
            EnterText(OthersField, text);
        }

        public void CheckIAgreeCheckbox()
        {
            ClickElement(IAgreeCheckbox);
        }
    }
}