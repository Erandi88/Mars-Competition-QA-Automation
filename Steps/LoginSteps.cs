using NUnit.Framework;
using Reqnroll;
using qa_dotnet_cucumber.Pages;
using TestHooks = qa_dotnet_cucumber.Hooks.Hooks;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage _loginPage;
        private readonly NavigationHelper _navigationHelper;

        public LoginSteps(LoginPage loginPage,NavigationHelper navigationHelper)
        {
            _loginPage = loginPage;
            _navigationHelper = navigationHelper;
        }

        [Given("I am on the Mars home page")]
        public void GivenIAmOnTheMarsHomePage()
        {
            _navigationHelper.NavigateTo("/Home");
        }

        [When("I log in with valid Mars credentials")]
        public void WhenILogInWithValidMarsCredentials()
        {
            string email = TestHooks.Settings.Credentials.Email;
            string password = TestHooks.Settings.Credentials.Password;

            _loginPage.Login(email, password);
        }

        [Then("I should be logged in to Mars")]
        public void ThenIShouldBeLoggedInToMars()
        {
            Assert.That(
                _loginPage.IsLoggedIn(),
                Is.True,
                "User should be successfully logged in to Mars");
        }

        [Given("I am logged in to Mars")]
        public void GivenIAmLoggedInToMars()
        {
            _navigationHelper.NavigateTo("/Home");

            string email = TestHooks.Settings.Credentials.Email;
            string password = TestHooks.Settings.Credentials.Password;

            _loginPage.Login(email, password);

            Assert.That(
                _loginPage.IsLoggedIn(),
                Is.True,
                "User should be successfully logged in to Mars");
        }
    }
}