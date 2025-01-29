
# PROGRESS Contact Form Automation

## Project Overview

Welcome to the The Progress project! This project automates UI testing of the Progress contact form using [C#](https://dotnet.microsoft.com/en-us/languages/csharp), [Selenium WebDriver](https://www.selenium.dev/documentation/webdriver/), and the **Page Object Model** design pattern.

## Table of Contents

- [Project Overview](#project-overview)
- [Project Structure](#project-structure)
- [Setup Instructions](#setup-instructions)
- [Running Tests](#running-tests)
- [Contributing](#contributing)

## Project Structure

- `Pages/`: Contains the page classes.
- `Tests/`: Contains the test classes.
- `Utilities/`: Contains the file for test data.

## Setup Instructions

### Prerequisites

- **Visual Studio** (2022)
- **Chrome Browser**
- **Git**
- The following NuGet packages:
  - `Bogus`
  - `coverlet.collector`
  - `Microsoft.NET.Test.Sdk`
  - `NUnit`
  - `NUnit.Analyzers`
  - `NUnit3TestAdapter`
  - `Selenium.Support`
  - `Selenium.WebDriver`

### Clone the Repository

`git clone https://github.com/AleksandrosV/CS_SW_PROGRESS.git`

### Open the project in Visual Studio.

### Restore NuGet packages:

`dotnet restore`

### Build the solution:

`dotnet build`

## Running Tests

1.Open Visual Studio's Test Explorer and run tests from there.
2.Alternatively, use the terminal:

`dotnet test`

## Contributing

Feel free to fork the repository and contribute by submitting pull requests. All contributions are welcome!

1. Fork the repository.
2. Create a new branch: `git checkout -b feature/your-feature-name`.
3. Make your changes and commit them: `git commit -m 'Add some feature'`.
4. Push to the branch: `git push origin feature/your-feature-name`.
5. Submit a pull request.

Please make sure to update tests as appropriate.
