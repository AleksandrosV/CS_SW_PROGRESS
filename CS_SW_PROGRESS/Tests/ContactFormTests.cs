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
            Assert.That(headerText, Is.EqualTo("How Can We Help?"), $"The header text does not match the expected.");
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
        [TestCase("Product / interest", "Select product", nameof(ContactFormPage.GetDefaultProductDropdownOption))]
        [TestCase("Country/Territory", "Select country/territory", nameof(ContactFormPage.GetDefaultCountryDropdownOption))]
        [TestCase("I am...", "Select company type", nameof(ContactFormPage.GetDefaultCompanyTypeDropdownOption))]
        public void VerifyDefaultSelectedDropdownOptions(string dropdown, string expectedDefaultOption, string methodName)
        {
            var method = typeof(ContactFormPage).GetMethod(methodName);
            if (method == null)
            {
                Assert.Fail($"Method '{methodName}' not found in ContactFormPage.");
            }
            var actualDefaultOption = method?.Invoke(_contactFormPage, null) as string;
            Assert.That(actualDefaultOption, Is.Not.Null, $"The method '{methodName}' returned null.");
            Assert.That(actualDefaultOption, Is.EqualTo(expectedDefaultOption), $"Default option for dropdown '{dropdown}' was expected to be '{expectedDefaultOption}' but was '{actualDefaultOption}'.");
        }

        [Test]
        [Category("Dropdowns")]
        [TestCase("Product / interest", nameof(ContactFormPage.GetProducDropdownOptions))]
        [TestCase("I am...", nameof(ContactFormPage.GetCompanyTypeDropdownOptions))]
        [TestCase("Country/Territory", nameof(ContactFormPage.GetCountryDropdownOptions))]
        public void VerifyDropdownsOptions(string dropdown, string methodName)
        {
            // Use reflection to get the method by name
            var method = typeof(ContactFormPage).GetMethod(methodName);
            if (method == null)
            {
                Assert.Fail($"Method '{methodName}' not found in ContactFormPage.");
            }
            var actualOptions = method?.Invoke(_contactFormPage, null) as List<string>;
            Assert.That(actualOptions, Is.Not.Null, $"The method '{methodName}' returned null.");
            CollectionAssert.AreEqual(TestData.ExpectedDropdownOptions[dropdown], actualOptions, $"The options for the '{dropdown}' dropdown do not match the expected options.");
        }

        [Test]
        [Category("Dropdowns")]
        [TestCase("Canada", true)]
        [TestCase("USA", true)]
        [TestCase("Venezuela", false)]
        [TestCase("Tuvalu", false)]
        public void VerifyStateDropdownVisibility(string country, bool shouldBeDisplayed)
        {
            _contactFormPage.SelectCountry(country);
            Assert.That(_contactFormPage.IsStateDropdownDisplayed(), Is.EqualTo(shouldBeDisplayed),
                $"The 'State' field display status for '{country}' was expected to be '{shouldBeDisplayed}', but it was not.");
        }

        [Test]
        [Category("Dropdowns")]
        [TestCase("Canada")]
        [TestCase("USA")]
        public void VerifyStateDropdownOptionsForCountriesWithStates(string country)
        {
            _contactFormPage.SelectCountry(country);
            List<string> actualOptions = _contactFormPage.GetStateDropdownOptions();
            CollectionAssert.AreEqual(TestData.StateOptions[country], actualOptions, $"The options for the 'State' dropdown when '{country}' is selected do not match the expected options.");
        }

        [Test]
        [TestCase("Canada", "+1 ")]
        [TestCase("Bulgaria", "+359 ")]
        public void VerifyPhoneNumberCodeForCountry(string country, string expectedCode)
        {
            _contactFormPage.SelectCountry(country);
            string actualPhoneNumberCode = _contactFormPage.GetPhoneNumberCode();
            Assert.That(actualPhoneNumberCode, Is.EqualTo(expectedCode), $"The phone number code for '{country}' does not match the expected '{expectedCode}'.");
        }

        [Test]
        public void VerifyDisclaimerLinksNavigateToCorrectUrls()
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
            _contactFormPage.SelectRandomIndustyType();
            _contactFormPage.SelectRandomCompanyType();
            _contactFormPage.SelectRandomCountry();
            _contactFormPage.FillContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
            _contactFormPage.SubmitForm();
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
            _contactFormPage.SelectRandomCountry();
            _contactFormPage.SelectRandomIndustyType();
            _contactFormPage.FillContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
            _contactFormPage.SubmitForm();
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
            _contactFormPage.SelectRandomCountry();
            _contactFormPage.SelectRandomIndustyType();
            _contactFormPage.FillOthersField(data["Job Function"]);
            _contactFormPage.FillContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
            _contactFormPage.SubmitForm();
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
                _contactFormPage.FillContactForm(data["FirstName"], data["LastName"], data["Email"], data["Company"], data["Phone"], data["Message"]);
                _contactFormPage.SubmitForm(false);
                Assert.That(_contactFormPage.IsEmailErrorMessageDisplayed(), Is.True, $"The form was submitted successfully with an invalid email: {invalidEmail.Value} and the email error message is not displayed.");
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
            _contactFormPage.FillContactForm(data[firstNameKey], data[lastNameKey], data["Email"], data["Company"], data["Phone"], data["Message"]);
            _contactFormPage.SubmitForm(false);
            Assert.That(_contactFormPage.IsFirstLastNameErrorMessageDisplayed(), Is.True, $"The name error message is not displayed for: {data[firstNameKey]} {data[lastNameKey]}.");
        }

        [Test]
        [TestCase("MOVEit – Secure File Transfer", "Other")]
        public void VerifyOtherFieldPlaceholder(string product, string job)
        {
            _contactFormPage.SelectProductType(product);
            _contactFormPage.SelectJobFunctionType(job);
            string actualPlaceholder = _contactFormPage.GetOtherFieldPlaceholder();
            Assert.That(actualPlaceholder, Is.EqualTo("e.g. Security Officer"), $"The placeholder text for the {job} field does not match the expected.");
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