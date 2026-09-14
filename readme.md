# Mars Competition - Education and Certification Test Automation

This project contains the manual test documentation and automated test suite developed for the Mars Competition Task.

The automation covers the **Education** and **Certification** profile features in Project Mars.

The framework uses .NET 8, Reqnroll, Selenium WebDriver, NUnit, JSON-based test data, Page Object Model (POM), and ExtentReports.

## Technology Stack

- **.NET 8**
- **Reqnroll** - BDD framework using Gherkin syntax
- **Selenium WebDriver** - Browser automation
- **NUnit** - Test execution and assertions
- **ExtentReports** - HTML test reporting
- **WebDriverManager** - ChromeDriver management
- **JSON** - External test data
- **Page Object Model (POM)** - Separation of test logic and UI interaction

## Features Covered

### Education

Automation coverage includes:

- Add Education with valid details
- Edit Education with valid details
- Delete Education
- Cancel Education edit
- Missing required fields
- Spaces-only text fields
- Duplicate Education records
- Duplicate Education update
- Very long University value / boundary testing

### Certification

Automation coverage includes:

- Add Certification with valid details
- Edit Certification with valid details
- Delete Certification
- Cancel Certification edit
- Missing required fields
- Spaces-only text fields
- Exact duplicate Certification
- Duplicate Certification update

## Final Test Execution

The final regression execution completed successfully:

- **Total tests:** 36
- **Passed:** 36
- **Failed:** 0
- **Skipped:** 0

## Project Structure

```text
├── Config/
│   └── Configuration models for browser, environment, report and credentials
│
├── Context/
│   └── TestDataContext.cs
│       Tracks Education and Certification records created during each scenario
│
├── Features/
│   ├── Login.feature
│   ├── Education.feature
│   └── Certification.feature
│
├── Helpers/
│   └── JsonDataReader.cs
│       Reads external JSON test data
│
├── Hooks/
│   └── Hooks.cs
│       WebDriver setup, dependency registration, reporting,
│       screenshots and scenario cleanup
│
├── Models/
│   ├── EducationData.cs
│   └── CertificationData.cs
│
├── Pages/
│   ├── LoginPage.cs
│   ├── EducationPage.cs
│   ├── CertificationPage.cs
│   └── NavigationHelper.cs
│
├── Steps/
│   ├── LoginSteps.cs
│   ├── EducationSteps.cs
│   └── CertificationSteps.cs
│
├── TestData/
│   ├── EducationTestData.json
│   └── CertificationTestData.json
│
├── TestCases/
│   └── Mars_Competition_Education_Certification_TestCases.xlsx
│
├── Tests/
│
├── settings.json
├── reqnroll.json
├── parallel.runsettings
└── qa-dotnet-cucumber.csproj
```

## Framework Architecture

The framework follows the Page Object Model pattern.

The test flow is:

```text
Feature File
    ↓
Step Definition
    ↓
JSON Test Data
    ↓
Page Object
    ↓
Selenium WebDriver
    ↓
Mars Application
    ↓
Assertion
    ↓
ExtentReport
```

### Feature Files

Reqnroll feature files contain the BDD scenarios written using:

```gherkin
Given
When
Then
```

Scenario Outlines are used where the same test logic needs to execute against multiple datasets.

### Step Definitions

Step-definition classes connect the Gherkin scenarios to the automation code.

Assertions are performed in the step-definition layer rather than inside the Page Object classes.

### Page Objects

Page Object classes contain:

- Selenium locators
- Browser interactions
- Explicit waits
- Add/Edit/Delete operations
- UI verification methods

This keeps Selenium implementation details separate from the test scenarios.

### Hooks

`Hooks.cs` manages the test lifecycle.

It is responsible for:

- Loading configuration
- Creating ChromeDriver
- Registering Page Objects using dependency injection
- Creating scenario-scoped `TestDataContext`
- Starting ExtentReports
- Logging test steps
- Capturing screenshots when a step fails
- Cleaning up Education and Certification test data
- Closing the browser after each scenario
- Flushing the final HTML report

## JSON Test Data

Test data is stored outside the feature files and C# test methods.

The project contains:

```text
TestData/
├── EducationTestData.json
└── CertificationTestData.json
```

Each dataset has a unique key.

Example Education data:

```json
{
  "validAdd": {
    "country": "New Zealand",
    "university": "Auto University Auckland",
    "title": "B.Sc",
    "degree": "Computer Science",
    "graduationYear": "2020"
  }
}
```

`JsonDataReader` reads the requested dataset and converts it into an `EducationData` or `CertificationData` object.

Example flow:

```text
"validAdd"
    ↓
JsonDataReader
    ↓
EducationTestData.json
    ↓
EducationData object
    ↓
Step Definition
    ↓
EducationPage
```

This prevents test data from being hardcoded inside the automation logic.

## Test Data Cleanup

Each scenario manages its own test data.

`TestDataContext` keeps track of records created during the scenario.

Example:

```text
Scenario creates Education
        ↓
Education is added to CreatedEducations
        ↓
Test continues
        ↓
AfterScenario executes
        ↓
Created record is deleted
```

The same approach is used for Certification records.

This helps keep scenarios independent and prevents test data from affecting later tests.

## Configuration

The tracked `settings.json` contains the common project configuration.

Example:

```json
{
  "Browser": {
    "Type": "Chrome",
    "Headless": false,
    "TimeoutSeconds": 30
  },
  "Report": {
    "Path": "TestReport.html",
    "Title": "Test Automation Report"
  },
  "Environment": {
    "BaseUrl": "http://localhost:5003"
  },
  "Credentials": {
    "Email": "your-email@example.com",
    "Password": "your-password"
  }
}
```

Real credentials are stored locally in:

```text
settings.local.json
```

`settings.local.json` is excluded from Git and must not be committed.

When available, the framework loads `settings.local.json`; otherwise it falls back to `settings.json`.

## Prerequisites

Before running the tests, install:

- .NET 8 SDK
- Visual Studio or another compatible .NET IDE
- Google Chrome
- Local Mars application

Mars should be available at:

```text
http://localhost:5003
```

## Running the Tests

### 1. Clone the repository

```bash
git clone https://github.com/Erandi88/Mars-Competition-QA-Automation.git
```

Move into the project folder.

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Configure credentials

Create a local file:

```text
settings.local.json
```

Use the same structure as `settings.json` and enter valid Mars login credentials.

Do not commit this file to GitHub.

### 4. Start Project Mars

Make sure the Mars application is running locally at:

```text
http://localhost:5003
```

### 5. Run the tests

```bash
dotnet test
```

Tests can also be executed using Visual Studio Test Explorer.

## Reporting

ExtentReports is integrated into the framework.

After execution, the report is generated as:

```text
TestReport.html
```

The report contains:

- Scenario names
- Given/When/Then step results
- Pass/fail status
- Execution duration
- Screenshots for failed steps

## Evidence

The final submission contains an `Evidence` folder with:

```text
Evidence/
├── Screenshot_TestPassed
└── Screenshot_TestReport
```

These provide evidence of the successful regression run and ExtentReports implementation.

## Manual Test Cases

The manual test cases for User Story 1 are stored in:

```text
TestCases/
└── Mars_Competition_Education_Certification_TestCases.xlsx
```

The workbook contains:

- Test steps
- Test data
- Expected results
- Actual results
- Test status
- Testing category
- Automation decision
- Priority

## Git Workflow

Development was completed on the feature branch:

```text
feature/education-certification-automation
```

A Pull Request was created to merge the completed work into `main`.

## Pull Request

Mars Competition - Education and Certification Test Automation

PR:

```text
https://github.com/Erandi88/Mars-Competition-QA-Automation/pull/1
```