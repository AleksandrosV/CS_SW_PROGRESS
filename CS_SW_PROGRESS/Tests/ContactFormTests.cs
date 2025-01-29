using CS_SW_PROGRESS.Pages;
using CS_SW_PROGRESS.Utilities;
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
        public void VerifyDefaultSelectedDropdownOptions()
        {
            foreach (var dropdown in TestData.DropdownDefaultOptions)
            {
                string actualDefaultOption = _contactFormPage.GetDefaultDropdownOption(dropdown.Key);
                Assert.That(actualDefaultOption, Is.EqualTo(dropdown.Value), $"Default option for dropdown with locator {dropdown.Key} was expected to be '{dropdown.Value}' but was '{actualDefaultOption}'.");
            }
        }

        [Test]
        [Category("Dropdowns")]
        public void VerifyDropdownsOptions()
        {
            foreach (var dropdown in TestData.ExpectedDropdownOptions)
            {
                List<string> expectedOptions = dropdown.Value;
                List<string> actualOptions = _contactFormPage.GetDropdownOptions(dropdown.Key);
                CollectionAssert.AreEqual(expectedOptions, actualOptions, $"The options for dropdown '{dropdown.Key}' do not match the expected options.");
            }
        }

        [Test]
        [Category("Dropdowns")]
        [TestCase("Canada")]
        [TestCase("USA")]
        public void VerifyStateDropdownIsDysplayedForCountriesWithStates(string country)
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
        [Category("Contact Form Valid Submission")]
        [TestCase("MarkLogic Data Platform – Solve Complex Data Challenges")]
        public void VerifySubmitValidContactFormWithIndustry(string product)
        {
            var data = TestData.GenerateContactFormData();
            _contactFormPage.SelectProductType(product);
            _contactFormPage.SelectRandomCompanyType();
            _contactFormPage.SubmitContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
            Assert.That(Driver.Url, Is.EqualTo(TestData.ThankYouPageUrl), "The URL redirection is incorrect.");
        }

        [Test]
        [Category("Contact Form Valid Submission")]
        [TestCase("MOVEit – Secure File Transfer", "System Administrator")]
        public void VerifySubmitValidContactFormWithJobFunction(string product, string job)
        {
            var data = TestData.GenerateContactFormData();
            _contactFormPage.SelectProductType(product);
            _contactFormPage.SelectJobFunctionType(job);
            _contactFormPage.SubmitContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
            Assert.That(Driver.Url, Is.EqualTo(TestData.ThankYouPageUrl), "The URL redirection is incorrect.");
        }

        [Test]
        [Category("Contact Form Valid Submission")]
        [TestCase("MOVEit – Secure File Transfer", "Other")]
        public void VerifySubmitValidContactFormWithJobFunctionOther(string product, string job)
        {
            var data = TestData.GenerateContactFormData();
            _contactFormPage.SelectProductType(product);
            _contactFormPage.SelectJobFunctionType(job);
            _contactFormPage.FillOthersField(data["Job Function"]);
            _contactFormPage.SubmitContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
            Assert.That(Driver.Url, Is.EqualTo(TestData.ThankYouPageUrl), "The URL redirection is incorrect.");
        }

        [Test]
        [Category("Contact Form Invalid Submission")]
        [TestCase("MOVEit – Secure File Transfer", "Head of Security/Compliance")]
        public void VerifyInvalidEmailSubmission(string product, string job)
        {
            var invalidEmails = TestData.InvalidEmailData();
            foreach (var invalidEmail in invalidEmails)
            {
                var data = TestData.GenerateContactFormData();
                data["Email"] = invalidEmail.Value;
                _contactFormPage.SelectProductType(product);
                _contactFormPage.SelectJobFunctionType(job);
                _contactFormPage.SubmitContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"], waitForThankYouPage: false);
                Assert.Multiple(() =>
                {
                    Assert.That(Driver.Url, Is.Not.EqualTo(TestData.ThankYouPageUrl), $"The form was submitted successfully with an invalid email: {invalidEmail.Value}");
                    Assert.That(_contactFormPage.IsEmailErrorMessageDisplayed(), Is.True, "The email error message is not displayed.");
                });
            }
        }

        [Test]
        [Category("Contact Form Invalid Submission")]
        [TestCase("MOVEit – Secure File Transfer", "IT Director/Executive", "InvalidFirstName", "LastName")]
        [TestCase("MOVEit – Secure File Transfer", "Solution Architect", "FirstName", "InvalidLastName")]
        public void VerifyInvalidFirstLastNameSubmission(string product, string job, string firstNameKey, string lastNameKey)
        {
            var data = TestData.GenerateContactFormData();
            _contactFormPage.SelectProductType(product);
            _contactFormPage.SelectJobFunctionType(job);
            _contactFormPage.SubmitContactForm(data[firstNameKey], data[lastNameKey], data["Email"], data["Company"], data["Phone"], data["Message"], waitForThankYouPage: false);
            Assert.Multiple(() =>
            {
                Assert.That(Driver.Url, Is.Not.EqualTo(TestData.ThankYouPageUrl), $"The form was submitted successfully with an invalid first name: {data["InvalidFirstName"]}");
                Assert.That(_contactFormPage.IsFirstLastNameErrorMessageDisplayed(), Is.True, "The first name error message is not displayed.");
            });
        }

        [Test]
        [TestCase("MOVEit – Secure File Transfer", "Other")]
        public void VerifyOtherFieldPlaceholder(string product, string job)
        {
            _contactFormPage.SelectProductType(product);
            _contactFormPage.SelectJobFunctionType(job);
            _contactFormPage.GetOtherFieldPlaceholder();
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