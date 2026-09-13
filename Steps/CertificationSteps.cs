using NUnit.Framework;
using Reqnroll;
using qa_dotnet_cucumber.Helpers;
using qa_dotnet_cucumber.Pages;
using qa_dotnet_cucumber.Context;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class CertificationSteps
    {
        private readonly CertificationPage _certificationPage;
        private readonly TestDataContext _testDataContext;

        public CertificationSteps(
            CertificationPage certificationPage,
            TestDataContext testDataContext)
        {
            _certificationPage = certificationPage;
            _testDataContext = testDataContext;
        }

        [Given("I am on the Certification tab")]
        public void GivenIAmOnTheCertificationTab()
        {
            _certificationPage.ClickCertificationTab();
        }

        [Given("Certification from \"(.*)\" does not exist")]
        public void GivenCertificationFromDoesNotExist(string dataKey)
        {
            var certification =
                JsonDataReader.GetCertificationData(dataKey);

            _certificationPage.DeleteCertificationIfExists(
                certification.Certificate,
                certification.CertifiedFrom,
                certification.Year);

            bool isRemoved =
                _certificationPage.IsCertificationRemoved(
                    certification.Certificate,
                    certification.CertifiedFrom,
                    certification.Year);

            Assert.That(
                isRemoved,
                Is.True,
                $"Leftover Certification '{certification.Certificate}' should not exist before the test.");
        }

        [When("I add Certification using \"(.*)\"")]
        public void WhenIAddCertificationUsing(string dataKey)
        {
            var certification =
                JsonDataReader.GetCertificationData(dataKey);

            _certificationPage.AddCertification(
                certification.Certificate,
                certification.CertifiedFrom,
                certification.Year);

            _testDataContext.CreatedCertifications.Add(certification);
        }

        [Then("the Certification from \"(.*)\" should be displayed")]
        public void ThenTheCertificationFromShouldBeDisplayed(string dataKey)
        {
            var certification =
                JsonDataReader.GetCertificationData(dataKey);

            bool isDisplayed =
                _certificationPage.IsCertificationDisplayed(
                    certification.Certificate,
                    certification.CertifiedFrom,
                    certification.Year);

            Assert.That(
                isDisplayed,
                Is.True,
                $"Certification '{certification.Certificate}' should be displayed.");
        }

        [Given("Certification from \"(.*)\" exists")]
        public void GivenCertificationFromExists(string dataKey)
        {
            var certification =
                JsonDataReader.GetCertificationData(dataKey);

            _certificationPage.DeleteCertificationIfExists(
                certification.Certificate,
                certification.CertifiedFrom,
                certification.Year);

            _certificationPage.AddCertification(
                certification.Certificate,
                certification.CertifiedFrom,
                certification.Year);

            _testDataContext.CreatedCertifications.Add(certification);

            bool isDisplayed =
                _certificationPage.IsCertificationDisplayed(
                    certification.Certificate,
                    certification.CertifiedFrom,
                    certification.Year);

            Assert.That(
                isDisplayed,
                Is.True,
                $"Certification '{certification.Certificate}' should exist before the test.");
        }


        [When("I update Certification from \"(.*)\" using \"(.*)\"")]
        public void WhenIUpdateCertificationFromUsing(string existingDataKey, string updatedDataKey)
        {
            var existingCertification =
                JsonDataReader.GetCertificationData(existingDataKey);

            var updatedCertification =
                JsonDataReader.GetCertificationData(updatedDataKey);

            _certificationPage.UpdateCertification(
                existingCertification.Certificate,
                existingCertification.CertifiedFrom,
                existingCertification.Year,

                updatedCertification.Certificate,
                updatedCertification.CertifiedFrom,
                updatedCertification.Year);

            _testDataContext.CreatedCertifications.Add(updatedCertification);
        }

        [Then("the Certification from \"(.*)\" should not be displayed")]
        public void ThenTheCertificationFromShouldNotBeDisplayed(string dataKey)
        {
            var certification =
                JsonDataReader.GetCertificationData(dataKey);

            bool isRemoved =
                _certificationPage.IsCertificationRemoved(
                    certification.Certificate,
                    certification.CertifiedFrom,
                    certification.Year);

            Assert.That(
                isRemoved,
                Is.True,
                $"Certification '{certification.Certificate}' should not be displayed.");
        }


        [When("I edit Certification from \"(.*)\" using \"(.*)\" and cancel the changes")]
        public void WhenIEditCertificationFromUsingAndCancelTheChanges(string existingDataKey, string updatedDataKey)
        {
            var existingCertification =
                JsonDataReader.GetCertificationData(existingDataKey);

            var updatedCertification =
                JsonDataReader.GetCertificationData(updatedDataKey);

            _certificationPage.EditCertificationAndCancel(
                existingCertification.Certificate,
                existingCertification.CertifiedFrom,
                existingCertification.Year,

                updatedCertification.Certificate,
                updatedCertification.CertifiedFrom,
                updatedCertification.Year);
        }

        [When("I delete Certification from \"(.*)\"")]
        public void WhenIDeleteCertificationFrom(string dataKey)
        {
            var certification =
                JsonDataReader.GetCertificationData(dataKey);

            _certificationPage.DeleteCertification(
                certification.Certificate,
                certification.CertifiedFrom,
                certification.Year);
        }


        [Then("the duplicate Certification message should be displayed")]
        public void ThenTheDuplicateCertificationMessageShouldBeDisplayed()
        {
            bool isDisplayed =
                _certificationPage.IsCertificationDuplicateMessageDisplayed();

            Assert.That(
                isDisplayed,
                Is.True,
                "Expected duplicate Certification message 'This information is already exist.' to be displayed.");
        }

        [Then("only one Certification from \"(.*)\" should be displayed")]
        public void ThenOnlyOneCertificationFromShouldBeDisplayed(string dataKey)
        {
            var certification =
                JsonDataReader.GetCertificationData(dataKey);

            int rowCount =
                _certificationPage.GetCertificationRowCount(
                    certification.Certificate,
                    certification.CertifiedFrom,
                    certification.Year);

            Assert.That(
                rowCount,
                Is.EqualTo(1),
                $"Expected only one Certification '{certification.Certificate}', but found {rowCount}.");
        }

        [When("I cancel the certification edit")]
        public void WhenICancelTheCertificationEdit()
        {
            _certificationPage.ClickCancelButton();
        }


        [When("I attempt to add Certification using \"(.*)\"")]
        public void WhenIAttemptToAddCertificationUsing(string dataKey)
        {
            var certification =
                JsonDataReader.GetCertificationData(dataKey);

            //save the table row count before the action
            _testDataContext.CertificationRowCountBeforeAction =
                _certificationPage.GetCertificationTotalRowCount();

            _certificationPage.AttemptToAddCertification(
                certification.Certificate,
                certification.CertifiedFrom,
                certification.Year);

            _testDataContext.CreatedCertifications.Add(certification);
        }

        [Then("the Certification required-fields message should be displayed")]
        public void ThenTheCertificationRequiredFieldsMessageShouldBeDisplayed()
        {
            bool isDisplayed =
                _certificationPage.IsCertificationRequiredFieldsMessageDisplayed();

            Assert.That(
                isDisplayed,
                Is.True,
                "Expected Certification required-fields validation message to be displayed.");
        }

        [Then("no Certification record should be created")]
        public void ThenNoCertificationRecordShouldBeCreated()
        {
            //after table row count
            int rowCountAfterAction =
                _certificationPage.GetCertificationTotalRowCount();

            Console.WriteLine("Row count BEFORE : " + _testDataContext.CertificationRowCountBeforeAction);
            Console.WriteLine("Row count AFTER : " + rowCountAfterAction);

            Assert.That(
                rowCountAfterAction,
                Is.EqualTo(_testDataContext.CertificationRowCountBeforeAction),
                $"Certification row count should remain " +
                $"{_testDataContext.CertificationRowCountBeforeAction}, " +
                $"but found {rowCountAfterAction}.");
        }
    }
}