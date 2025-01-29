using CS_SW_PROGRESS.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CS_SW_PROGRESS.Pages
{
    public class ContactFormPage(IWebDriver driver) : BasePage(driver)
    {
        public readonly By ProductDropdown = By.Id("Dropdown-1");
        public readonly By BusinessEmailField = By.Id("Email-1");
        public readonly By FirstNameField = By.Id("Textbox-1");
        public readonly By LastNameField = By.Id("Textbox-2");
        public readonly By CompanyField = By.Id("Textbox-3");
        public readonly By IAmDropdown = By.Id("Dropdown-2");
        public readonly By CountryDropdown = By.Id("Country-1");
        public readonly By PhoneField = By.Id("Textbox-5");
        public readonly By StateDropdown = By.Id("State-1");
        public readonly By MessageField = By.Id("Textarea-1");
        public readonly By ContactSalesBtn = By.CssSelector("button[type='submit']");
        public readonly By ContactHeaderText = By.CssSelector("h1.-mb2.-tac");
        public readonly By IndustryDropdown = By.Id("TaxonomiesListField-1");
        public readonly By JobFunctionDropdown = By.Id("Dropdown-3");
        public readonly By OthersField = By.Id("Textbox-4");
        public readonly By IAgreeCheckbox = By.XPath("//input[@name='ElectricMessageOptOut']");
        public readonly By EmailIvalidErrorMessage = By.XPath("//p[@data-sf-role='error-message' and text()='Invalid email format']");
        public readonly By FistLastNameErrorMessage = By.XPath("//p[@data-sf-role='error-message' and text()='Invalid format']");
        public const string OtherFieldPlaceholderTxt = "e.g. Security Officer";
        public const string ContactFormTitle = "How Can We Help?";


        public string GetHeaderText()
        {
            return GetText(ContactHeaderText);
        }

        public void ClickContactSalesBtn()
        {
            ClickElement(ContactSalesBtn);
        }

        public string GetErrorMessage(string forAttribute)
        {
            var errorMessageLocator = By.XPath($"//p[@data-sf-role='error-message' and text()='{forAttribute}']");
            return GetText(errorMessageLocator);
        }

        public bool IsEmailErrorMessageDisplayed()
        {
            return IsElementDisplayed(EmailIvalidErrorMessage);
        }

        public bool IsFirstLastNameErrorMessageDisplayed()
        {
            return IsElementDisplayed(FistLastNameErrorMessage);
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
            SelectDropdownValue(CountryDropdown, country);
        }

        public bool IsStateDropdownDisplayed()
        {
            return IsElementDisplayed(StateDropdown);
        }

        public string GetPhoneNumberCode()
        {
            return Driver.FindElement(PhoneField).GetAttribute("value");
        }

        public void SelectRandomCountry()
        {
            var countryOptions = GetDropdownOptions(CountryDropdown);
            Random random = new();
            string randomCountry = countryOptions[random.Next(3, countryOptions.Count)];
            SelectDropdownValue(CountryDropdown, randomCountry);
        }

        public void SelectRandomCompanyType()
        {
            var companyTypeOptions = GetDropdownOptions(IAmDropdown);
            Random random = new();
            string randomCompanyType = companyTypeOptions[random.Next(1, companyTypeOptions.Count)];
            SelectDropdownValue(IAmDropdown, randomCompanyType);
        }

        public void SelectRandomIndustyType()
        {
            var industryTypeOptions = GetDropdownOptions(IndustryDropdown);
            Random random = new();
            string randomIndustryType = industryTypeOptions[random.Next(1, industryTypeOptions.Count)];
            SelectDropdownValue(IndustryDropdown, randomIndustryType);
        }

        public void ClickDisclaimerLink(string linkText)
        {
            By linkLocator = By.LinkText(linkText);
            ClickElement(linkLocator);
        }

        public void SubmitContactForm(string firstName, string lastName, string email, string company, string phone, string message, bool waitForThankYouPage = true)
        {
            EnterText(FirstNameField, firstName);
            EnterText(LastNameField, lastName);
            EnterText(BusinessEmailField, email);
            EnterText(CompanyField, company);
            EnterText(PhoneField, phone);
            EnterText(MessageField, message);
            SelectRandomIndustyType();
            SelectRandomCountry();
            ClickContactSalesBtn();
            if (waitForThankYouPage)
            {
                WaitForThankYouPage();
            }
        }

        public void WaitForThankYouPage(int timeout = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            wait.Until(drv => drv.Url == TestData.ThankYouPageUrl);
        }

        public void SelectProductType(string product)
        {
            SelectDropdownValue(ProductDropdown, product);
        }

        public void SelectJobFunctionType(string job)
        {
            SelectDropdownValue(JobFunctionDropdown, job);
        }

        public void FillOthersField(string text)
        {
            EnterText(OthersField, text);
        }

        public string GetOtherFieldPlaceholder()
        {
            return Driver.FindElement(OthersField).GetAttribute("placeholder");
        }

        public void CheckIAgreeCheckbox()
        {
            ClickElement(IAgreeCheckbox);
        }

        public bool IsIAgreeCheckboxChecked()
        {
            return Driver.FindElement(IAgreeCheckbox).Selected;
        }

        public string GetDefaultDropdownOption(By dropdownLocator)
        {
            var dropdownElement = new SelectElement(Driver.FindElement(dropdownLocator));
            return dropdownElement.SelectedOption.Text;
        }
    }
}