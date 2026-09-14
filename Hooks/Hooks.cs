using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using qa_dotnet_cucumber.Config;
using qa_dotnet_cucumber.Context;
using qa_dotnet_cucumber.Pages;
using Reqnroll;
using Reqnroll.BoDi;
using System.IO;
using System.Text.Json;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace qa_dotnet_cucumber.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private static ExtentReports? _extent;
        private static ExtentSparkReporter? _htmlReporter;
        private static TestSettings _settings;
        private ExtentTest? _test;
        private static readonly object _reportLock = new object();

        public static TestSettings Settings => _settings;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            
            string currentDir = Directory.GetCurrentDirectory();

            string localSettingsPath = Path.Combine(currentDir, "settings.local.json");
            string defaultSettingsPath = Path.Combine(currentDir, "settings.json");

            string settingsPath = File.Exists(localSettingsPath)
                ? localSettingsPath
                : defaultSettingsPath;

            string json = File.ReadAllText(settingsPath);

            _settings = JsonSerializer.Deserialize<TestSettings>(json)
                ?? throw new InvalidOperationException(
                    $"Unable to load settings from {settingsPath}");

            Console.WriteLine($"Using settings file: {Path.GetFileName(settingsPath)}");

            // Get project root by navigating up from bin/Debug/net8.0
            //string projectRoot = Path.GetFullPath(Path.Combine(currentDir, "..", ".."));
            string projectRoot = Path.GetFullPath(Path.Combine(currentDir, "..", "..", ".."));
            string reportFileName = _settings.Report.Path.TrimStart('/'); // e.g., "TestReport.html"
            string reportPath = Path.Combine(projectRoot, reportFileName);

            _htmlReporter = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(_htmlReporter);
            _extent.AddSystemInfo("Environment", _settings.Environment.BaseUrl);
            _extent.AddSystemInfo("Browser", _settings.Browser.Type);
            Console.WriteLine($"BeforeTestRun started at {DateTime.Now}, Report Path: {reportPath}");
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Console.WriteLine($"Starting {scenarioContext.ScenarioInfo.Title} on Thread {Thread.CurrentThread.ManagedThreadId} at {DateTime.Now}");
            new DriverManager().SetUpDriver(new ChromeConfig());
            var chromeOptions = new ChromeOptions();
            if (_settings.Browser.Headless)
            {
                chromeOptions.AddArgument("--headless");
            }
            var driver = new ChromeDriver(chromeOptions);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_settings.Browser.TimeoutSeconds);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
            driver.Manage().Window.Maximize();

            _objectContainer.RegisterInstanceAs<IWebDriver>(driver);
            _objectContainer.RegisterInstanceAs(new NavigationHelper(driver));
            _objectContainer.RegisterInstanceAs(new LoginPage(driver));
            _objectContainer.RegisterInstanceAs(new EducationPage(driver));
            _objectContainer.RegisterInstanceAs(new TestDataContext());
            _objectContainer.RegisterInstanceAs(new CertificationPage(driver));

            lock (_reportLock)
            {
                _test = _extent!.CreateTest(scenarioContext.ScenarioInfo.Title);
            }
            Console.WriteLine($"Created test: {scenarioContext.ScenarioInfo.Title} on Thread {Thread.CurrentThread.ManagedThreadId} at {DateTime.Now}");
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepText = scenarioContext.StepContext.StepInfo.Text;
            lock (_reportLock)
            {
                if (scenarioContext.TestError == null)
                {
                    _test!.Log(Status.Pass, $"{stepType} {stepText}");
                    Console.WriteLine($"Logged pass: {stepType} {stepText} on Thread {Thread.CurrentThread.ManagedThreadId} at {DateTime.Now}");
                }
                else
                {
                    var driver = _objectContainer.Resolve<IWebDriver>();
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    var screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), $"Screenshot_{DateTime.Now.Ticks}_{Thread.CurrentThread.ManagedThreadId}.png");
                    screenshot.SaveAsFile(screenshotPath);
                    _test!.Log(Status.Fail, $"{stepType} {stepText}", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                    Console.WriteLine($"Logged fail with screenshot: {screenshotPath} on Thread {Thread.CurrentThread.ManagedThreadId} at {DateTime.Now}");
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _objectContainer.Resolve<IWebDriver>();

            try
            {
                var testDataContext = _objectContainer.Resolve<TestDataContext>();

                var educationPage = _objectContainer.Resolve<EducationPage>();

                var certificationPage = _objectContainer.Resolve<CertificationPage>();

                CleanupEducation(testDataContext, educationPage);

                CleanupCertifications(testDataContext, certificationPage);
            }
            finally
            {
                driver.Quit();

                Console.WriteLine(
                    "Browser closed after scenario.");
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            lock (_reportLock)
            {
                Console.WriteLine("AfterTestRun executed - Flushing report to: " + _settings.Report.Path + " at " + DateTime.Now);
                _extent!.Flush();
            }
        }

        //Helper methods for clean up
        private void CleanupEducation(TestDataContext testDataContext,EducationPage educationPage)
        {
            foreach (var education in testDataContext.CreatedEducations)
            {
                educationPage.DeleteEducationIfExists(
                    education.Country,
                    education.University,
                    education.Title,
                    education.Degree,
                    education.GraduationYear);

                bool isRemoved =
                    educationPage.IsEducationRemoved(
                        education.Country,
                        education.University,
                        education.Title,
                        education.Degree,
                        education.GraduationYear);

                Console.WriteLine(
                    isRemoved
                        ? $"Education cleanup successful: {education.University}"
                        : $"Education cleanup warning: Could not remove {education.University}");
            }
        }

        private void CleanupCertifications(TestDataContext testDataContext, CertificationPage certificationPage)
        {
            foreach (var certification in testDataContext.CreatedCertifications)
            {
                certificationPage.DeleteCertificationIfExists(
                    certification.Certificate,
                    certification.CertifiedFrom,
                    certification.Year);

                bool isRemoved =
                    certificationPage.IsCertificationRemoved(
                        certification.Certificate,
                        certification.CertifiedFrom,
                        certification.Year);

                Console.WriteLine(
                    isRemoved
                        ? $"Certification cleanup successful: {certification.Certificate}"
                        : $"Certification cleanup warning: Could not remove {certification.Certificate}");
            }
        }
    }
}