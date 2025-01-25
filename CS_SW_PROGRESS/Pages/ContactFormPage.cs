using OpenQA.Selenium;

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
        private readonly By headerText = By.CssSelector("h1.-mb2.-tac");

        public string GetHeaderText()
        {
            return GetText(headerText);
        }

        public string GetLabelTextByForAttribute(string forAttribute)
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
    }
}
