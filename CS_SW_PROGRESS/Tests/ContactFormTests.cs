using CS_SW_PROGRESS.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace CS_SW_PROGRESS.Tests
{
    public class ContactFormTests : TestBase
    {
        private ContactFormPage _contactFormPage;

        [SetUp]
        public void SetUpTest()
        {
            _contactFormPage = new ContactFormPage(Driver);
            Driver.Navigate().GoToUrl(ContactPageUrl);
        }

        [Test]
        public void VerifyHeaderText()
        {
            string actualHeaderText = _contactFormPage.GetHeaderText();
            Assert.That(actualHeaderText, Is.EqualTo(TestData.ContactFormHeader), $"The header text does not match the expected '{TestData.ContactFormHeader}'.");
        }

        [Test]
        public void VerifyLabelsText()
        {
            foreach (var label in TestData.ExpectedLabels)
            {
                string actualLabelText = _contactFormPage.GetLabelText(label.Key);
                Assert.That(actualLabelText, Is.EqualTo(label.Value), $"The label text for '{label.Key}' does not match the expected '{label.Value}'.");
            }
        }

        [Test]
        public void VerifyLabelsHaveRequiredClass()
        {
            foreach (var field in TestData.RequiredFields)
            {
                string forAttribute = field.Value;
                bool isLabelRequired = _contactFormPage.IsLabelRequired(forAttribute);
                Assert.That(isLabelRequired, Is.True, $"The label for {field.Key} does not have the 'required' class.");
            }
        }

        [Test]
        public void VerifyErrorMessagesForRequiredField()
        {
            _contactFormPage.ClickContactSalesBtn();
            foreach (var field in TestData.ExpectedErrorMessages)
            {
                string actualErrorMessage = _contactFormPage.GetErrorMessage(field.Value);
                Assert.That(actualErrorMessage, Is.EqualTo(field.Value), $"The error message for '{field.Key}' does not match the expected '{field.Value}'.");
            }
        }

        [Test]
        [Category("Dropdowns")]
        [TestCase("Dropdown-1", "Select product", "Product / interest")]
        [TestCase("Dropdown-2", "Select company type", "I am...")]
        [TestCase("Country-1", "Select country/territory", "Country/territory")]
        public void VerifyDefaultValuesForDropdowns(string dropdown, string dropdownDefault, string dropdownName)
        {
            string dropdownField = _contactFormPage.GetSelectedDropdownValue(By.Id(dropdown));
            Assert.That(dropdownField, Is.EqualTo(dropdownDefault), $"The default value for dropdown '{dropdownName}' does not match the expected value.");
        }

        [Test]
        [Category("Dropdowns")]
        [TestCase("Dropdown-1", "Product / interest")]
        [TestCase("Dropdown-2", "I am...")]
        [TestCase("Country-1", "Country/Territory")]
        public void VerifyDropdownsOptions(string dropdown, string name)
        {
            List<string> actualOptions = _contactFormPage.GetDropdownOptions(By.Id(dropdown));
            CollectionAssert.AreEqual(TestData.ExpectedDropdownOptions[dropdown], actualOptions, $"The options for dropdown '{name}' do not match the expected options.");
        }

        [Test]
        [Category("Dropdowns")]
        [TestCase("Canada")]
        [TestCase("USA")]
        public void VerifyDefaultDropdownValuesForCountriesWithStates(string country)
        {
            _contactFormPage.SelectCountry(country);
            Assert.That(_contactFormPage.IsStateDropdownDisplayed(), Is.True, $"The 'State' field is not displayed when '{country}' is selected.");
        }

        [Test]
        [Category("Dropdowns")]
        [TestCase("Canada", "State-1")]
        [TestCase("USA", "State-1")]
        public void VerifyStateDropdownOptionsForCountriesWithStates(string country, string dropdown)
        {
            _contactFormPage.SelectCountry(country);
            List<string> actualOptions = _contactFormPage.GetDropdownOptions(By.Id(dropdown));
            CollectionAssert.AreEqual(TestData.StateOptions[country], actualOptions, $"The options for the 'State' dropdown when '{country}' is selected do not match the expected options.");
        }

        [Test]
        [Category("Dropdowns")]
        [TestCase("Venezuela")]
        [TestCase("Tuvalu")]
        public void VerifyStateDropdownNotDisplayedForCountriesWithoutStates(string country)
        {
            _contactFormPage.SelectCountry(country);
            Assert.That(_contactFormPage.IsStateDropdownDisplayed(), Is.False, $"The 'State' field is displayed when '{country}' is selected.");
        }

        [Test]
        [TestCase("Canada", "+1 ")]
        [TestCase("Bulgaria", "+359 ")]
        public void VerifyPhoneNumberCodeForCountry(string country, string expectedCode)
        {
            _contactFormPage.SelectCountry(country);
            string actualPhoneNumberCode = _contactFormPage.GetPhoneNumberCode();
            Assert.That(actualPhoneNumberCode, Is.EqualTo(expectedCode));
        }

        [Test]
        public void VerifyDisclaimerLinks()
        {
            _contactFormPage.SelectRandomCountry();
            foreach (var link in TestData.DisclaimerLinks)
            {
                string linkText = link.Key;
                string expectedUrl = link.Value;
                _contactFormPage.ClickDisclaimerLink(linkText);
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                string actualUrl = Driver.Url;
                Assert.That(actualUrl, Is.EqualTo(expectedUrl), $"The URL for '{linkText}' does not match the expected '{expectedUrl}'.");
                Driver.Close();
                Driver.SwitchTo().Window(Driver.WindowHandles.First());
            }
        }

        [Test]
        public void SubmitValidContactForm()
        {
            var data = TestData.GenerateContactFormData();
            _contactFormPage.SubmitContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains(TestData.ThankYouUrl));
            Assert.That(Driver.Url, Is.EqualTo(TestData.ThankYouUrl), "The URL redirection is incorrect.");
            var successMessage = wait.Until(d => d.FindElement(By.CssSelector("h1.-mb4")));
            Assert.Multiple(() =>
            {
                Assert.That(successMessage.Displayed, Is.True, "The success message is not displayed.");
                Assert.That(successMessage.Text, Is.EqualTo(TestData.ThankYouMessage), "The success message text is incorrect.");
            });
        }
    }
}

