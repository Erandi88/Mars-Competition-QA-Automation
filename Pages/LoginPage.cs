using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Locators
        private readonly By SignInLink = By.XPath("//a[normalize-space()='Sign In']");
        private readonly By EmailField = By.Name("email");
        private readonly By PasswordField = By.Name("password");
        private readonly By LoginButton = By.XPath("//button[normalize-space()='Login']");
        private readonly By SignOutButton = By.XPath("//button[normalize-space()='Sign Out']");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        public void ClickSignIn()
        {
            var signInLink = _wait.Until(ExpectedConditions.ElementToBeClickable(SignInLink));

            signInLink.Click();
        }

        public void EnterEmail(string email)
        {
            var emailField = _wait.Until(ExpectedConditions.ElementIsVisible(EmailField));

            emailField.Clear();
            emailField.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            var passwordField = _wait.Until(ExpectedConditions.ElementIsVisible(PasswordField));

            passwordField.Clear();
            passwordField.SendKeys(password);
        }

        public void ClickLogin()
        {
            var loginButton = _wait.Until(ExpectedConditions.ElementToBeClickable(LoginButton));

            loginButton.Click();
        }

        public void Login(string email, string password)
        {
            ClickSignIn();
            EnterEmail(email);
            EnterPassword(password);
            ClickLogin();
        }

        public bool IsLoggedIn()
        {
            try
            {
                return _wait
                    .Until(ExpectedConditions.ElementIsVisible(SignOutButton)).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}