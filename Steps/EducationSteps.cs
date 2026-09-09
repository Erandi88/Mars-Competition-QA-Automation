using NUnit.Framework;
using qa_dotnet_cucumber.Context;
using qa_dotnet_cucumber.Helpers;
using qa_dotnet_cucumber.Pages;
using Reqnroll;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class EducationSteps
    {
        private readonly EducationPage _educationPage;
        private readonly TestDataContext _testDataContext;

        public EducationSteps(EducationPage educationPage, TestDataContext testDataContext)
        {
            _educationPage = educationPage;
            _testDataContext = testDataContext;

        }

        [Given("I am on the Education tab")]
        public void GivenIAmOnTheEducationTab()
        {
            _educationPage.ClickEducationTab();
        }

        [Given("Education from \"(.*)\" does not exist")]
        public void GivenEducationFromDoesNotExist(string dataKey)
        {
            var education = JsonDataReader.GetEducationData(dataKey);

            _educationPage.DeleteEducationIfExists( education.Country, education.University, education.Title,
                education.Degree, education.GraduationYear);

            bool isRemoved = _educationPage.IsEducationRemoved(
                    education.Country,
                    education.University,
                    education.Title,
                    education.Degree,
                    education.GraduationYear);

            Assert.That(
                isRemoved,
                Is.True,
                $"Leftover Education record '{education.University}' should not exist before the test.");
        }

        [When("I add Education using \"(.*)\"")]
        public void WhenIAddEducationUsing(string dataKey)
        {
            var education =
                JsonDataReader.GetEducationData(dataKey);

            _educationPage.AddEducation(
                education.University,
                education.Country,
                education.Title,
                education.Degree,
                education.GraduationYear);

            _testDataContext.CreatedEducations.Add(education);
        }

        [Then("the Education from \"(.*)\" should be displayed")]
        public void ThenTheEducationFromShouldBeDisplayed(string dataKey)
        {
            var education =
                JsonDataReader.GetEducationData(dataKey);

            bool isDisplayed =
                _educationPage.IsEducationDisplayed(
                    education.Country,
                    education.University,
                    education.Title,
                    education.Degree,
                    education.GraduationYear);

            Assert.That(
                isDisplayed,
                Is.True,
                $"Education record '{education.University}' should be displayed.");
        }
    }
}