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
        public const string OtherFieldPlaceholderTxt = "e.g. Security Officer";

        public string GetHeaderText()
        {
            return GetText(ContactHeaderText);
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
            ClickElement(ContactSalesBtn);
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

        public void SubmitContactForm(string firstName, string lastName, string email, string company, string phone, string message)
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
            WaitForThankYouMessage();
        }

        public void WaitForThankYouMessage()
        {
            WaitForElement(TestData.ThankYouMessage);
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
    }
}