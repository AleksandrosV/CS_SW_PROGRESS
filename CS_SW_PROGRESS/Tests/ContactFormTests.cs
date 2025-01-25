using CS_SW_PROGRESS.Pages;
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
            Assert.That(actualHeaderText, Is.EqualTo(Configuration.HeaderText), "The header text does not match the expected value.");
        }

        [Test]
        public void VerifyAllLabelsText()
        {
            foreach (var label in Configuration.ExpectedLabels)
            {
                string actualLabelText = _contactFormPage.GetLabelTextByForAttribute(label.Key);
                Assert.That(actualLabelText, Is.EqualTo(label.Value), $"The label text for '{label.Key}' does not match the expected '{label.Value}'.");
            }
        }
    }
}

