using AventStack.ExtentReports;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{
    public class EducationPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Locators
        private readonly By EducationTab =
            By.XPath("//a[@data-tab='third' and normalize-space()='Education']");

        private readonly By AddNewButton =
            By.XPath("//div[contains(@class,'active')]//div[contains(@class,'ui teal button') and normalize-space()='Add New']");

        private readonly By UniversityField = By.XPath("//input[@placeholder='College/University Name']");

        private readonly By CountryDropdown = By.XPath("//select[@name='country']");

        private readonly By TitleDropdown = By.XPath("//select[@name='title']");

        private readonly By DegreeField = By.XPath("//input[@placeholder='Degree']");

        private readonly By GraduationYearDropdown = By.XPath("//select[@name='yearOfGraduation']");

        private readonly By AddButton = By.XPath("//input[@value='Add']");

        private readonly By CancelButton = By.XPath("//input[@value='Cancel']");

        private readonly By UpdateButton = By.XPath("//input[@value='Update']");

       private string GetEducationRowXPath(string country, string university, string title, string degree,string graduationYear)
        {
            return
                $"//tbody/tr[" +
                $"td[1][normalize-space()='{country}'] and " +
                $"td[2][normalize-space()='{university}'] and " +
                $"td[3][normalize-space()='{title}'] and " +
                $"td[4][normalize-space()='{degree}'] and " +
                $"td[5][normalize-space()='{graduationYear}']]";
        }

        private By GetEducationRowLocator(string country, string university, string title, string degree, string graduationYear)
        {
            return By.XPath(
                GetEducationRowXPath(
                    country,
                    university,
                    title,
                    degree,
                    graduationYear));
        }

        private By GetEducationEditButtonLocator(string country, string university, string title, string degree, string graduationYear)
        {
            return By.XPath(
                GetEducationRowXPath(
                    country,
                    university,
                    title,
                    degree,
                    graduationYear)
                + "//i[contains(@class,'write')]");
        }

        private By GetEducationDeleteButtonLocator(string country, string university, string title, string degree, string graduationYear)
        {
            return By.XPath(
                GetEducationRowXPath(
                    country,
                    university,
                    title,
                    degree,
                    graduationYear)
                + "//i[contains(@class,'remove')]");
        }

        private readonly By EducationRows =
            By.XPath("//div[@data-tab='third' and contains(@class,'active')]//tbody/tr");

        private readonly By EducationValidationMessage =
            By.XPath("//*[normalize-space()='Please enter all the fields']");

        private readonly By EducationInvalidMessage =
            By.XPath("//*[normalize-space()='Education information was invalid']");


        public EducationPage(IWebDriver driver)
        {
            _driver = driver;

            _wait = new WebDriverWait(
                _driver,
                TimeSpan.FromSeconds(30));
        }


        public void ClickEducationTab()
        {
            var educationTab =
                _wait.Until(
                    ExpectedConditions.ElementToBeClickable(EducationTab));

            educationTab.Click();
        }


        public void ClickAddNewButton()
        {
            var addNewButton =
                _wait.Until(
                    ExpectedConditions.ElementToBeClickable(AddNewButton));

            addNewButton.Click();
        }


        public void EnterUniversity(string university)
        {
            var universityField =
                _wait.Until(
                    ExpectedConditions.ElementIsVisible(UniversityField));

            universityField.Clear();
            universityField.SendKeys(university);
        }


        public void SelectCountry(string country)
        {
            var countryElement =
                _wait.Until(
                    ExpectedConditions.ElementIsVisible(CountryDropdown));

            var selectCountry = new SelectElement(countryElement);

            selectCountry.SelectByText(country);
        }


        public void SelectTitle(string title)
        {
            var titleElement =
                _wait.Until(
                    ExpectedConditions.ElementIsVisible(TitleDropdown));

            var selectTitle = new SelectElement(titleElement);

            selectTitle.SelectByText(title);
        }


        public void EnterDegree(string degree)
        {
            var degreeField =
                _wait.Until(
                    ExpectedConditions.ElementIsVisible(DegreeField));

            degreeField.Clear();
            degreeField.SendKeys(degree);
        }


        public void SelectGraduationYear(string graduationYear)
        {
            var yearElement =
                _wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        GraduationYearDropdown));

            var selectYear = new SelectElement(yearElement);

            selectYear.SelectByText(graduationYear);
        }


        public void ClickAddButton()
        {
            var addButton =
                _wait.Until(
                    ExpectedConditions.ElementToBeClickable(AddButton));

            addButton.Click();
        }


        public void ClickCancelButton()
        {
            var cancelButton =
                _wait.Until(
                    ExpectedConditions.ElementToBeClickable(CancelButton));

            cancelButton.Click();
        }


        public void AddEducation(string university, string country, string title,string degree,string graduationYear)
        {
            Console.WriteLine("Add Education Full method");

            ClickAddNewButton();

            EnterUniversity(university);
            SelectCountry(country);
            SelectTitle(title);
            EnterDegree(degree);
            SelectGraduationYear(graduationYear);

            ClickAddButton();
        }

        //click edit button
        public void ClickEditEducation(string country, string university, string title, string degree, string graduationYear)
        {
            var editButton = _wait.Until(ExpectedConditions.ElementToBeClickable(
                    GetEducationEditButtonLocator(country, university, title, degree, graduationYear)));

            editButton.Click();
        }

        //click update button
        public void ClickUpdateButton()
        {
            var updateButton = _wait.Until(
                ExpectedConditions.ElementToBeClickable(UpdateButton));

            updateButton.Click();
        }

        //update edit form
        public void UpdateEducation(string currentCountry,string currentUniversity,string currentTitle,string currentDegree,string currentGraduationYear,
            string updatedUniversity,string updatedCountry,string updatedTitle,string updatedDegree,string updatedGraduationYear)
        {
            ClickEditEducation(currentCountry,currentUniversity, currentTitle, currentDegree, currentGraduationYear);

            EnterUniversity(updatedUniversity);
            SelectCountry(updatedCountry);
            SelectTitle(updatedTitle);
            EnterDegree(updatedDegree);
            SelectGraduationYear(updatedGraduationYear);

            ClickUpdateButton();
        }

        public void DeleteEducation(string country, string university, string title, string degree, string graduationYear) 
        {
            var deleteButton = _wait.Until(ExpectedConditions.ElementToBeClickable(GetEducationDeleteButtonLocator(country,university, title, degree, graduationYear)));
            deleteButton.Click();
        
        }

        public void EditEducationAndCancel(string currentCountry, string currentUniversity, string currentTitle, string currentDegree, string currentGraduationYear,
            string updatedUniversity, string updatedCountry, string updatedTitle, string updatedDegree, string updatedGraduationYear)
        {
            ClickEditEducation(currentCountry, currentUniversity, currentTitle, currentDegree, currentGraduationYear);

            EnterUniversity(updatedUniversity);
            SelectCountry(updatedCountry);
            SelectTitle(updatedTitle);
            EnterDegree(updatedDegree);
            SelectGraduationYear(updatedGraduationYear);

            ClickCancelButton();
        }

        public void AttemptToAddEducation(string university, string country, string title, string degree, string graduationYear)
        {
            ClickAddNewButton();
            Console.WriteLine("Status uni spce 1" +string.IsNullOrEmpty(university));
            Console.WriteLine("Status degree spce 1" + string.IsNullOrEmpty(degree));
            if (!string.IsNullOrEmpty(university))
            {
                Console.WriteLine("Status uni spce 2 "+ !string.IsNullOrEmpty(university));
                EnterUniversity(university);
            }

            if (!string.IsNullOrEmpty(country))
            {
                SelectCountry(country);
            }

            if (!string.IsNullOrEmpty(title))
            {
                SelectTitle(title);
            }

            if (!string.IsNullOrEmpty(degree))
            {
                Console.WriteLine("Status degree spce 2" + !string.IsNullOrEmpty(degree));
                EnterDegree(degree);
            }

            if (!string.IsNullOrEmpty(graduationYear))
            {
                SelectGraduationYear(graduationYear);
            }

            ClickAddButton();

        }

        public int GetEducationTotalRowCount()
        {
            return _driver.FindElements(EducationRows).Count;
        }

        public bool IsEducationValidationMessageDisplayed()
        {
            try
            {
                var message = _wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        EducationValidationMessage));

                return message.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsEducationInvalidMessageDisplayed()
        {
            try
            {
                var message = _wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        EducationInvalidMessage));

                return message.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }


        public bool IsEducationDisplayed(string country, string university,string title,string degree,string graduationYear)
        {
            try
            {
                var educationRow = GetEducationRowLocator(country,university,title,degree,graduationYear);

                return _wait
                    .Until(ExpectedConditions.ElementIsVisible(educationRow))
                    .Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsEducationRemoved( string country, string university, string title, string degree, string graduationYear)
        {
            var educationRow = GetEducationRowLocator(country,university,title,degree,graduationYear);

            try
            {
                return _wait.Until(
                    ExpectedConditions.InvisibilityOfElementLocated(educationRow));
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void DeleteEducationIfExists(string country,string university, string title, string degree, string graduationYear)
        {
            var deleteButton = GetEducationDeleteButtonLocator(country,university,title, degree,graduationYear);

            var elements = _driver.FindElements(deleteButton);

            Console.WriteLine("Elementcount : "+elements.Count());


            if (elements.Count > 0)
            {
                elements[0].Click();
            }
        }

        public bool IsEducationAlreadyExistMessageDisplayed()
        {
            try
            {
                var message = By.XPath(
                    "//*[normalize-space()='This information is already exist.']"
                );

                return _wait
                    .Until(ExpectedConditions.ElementIsVisible(message))
                    .Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public int GetEducationRowCount(string country, string university, string title,string degree,string graduationYear)
        {
            var educationRow = GetEducationRowLocator(
                country,
                university,
                title,
                degree,
                graduationYear);

            return _driver.FindElements(educationRow).Count;
        }


        public void ClearUniversity()
        {
            //Find University field
            var universityInput =
                _wait.Until(ExpectedConditions.ElementIsVisible(UniversityField));

            //Click inside it
            universityInput.Click();

            //Select all existing text
            universityInput.SendKeys(Keys.Control + "a");

            //Delete selected text
            universityInput.SendKeys(Keys.Backspace);

            //Wait until the field value is actually empty
            _wait.Until(driver =>
                string.IsNullOrEmpty(universityInput.GetAttribute("value"))
            );
        }

       /* public string GetUniversityValue()
        {
            var universityInput =
                _wait.Until(ExpectedConditions.ElementIsVisible(UniversityField));

            return universityInput.GetAttribute("value") ?? "";
        }*/

        public void ClearDegree()
        {
            var degreeInput =
                _wait.Until(ExpectedConditions.ElementIsVisible(DegreeField));

            degreeInput.Click();
            degreeInput.SendKeys(Keys.Control + "a");
            degreeInput.SendKeys(Keys.Backspace);

            _wait.Until(driver =>
                string.IsNullOrEmpty(degreeInput.GetAttribute("value"))
            );
        }

        private void SelectDefaultOption(By dropdownLocator)
        {
            var dropdownElement =
                _wait.Until(ExpectedConditions.ElementIsVisible(dropdownLocator));

            var selectElement = new SelectElement(dropdownElement);

            selectElement.SelectByIndex(0);
        }

        public void AttemptToUpdateEducation(
    string currentCountry,
    string currentUniversity,
    string currentTitle,
    string currentDegree,
    string currentGraduationYear,
    string updatedUniversity,
    string updatedCountry,
    string updatedTitle,
    string updatedDegree,
    string updatedGraduationYear)

        {
            ClickEditEducation(currentCountry, currentUniversity, currentTitle, currentDegree, currentGraduationYear);

            if (string.IsNullOrEmpty(updatedUniversity))
            {
                ClearUniversity();
            }
            else
            {
                EnterUniversity(updatedUniversity);
            }

            if (string.IsNullOrEmpty(updatedCountry))
            {
                SelectDefaultOption(CountryDropdown);
            }
            else
            {
                SelectCountry(updatedCountry);
            }

            if (string.IsNullOrEmpty(updatedTitle))
            {
                SelectDefaultOption(TitleDropdown);
            }
            else
            {
                SelectTitle(updatedTitle);
            }

            if (string.IsNullOrEmpty(updatedDegree))
            {
                ClearDegree();
            }
            else
            {
                EnterDegree(updatedDegree);
            }

            if (string.IsNullOrEmpty(updatedGraduationYear))
            {
                SelectDefaultOption(GraduationYearDropdown);
            }
            else
            {
                SelectGraduationYear(updatedGraduationYear);
            }

            //Console.WriteLine( $"University value before Update: '{GetUniversityValue()}'");

            ClickUpdateButton();
        }
    }
}