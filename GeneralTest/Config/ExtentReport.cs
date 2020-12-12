using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using GeneralTest.Defines;
using GeneralTest.PageMethods;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GeneralTest.Config
{
    class ExtentReport
    {

        public String chromeDriverPath = $"{Helper.getRootPath()}packages\\Selenium.Chrome.WebDriver.85.0.0\\driver\\win32";
        ExtentReports extent;
        ExtentTest test;
        IWebDriver seleDriver;

        [OneTimeSetUp]
        public void setup()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            var reportPath = projectPath + "Reports/ExtentReport.html";
            var htmlReport = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReport);
        }

        [SetUp]
        public void beforeTest()
        {
            seleDriver = new ChromeDriver();
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            initEnvironmentVeriable();
        }

        [TearDown]
        public void afterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;
            string screenShotPath = Capture(TestContext.CurrentContext.Test.Name);
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    test.Log(Status.Fail, "Fail");
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            //seleDriver.Quit();
            test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath));
            extent.Flush();
        }
        public  string Capture( string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)seleDriver;
            Screenshot screenshot = ts.GetScreenshot();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpth = Helper.getRootPath()+ "GeneralTest\\Assets\\screenshots\\" + screenShotName + ".png";
            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath);
            return localpath;
        }

        public IWebDriver getDriver()
        {
            return seleDriver;
        }

        // LOG TO REPORT
        public void logInfo(String content)
        {
            test.Log(Status.Info, content);
        }

        public void logPass(String content)
        {
            test.Log( Status.Pass,content);
        }
        public void logFail(String content)
        {
            test.Log(Status.Fail, content);
        }
        public void initEnvironmentVeriable()
        {
            System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", chromeDriverPath);
        }
    }
}
