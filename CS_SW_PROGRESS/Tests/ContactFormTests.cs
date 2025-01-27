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
        public void VerifyHeaderText()
        {
            string actualHeaderText = _contactFormPage.GetHeaderText();
            Assert.That(actualHeaderText, Is.EqualTo(Configuration.ContactFormHeader), $"The header text does not match the expected '{Configuration.ContactFormHeader}'.");
        }

        [Test]
        public void VerifyLabelsText()
        {
            foreach (var label in Configuration.ExpectedLabels)
            {
                string actualLabelText = _contactFormPage.GetLabelText(label.Key);
                Assert.That(actualLabelText, Is.EqualTo(label.Value), $"The label text for '{label.Key}' does not match the expected '{label.Value}'.");
            }
        }

        [Test]
        public void VerifyLabelsHaveRequiredClass()
        {
            foreach (var field in Configuration.RequiredFields)
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
            foreach (var field in Configuration.ExpectedErrorMessages)
            {
                string actualErrorMessage = _contactFormPage.GetErrorMessage(field.Value);
                Assert.That(actualErrorMessage, Is.EqualTo(field.Value), $"The error message for '{field.Key}' does not match the expected '{field.Value}'.");
            }
        }

        [Test]
        public void VerifyDefaultValuesForDropdowns()
        {
            foreach (var dropdown in Configuration.ExpectedDropdownDefaults)
            {
                string actualDropdownValue = _contactFormPage.GetSelectedDropdownValue(By.Id(dropdown.Key));
                string dropdownLabel = Configuration.DropdownLabels[dropdown.Key];
                Assert.That(actualDropdownValue, Is.EqualTo(dropdown.Value), $"The default value for dropdown '{dropdownLabel}' does not match the expected value.");
            }
        }

        [Test]
        public void VerifyDropdownOptions()
        {
            foreach (var dropdown in Configuration.ExpectedDropdownOptions)
            {
                List<string> actualOptions = _contactFormPage.GetDropdownOptions(By.Id(dropdown.Key));
                string dropdownLabel = Configuration.DropdownLabels[dropdown.Key];
                CollectionAssert.AreEqual(dropdown.Value, actualOptions, $"The options for dropdown '{dropdownLabel}' do not match the expected options.");
            }
        }

        [Test]
        public void VerifyDefaultDropdownValuesForCountriesWithStates()
        {
            foreach (var country in Configuration.CountriesWithStateDropdownLabels)
            {
                _contactFormPage.SelectCountry(country.Key);
                Assert.That(_contactFormPage.IsStateDropdownDisplayed(), Is.True, $"The 'State' field is not displayed when '{country}' is selected.");
            }
        }

        [Test]
        public void VerifyStateDropdownOptionsForCountriesWithStates()
        {
            foreach (var country in Configuration.CountriesWithStateDropdownLabels)
            {
                _contactFormPage.SelectCountry(country.Key);
                List<string> actualOptions = _contactFormPage.GetDropdownOptions(By.Id(country.Value));
                CollectionAssert.AreEqual(Configuration.StateOptions[country.Key], actualOptions, $"The options for the 'State' dropdown when '{country.Key}' is selected do not match the expected options.");
            }
        }

        [Test]
        public void VerifyStateDropdownNotDisplayedForCountriesWithoutStates()
        {
            foreach (var country in Configuration.CountriesWithoutStateDropdown)
            {
                _contactFormPage.SelectCountry(country);
                Assert.That(_contactFormPage.IsStateDropdownDisplayed(), Is.False, $"The 'State' field is displayed when '{country}' is selected.");
            }
        }

        [Test]
        public void VerifyPhoneNumberCodeForSelectedCountries()
        {
            foreach (var country in Configuration.CountryPhoneCodes)
            {
                _contactFormPage.SelectCountry(country.Key);
                string actualPhoneNumberCode = _contactFormPage.GetPhoneNumberCode();
                Assert.That(actualPhoneNumberCode, Is.EqualTo(country.Value), $"The phone number code for '{country.Key}' does not match the expected '{country.Value}'.");
            }
        }

        [Test]
        public void VerifyDisclaimerLinks()
        {
            _contactFormPage.SelectRandomCountry();
            foreach (var link in Configuration.DisclaimerLinks)
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
    }
}

