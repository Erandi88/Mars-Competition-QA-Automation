using AventStack.ExtentReports.Gherkin.Model;
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

        [Given("Education from \"(.*)\" exists")]
        public void GivenEducationFromExists(string dataKey)
        {
            var education = JsonDataReader.GetEducationData(dataKey);

            _educationPage.DeleteEducationIfExists(education.Country, education.University,education.Title,
                                                    education.Degree,education.GraduationYear);

            _educationPage.AddEducation(education.University, education.Country, education.Title, 
                                        education.Degree, education.GraduationYear);

            _testDataContext.CreatedEducations.Add(education);

            bool isDisplayed = _educationPage.IsEducationDisplayed(education.Country, education.University,
                                                    education.Title, education.Degree, education.GraduationYear);

            Assert.That(isDisplayed,Is.True,
                $"Education record '{education.University}' should exist before the edit.");
        }

        [When("I update Education from \"(.*)\" using \"(.*)\"")]
        public void WhenIUpdateEducationFromUsing(string existingDataKey,string updatedDataKey)
        {
            var existingEducation = JsonDataReader.GetEducationData(existingDataKey);

            var updatedEducation = JsonDataReader.GetEducationData(updatedDataKey);

            _educationPage.UpdateEducation(
                existingEducation.Country,
                existingEducation.University,
                existingEducation.Title,
                existingEducation.Degree,
                existingEducation.GraduationYear,

                updatedEducation.University,
                updatedEducation.Country,
                updatedEducation.Title,
                updatedEducation.Degree,
                updatedEducation.GraduationYear);

            _testDataContext.CreatedEducations.Add(updatedEducation);
        }

        [Then("the Education from \"(.*)\" should not be displayed")]
        public void ThenTheEducationFromShouldNotBeDisplayed(string dataKey)
        {
            var education =
                JsonDataReader.GetEducationData(dataKey);

            bool isRemoved =
                _educationPage.IsEducationRemoved(
                    education.Country,
                    education.University,
                    education.Title,
                    education.Degree,
                    education.GraduationYear);

            Assert.That(
                isRemoved,
                Is.True,
                $"Education record '{education.University}' should not be displayed.");
        }

        [When("I delete Education from \"(.*)\"")]
        public void WhenIDeleteEducationFrom(string dataKey)
        {
            var education = JsonDataReader.GetEducationData(dataKey);

            _educationPage.DeleteEducation(
                education.Country,
                education.University,
                education.Title,
                education.Degree,
                education.GraduationYear);
        }

        [When("I edit Education from \"(.*)\" using \"(.*)\" and cancel the changes")]
        public void WhenIEditEducationFromUsingAndCancelTheChanges(string existingDataKey,string updatedDataKey)
        {
            var existingEducation =
                JsonDataReader.GetEducationData(existingDataKey);

            var updatedEducation =
                JsonDataReader.GetEducationData(updatedDataKey);

            _educationPage.EditEducationAndCancel(
                existingEducation.Country,
                existingEducation.University,
                existingEducation.Title,
                existingEducation.Degree,
                existingEducation.GraduationYear,

                updatedEducation.University,
                updatedEducation.Country,
                updatedEducation.Title,
                updatedEducation.Degree,
                updatedEducation.GraduationYear);
        }

        [Then("the duplicate Education message should be displayed")]
        public void ThenTheDuplicateEducationMessageShouldBeDisplayed()
        {
            Assert.That(
                _educationPage.IsEducationAlreadyExistMessageDisplayed(),
                Is.True,
                "The language already exist message should be displayed."
            );
        }

        [Then("only one Education from \"(.*)\" should be displayed")]
        public void ThenOnlyOneEducationFromShouldBeDisplayed(string dataKey)
        {
            var education = JsonDataReader.GetEducationData(dataKey);

            int rowCount =
                _educationPage.GetEducationRowCount(
                    education.Country,
                    education.University,
                    education.Title,
                    education.Degree,
                    education.GraduationYear);

            Assert.That(
                rowCount,
                Is.EqualTo(1),
                $"Expected only one Education record '{education.University}', but found {rowCount}.");
        }

        [When("I cancel the education edit")]
        public void WhenICancelTheEducationEdit()
        {
            _educationPage.ClickCancelButton();
        }

        [When("I attempt to add Education using \"(.*)\"")]
        public void WhenIAttemptToAddEducationUsing(string dataKey)
        {
            var education =
                JsonDataReader.GetEducationData(dataKey);

            _testDataContext.EducationRowCountBeforeAction =
                _educationPage.GetEducationTotalRowCount();

            Console.WriteLine( "Education Row counter BEFORE : " + _educationPage.GetEducationTotalRowCount());

            _educationPage.AttemptToAddEducation(
                education.University,
                education.Country,
                education.Title,
                education.Degree,
                education.GraduationYear);

            _testDataContext.CreatedEducations.Add(education);
        }

        [Then("the Education validation message should be displayed")]
        public void ThenTheEducationValidationMessageShouldBeDisplayed()
        {
            bool isDisplayed =
                _educationPage.IsEducationValidationMessageDisplayed();

            Assert.That(
                isDisplayed,
                Is.True,
                "Expected Education validation message 'Please enter all the fields' to be displayed.");
        }

        [Then("no Education record should be created")]
        public void ThenNoEducationRecordShouldBeCreated()
        {
            int rowCountAfterAction =
                _educationPage.GetEducationTotalRowCount();

            Console.WriteLine("Education Row counter AFTER : " + rowCountAfterAction);

            Assert.That(
                rowCountAfterAction,
                Is.EqualTo(_testDataContext.EducationRowCountBeforeAction),
                $"Education row count should remain " +
                $"{_testDataContext.EducationRowCountBeforeAction}, " +
                $"but found {rowCountAfterAction}.");
        }


        [Then("the invalid Education message should be displayed")]
        public void ThenTheInvalidEducationMessageShouldBeDisplayed()
        {
            bool isDisplayed =
                _educationPage.IsEducationInvalidMessageDisplayed();

            Assert.That(
                isDisplayed,
                Is.True,
                "Expected message 'Education information was invalid' to be displayed.");
        }




    }
}