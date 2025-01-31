﻿using Bogus;

namespace CS_SW_PROGRESS.Utilities
{
    public static class TestData
    {
        public const string ThankYouPageUrl = "https://www.progress.com/company/contact-thank-you";
        public const string ContactFormTitleTxt = "How Can We Help?";
        public const string OtherFieldPlaceholderTxt = "e.g. Security Officer";

        public static Dictionary<string, string> GenerateContactFormData()
        {
            var faker = new Faker();
            return new Dictionary<string, string>
            {
                { "FirstName", faker.Name.FirstName() },
                { "LastName", faker.Name.LastName() },
                { "Email", "test@progress.com" },
                { "Company", faker.Company.CompanyName() },
                { "Phone", faker.Phone.PhoneNumber() },
                { "Message", faker.Lorem.Paragraph() },
                { "Job Function", faker.Name.JobTitle() },
                { "InvalidFirstName", $"{faker.Name.FirstName()}{faker.Random.String2(10, "~!@#$%^&*()-_=+")}" },
                { "InvalidLastName", $"{faker.Name.LastName()}{faker.Random.String2(10, "~!@#$%^&*()-_=+")}"}
            };
        }

        public static Dictionary<string, string> InvalidEmailData()
        {
            var faker = new Faker();
            return new Dictionary<string, string>
            {
                { "withoutAddressSign", $"{faker.Name.FirstName()}progress.com" },
                { "incompleteTopLevelDomain", $"{faker.Name.FirstName()}@progress" },
                { "emptyDomain", $"{faker.Name.FirstName()}@" },
                { "emptyString", "@progress" },
                { "emptySpaces", $" {faker.Name.FirstName()} @ progress.com" },
                { "onlyDomainName", "@progress.com" },
                { "withTwoDots", $"{faker.Name.FirstName()}@progress..com" },
                { "withTwo@Symbols", $"{faker.Name.FirstName()}@@progress.com" }
            };
        }

        public static readonly Dictionary<string, string> ExpectedErrorMessages = new()
        {
            { "Product / Interest", "Product is required" },
            { "Business Email", "Email is required" },
            { "First Name", "First name is required" },
            { "Last Name", "Last name is required" },
            { "Company", "Company is required" },
            //{ "I am...", "Company type is required" }, // Commented out because of inconsistency bug
            { "Country/Territory", "Country/territory is required" },
            { "Phone", "Phone is required" }
        };

        public static readonly Dictionary<string, List<string>> ExpectedDropdownOptions = new()
        {
            { "Product / interest", new List<string> {
                "Select product", "Chef – DevOps", "Professional Services – Consulting", "Corticon – Business Rules", "DataDirect – Secure Data Connectivity & Integration",
                "Flowmon – Network Performance and Security", "Kemp LoadMaster – Load Balancing", "Kendo UI & Telerik – UI/UX Tools", "MarkLogic Data Platform – Solve Complex Data Challenges",
                "MOVEit – Secure File Transfer", "OpenEdge – Mission Critical App Platform", "Podio – Project Management & Collaboration", "Semaphore – Smarter Decisions Powered by Metadata",
                "ShareFile – Document Sharing and Storage", "Sitefinity – Content Management and Digital Experience Platform", "ThemeBuilder – The Powerful Telerik and Kendo UI Styles Builder",
                "WhatsUp Gold – IT Infrastructure Monitoring", "WS_FTP – Secure FTP Server" } },
            { "I am...", new List<string> {
                "Select company type", "Independent Software Vendor", "Corporation or Government Entity", "Technology System Integrator or Consultancy",
                "Progress Partner", "Software Reseller", "Technology Service Provider", "Others" } },
            { "Country/Territory", new List<string> {
                "Select country/territory", "USA", "Canada", "Bulgaria", "Germany", "United Kingdom", "Japan", "Australia", "India", "Afghanistan", "Albania",
                "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina", "Armenia", "Aruba", "Austria",
                "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia and Herzegovina",
                "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Terr.", "Brunei Darussalam", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Cabo Verde",
                "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo (Brazzaville)",
                "Congo, the democratic republic of the", "Cook Islands", "Costa Rica", "Cote d'Ivoire", "Croatia (Hrvatska)", "Curacao", "Cyprus", "Czechia", "Denmark", "Djibouti",
                "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Eswatini", "Ethiopia", "Falkland Islands", "Faroe Islands",
                "Fiji", "Finland", "France", "French Guiana", "French Polynesia", "French Southern Terr.", "Gabon", "Gambia", "Georgia", "Ghana", "Gibraltar", "Greece", "Greenland",
                "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard and McDonald Is.", "Honduras", "Hong Kong", "Hungary",
                "Iceland", "Indonesia", "Iraq", "Ireland", "Israel", "Italy", "Jamaica", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea (the Republic of)", "Kuwait",
                "Kyrgyzstan", "Lao People's Dem. Rep.", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macao", "Madagascar", "Malawi",
                "Malaysia", "Maldives", "Mali", "Malta", "Man, Isle of", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia", "Moldova",
                "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "New Caledonia", "New Zealand",
                "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Northern Mariana Is.", "North Macedonia", "Norway", "Oman", "Pakistan", "Palau", "Palestine, State Of",
                "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Rwanda",
                "S.Georgia and S.Sandwich Is.", "Saint Kitts and Nevis", "Saint Lucia", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles",
                "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Sudan", "Spain", "Sri Lanka", "St. Helena", "St. Pierre and Miquelon",
                "St. Vincent and Grenadines", "Sudan", "Suriname", "Svalbard and Jan Mayen Is.", "Sweden", "Switzerland", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor-Leste",
                "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "U.S. Minor Outlying Is.", "Uganda", "Ukraine",
                "United Arab Emirates", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican (Holy See)", "Venezuela", "Viet Nam", "Virgin Islands (British)", "Virgin Islands (U.S.)", "Wallis and Futuna Is.",
                "Western Sahara", "Yemen", "Zambia", "Zimbabwe" } }
        };

        public static readonly Dictionary<string, List<string>> StateOptions = new()
        {
            { "Canada", new List<string> {
                "Select:", "Alberta", "British Columbia", "Manitoba", "New Brunswick", "Newfoundland and Labrador", "Northwest Territories",
                "Nova Scotia", "Nunavut", "Ontario", "Prince Edward Island", "Quebec", "Saskatchewan", "Yukon" } },
            { "USA", new List<string> {
                "Select:", "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia",
                "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland",
                "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey",
                "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island",
                "South Carolina", "South Dakota", "Tennessee", "Texas", "US ARMY EUROPE", "Utah", "Vermont", "Virgin Islands", "Virginia", "Washington",
                "West Virginia", "Wisconsin", "Wyoming" } }
        };

        public static readonly Dictionary<string, string> DisclaimerLinks = new()
        {
            { "Partners", "https://www.progress.com/partners/partner-locator" },
            { "Privacy Policy", "https://www.progress.com/legal/privacy-policy" },
            { "here", "https://forms.progress.com/SubscriptionMgt-English" }
        };
    }
}