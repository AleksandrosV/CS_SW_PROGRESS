using CS_SW_PROGRESS.Pages;
using OpenQA.Selenium;
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
        public void VerifyContactTitleIsDisplayed()
        {
            string headerText = _contactFormPage.GetHeaderText();
            Assert.That(headerText, Is.EqualTo(ContactFormPage.ContactFormTitle), $"The header text does not match the expected '{ContactFormPage.ContactFormTitle}'.");
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
        public void VerifyDefaultDropdownValueForCountriesWithStates(string country)
        {
            _contactFormPage.SelectCountry(country);
            Assert.That(_contactFormPage.IsStateDropdownDisplayed(), Is.True, $"The 'State' field is not displayed when '{country}' is selected.");
        }

        [Test]
        [Category("Dropdowns")]
        [TestCase("Canada")]
        [TestCase("USA")]
        public void VerifyStateDropdownOptionsForCountriesWithStates(string country)
        {
            _contactFormPage.SelectCountry(country);
            List<string> actualOptions = _contactFormPage.GetDropdownOptions(_contactFormPage.StateDropdown);
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
        [Category("Contact Form Submission")]
        [TestCase("MarkLogic Data Platform – Solve Complex Data Challenges")]
        public void VerifySubmitValidContactFormWithIndustry(string product)
        {
            var data = TestData.GenerateContactFormData();
            _contactFormPage.SelectProductType(product);
            _contactFormPage.SelectRandomCompanyType();
            _contactFormPage.SubmitContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
            Assert.That(Driver.Url, Is.EqualTo(TestData.ThankYouUrl), "The URL redirection is incorrect.");
        }

        [Test]
        [Category("Contact Form Submission")]
        [TestCase("MOVEit – Secure File Transfer", "Head of Security/Compliance")]
        public void VerifySubmitValidContactFormWithJobFunction(string product, string job)
        {
            var data = TestData.GenerateContactFormData();
            _contactFormPage.SelectProductType(product);
            _contactFormPage.SelectJobFunctionType(job);
            _contactFormPage.SubmitContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
            Assert.That(Driver.Url, Is.EqualTo(TestData.ThankYouUrl), "The URL redirection is incorrect.");
        }

        [Test]
        [Category("Contact Form Submission")]
        [TestCase("MOVEit – Secure File Transfer", "Other")]
        public void VerifySubmitValidContactFormWithJobFunctionOthers(string product, string job)
        {
            var data = TestData.GenerateContactFormData();
            _contactFormPage.SelectProductType(product);
            _contactFormPage.SelectJobFunctionType(job);
            _contactFormPage.FillOthersField(data["Job Function"]);
            _contactFormPage.SubmitContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
            Assert.That(Driver.Url, Is.EqualTo(TestData.ThankYouUrl), "The URL redirection is incorrect.");
        }

        [Test]
        [TestCase("MOVEit – Secure File Transfer", "Other")]
        public void VerifyOtherFieldPlaceholder(string product, string job)
        {
            _contactFormPage.SelectProductType(product);
            _contactFormPage.SelectJobFunctionType(job);
            _ = _contactFormPage.GetOtherFieldPlaceholder();
            Assert.That(_contactFormPage.GetOtherFieldPlaceholder(), Is.EqualTo(ContactFormPage.OtherFieldPlaceholderTxt));
        }

        [Test]
        [TestCase("Germany")]
        public void VerifyCheckboxSelection(string country)
        {
            _contactFormPage.SelectCountry(country);
            _contactFormPage.CheckIAgreeCheckbox();
            Assert.That(_contactFormPage.IsIAgreeCheckboxChecked(), Is.True, "The 'I agree' checkbox is not checked.");
        }
    }
}