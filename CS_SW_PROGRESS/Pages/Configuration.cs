namespace CS_SW_PROGRESS.Pages
{
    public static class Configuration
    {
        public const string HeaderText = "How Can We Help?";

        public static readonly Dictionary<string, string> ExpectedLabels = new()
        {
            { "Dropdown-1", "Product / interest" },
            { "Email-1", "Business Email" },
            { "Textbox-1", "First Name" },
            { "Textbox-2", "Last Name" },
            { "Textbox-3", "Company" },
            { "Dropdown-2", "I am..." },
            { "Country-1", "Country/Territory" },
            { "Textarea-1", "Message" },
            { "Textbox-5", "Phone" }
        };

        public static readonly Dictionary<string, string> RequiredFields = new()
        {
            { "Product Dropdown", "Dropdown-1" },
            { "Business Email Field", "Email-1" },
            { "First Name Field", "Textbox-1" },
            { "Last Name Field", "Textbox-2" },
            { "Company Field", "Textbox-3" },
            { "I Am Dropdown", "Dropdown-2" },
            { "Country Dropdown", "Country-1" },
            { "State Dropdown", "State-1" },
            { "Phone Field", "Textbox-5" }
        };
    }
}
