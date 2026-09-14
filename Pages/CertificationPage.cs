using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{
    public class CertificationPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By CertificationTab =
            By.XPath("//a[normalize-space()='Certifications']");

        private readonly By AddNewButton = By.XPath(
        "//div[contains(@class,'active')]" +
        "//div[contains(@class,'ui teal button') and normalize-space()='Add New']");

        private readonly By CertificateField =
            By.XPath("//input[@placeholder='Certificate or Award']");

        private readonly By CertifiedFromField =
            By.XPath("//input[@placeholder='Certified From (e.g. Adobe)']");

        private readonly By YearDropdown =
            By.XPath("//select[@name='certificationYear']");

        private readonly By AddButton =
            By.XPath("//input[@value='Add']");

        private readonly By CancelButton =
            By.XPath("//input[@value='Cancel']");

        private readonly By UpdateButton =
            By.XPath("//input[@value='Update']");

        //error msg  - "This information is already exist."
        private readonly By CertificationDuplicateMessage =
            By.XPath("//*[normalize-space()='This information is already exist.']");

        //validation mes - "Please enter Certification Name, Certification From and Certification Year"
        private readonly By CertificationRequiredFieldsMessage =By.XPath(
            "//*[normalize-space()='Please enter Certification Name, Certification From and Certification Year']");

        private string GetCertificationRowXPath(string certificate,string certifiedFrom,string year)
        {
            return
                $"//tbody/tr[" +
                $"td[1][normalize-space()='{certificate}'] and " +
                $"td[2][normalize-space()='{certifiedFrom}'] and " +
                $"td[3][normalize-space()='{year}']]";
        }

        //convert GetCertificationRowXPath into a Selenium locator
        private By GetCertificationRowLocator(string certificate,string certifiedFrom, string year)
        {
            return By.XPath(
                GetCertificationRowXPath(certificate,certifiedFrom,year));
        }

        //delete btn locator
        private By GetCertificationDeleteButtonLocator(string certificate,string certifiedFrom,string year)
        {
            return By.XPath(
                GetCertificationRowXPath(
                    certificate,
                    certifiedFrom,
                    year)
                + "//i[contains(@class,'remove')]");
        }

        //edit button locator
        private By GetCertificationEditButtonLocator(string certificate, string certifiedFrom, string year)
        {
            return By.XPath(
                GetCertificationRowXPath(
                    certificate,
                    certifiedFrom,
                    year)
                + "//i[contains(@class,'write')]");
        }

        //get the locator for certificate table row
        private readonly By CertificationRows =
            By.XPath("//div[@data-tab='fourth' and contains(@class,'active')]//tbody/tr");

        public CertificationPage(IWebDriver driver)
        {
            _driver = driver;

            _wait = new WebDriverWait(
                _driver,
                TimeSpan.FromSeconds(30));
        }

        public void ClickCertificationTab()
        {
            var certificationTab =
                _wait.Until(ExpectedConditions.ElementToBeClickable(CertificationTab));

            certificationTab.Click();
        }

        public void ClickAddNewButton()
        {
            var addNewButton =
                _wait.Until(ExpectedConditions.ElementToBeClickable( AddNewButton));

            addNewButton.Click();
        }

        public void EnterCertificate(string certificate)
        {
            var certificateInput =
                _wait.Until(ExpectedConditions.ElementIsVisible(CertificateField));

            certificateInput.Clear();
            certificateInput.SendKeys(certificate);
        }

        public void EnterCertifiedFrom(string certifiedFrom)
        {
            var certifiedFromInput =
                _wait.Until( ExpectedConditions.ElementIsVisible(CertifiedFromField));

            certifiedFromInput.Clear();
            certifiedFromInput.SendKeys(certifiedFrom);
        }

        public void SelectYear(string year)
        {
            var yearDropdown =
                _wait.Until(ExpectedConditions.ElementIsVisible(YearDropdown));

            var selectElement = new SelectElement(yearDropdown);

            selectElement.SelectByText(year);
        }

        public void ClickAddButton()
        {
            var addButton =
                _wait.Until(ExpectedConditions.ElementToBeClickable(AddButton));

            addButton.Click();
        }

        public void AddCertification(string certificate, string certifiedFrom, string year)
        {
            ClickAddNewButton();

            EnterCertificate(certificate);
            EnterCertifiedFrom(certifiedFrom);
            SelectYear(year);

            ClickAddButton();
        }

        public bool IsCertificationDisplayed(string certificate, string certifiedFrom, string year)
        {
            try
            {
                var certificationRow =
                    GetCertificationRowLocator(certificate, certifiedFrom, year);

                return _wait.Until(
                    ExpectedConditions.ElementIsVisible(certificationRow)).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void DeleteCertificationIfExists(string certificate, string certifiedFrom, string year)
        {
            var deleteButton =
                GetCertificationDeleteButtonLocator( certificate, certifiedFrom, year);

            var elements =
                _driver.FindElements(deleteButton);

            if (elements.Count > 0)
            {
                elements[0].Click();
            }
        }

        public bool IsCertificationRemoved(string certificate,string certifiedFrom,string year)
        {
            var certificationRow =
                GetCertificationRowLocator(certificate,certifiedFrom,year);

            try
            {
                return _wait.Until(
                    ExpectedConditions.InvisibilityOfElementLocated(certificationRow));
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        //click edit btn
        public void ClickEditCertification(string certificate,string certifiedFrom,string year)
        {
            var editButton =
                _wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        GetCertificationEditButtonLocator(certificate,certifiedFrom,year)));

            editButton.Click();
        }

        //click update btn
        public void ClickUpdateButton()
        {
            var updateButton =
                _wait.Until(
                    ExpectedConditions.ElementToBeClickable(UpdateButton));

            updateButton.Click();
        }

        //edit method
        public void UpdateCertification(string currentCertificate, string currentCertifiedFrom, string currentYear,
                string updatedCertificate, string updatedCertifiedFrom, string updatedYear)
        {
            ClickEditCertification(currentCertificate, currentCertifiedFrom, currentYear);

            EnterCertificate(updatedCertificate);
            EnterCertifiedFrom(updatedCertifiedFrom);
            SelectYear(updatedYear);

            ClickUpdateButton();
        }

        //cancel method
        public void ClickCancelButton()
        {
            var cancelButton =
                _wait.Until(ExpectedConditions.ElementToBeClickable(CancelButton));

            cancelButton.Click();
        }

        //edit and cancel
        public void EditCertificationAndCancel(string currentCertificate, string currentCertifiedFrom,string currentYear,
            string updatedCertificate, string updatedCertifiedFrom, string updatedYear)
        {
            ClickEditCertification(currentCertificate, currentCertifiedFrom, currentYear);

            EnterCertificate(updatedCertificate);
            EnterCertifiedFrom(updatedCertifiedFrom);
            SelectYear(updatedYear);

            ClickCancelButton();
        }

        //delete method
        public void DeleteCertification(string certificate, string certifiedFrom, string year)
        {
            var deleteButton =
                _wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        GetCertificationDeleteButtonLocator(certificate, certifiedFrom, year)));

            deleteButton.Click();
        }

        //check if the information already exist is displayed
        public bool IsCertificationDuplicateMessageDisplayed()
        {
            try
            {
                var message = _wait.Until(
                    ExpectedConditions.ElementIsVisible(CertificationDuplicateMessage));

                return message.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        //get certificate row count
        public int GetCertificationRowCount(string certificate, string certifiedFrom, string year)
        {
            var certificationRow =
                GetCertificationRowLocator(
                    certificate,
                    certifiedFrom,
                    year);

            return _driver.FindElements(certificationRow).Count;
        }

        //get table row count
        public int GetCertificationTotalRowCount()
        {
            return _driver.FindElements(CertificationRows).Count;
        }

        public void AttemptToAddCertification(string certificate, string certifiedFrom, string year)
        {
            ClickAddNewButton();

            if (!string.IsNullOrEmpty(certificate))
            {
                EnterCertificate(certificate);
            }

            if (!string.IsNullOrEmpty(certifiedFrom))
            {
                EnterCertifiedFrom(certifiedFrom);
            }

            if (!string.IsNullOrEmpty(year))
            {
                SelectYear(year);
            }

            ClickAddButton();
        }

        //Please enter Certification Name, Certification From and Certification Year
        public bool IsCertificationRequiredFieldsMessageDisplayed()
        {
            try
            {
                var message = _wait.Until(
                    ExpectedConditions.ElementIsVisible( CertificationRequiredFieldsMessage));

                return message.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        //clear certificate field 
        public void ClearCertificate()
        {
            var certificateInput =
                _wait.Until(ExpectedConditions.ElementIsVisible(CertificateField));

            certificateInput.Click();
            certificateInput.SendKeys(Keys.Control + "a");
            certificateInput.SendKeys(Keys.Backspace);

            _wait.Until(driver =>
                string.IsNullOrEmpty(certificateInput.GetAttribute("value")));
        }

        //clear certificate from field
        public void ClearCertifiedFrom()
        {
            var certifiedFromInput =
                _wait.Until(ExpectedConditions.ElementIsVisible(CertifiedFromField));

            certifiedFromInput.Click();
            certifiedFromInput.SendKeys(Keys.Control + "a");
            certifiedFromInput.SendKeys(Keys.Backspace);

            _wait.Until(driver =>
                string.IsNullOrEmpty(certifiedFromInput.GetAttribute("value")));
        }

        //set the default value for dropdown
        public void SelectDefaultYear()
        {
            var yearDropdown =
                _wait.Until(ExpectedConditions.ElementIsVisible(YearDropdown));

            var selectElement = new SelectElement(yearDropdown);

            selectElement.SelectByIndex(0);
        }

        public void AttemptToUpdateCertification(string currentCertificate, string currentCertifiedFrom, string currentYear,
                string updatedCertificate, string updatedCertifiedFrom, string updatedYear)
        {
            ClickEditCertification(currentCertificate, currentCertifiedFrom, currentYear);

            if (string.IsNullOrEmpty(updatedCertificate))
            {
                ClearCertificate();
            }
            else
            {
                EnterCertificate(updatedCertificate);
            }

            if (string.IsNullOrEmpty(updatedCertifiedFrom))
            {
                ClearCertifiedFrom();
            }
            else
            {
                EnterCertifiedFrom(updatedCertifiedFrom);
            }

            if (string.IsNullOrEmpty(updatedYear))
            {
                SelectDefaultYear();
            }
            else
            {
                SelectYear(updatedYear);
            }

            ClickUpdateButton();
        }
    }
}