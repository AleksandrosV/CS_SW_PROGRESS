namespace CS_SW_PROGRESS.Pages
{
    public static class Configuration
    {
        public const string ContactFormHeader = "How Can We Help?";

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

        public static readonly Dictionary<string, string> ExpectedDropdownDefaults = new()
        {
            { "Dropdown-1", "Select product" },
            { "Dropdown-2", "Select company type" },
            { "Country-1", "Select country/territory" }
        };

        public static readonly Dictionary<string, List<string>> ExpectedDropdownOptions = new()
        {
            { "Dropdown-1", new List<string> {
                "Select product", "Chef – DevOps", "Professional Services – Consulting", "Corticon – Business Rules", "DataDirect – Secure Data Connectivity & Integration",
                "Flowmon – Network Performance and Security", "Kemp LoadMaster – Load Balancing", "Kendo UI & Telerik – UI/UX Tools", "MarkLogic Data Platform – Solve Complex Data Challenges",
                "MOVEit – Secure File Transfer", "OpenEdge – Mission Critical App Platform", "Podio – Project Management & Collaboration", "Semaphore – Smarter Decisions Powered by Metadata",
                "ShareFile – Document Sharing and Storage", "Sitefinity – Content Management and Digital Experience Platform", "ThemeBuilder – The Powerful Telerik and Kendo UI Styles Builder",
                "WhatsUp Gold – IT Infrastructure Monitoring", "WS_FTP – Secure FTP Server" } },
            { "Dropdown-2", new List<string> {
                "Select company type", "Independent Software Vendor", "Corporation or Government Entity", "Technology System Integrator or Consultancy",
                "Progress Partner", "Software Reseller", "Technology Service Provider", "Others" } },
            { "Country-1", new List<string> {
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

        public static readonly Dictionary<string, string> DropdownLabels = new()
        {
            { "Dropdown-1", "Product / interest" },
            { "Dropdown-2", "I am..." },
            { "Country-1", "Country/Territory" }
        };

        // Data for countries without state dropdowns for example, we can add more countries
        public static readonly List<string> CountriesWithoutStateDropdown = ["Venezuela", "Tuvalu"];

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

        public static readonly Dictionary<string, string> CountriesWithStateDropdownLabels = new()
        {
            { "Canada", "State-1" },
            { "USA", "State-1" }
        };

        // Here we can add more countries and their phone codes
        public static readonly Dictionary<string, string> CountryPhoneCodes = new()
        {
            { "USA", "" },
            { "Canada", "+1 " },
            { "Bulgaria", "+359 " }
        };

        public static readonly Dictionary<string, string> DisclaimerLinks = new()
        {
            { "Partners", "https://www.progress.com/partners/partner-locator" },
            { "Privacy Policy", "https://www.progress.com/legal/privacy-policy" },
            { "here", "https://forms.progress.com/SubscriptionMgt-English" }
        };

    }
}
