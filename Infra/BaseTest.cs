using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace SeleniumDotNetCoreSample
{
   public class BaseTest
    {
        /// <summary>
        /// This methdod is used to do a one time setup before any tests run by configuring the filepath's and creating 
        /// result directory
        /// </summary>
        [OneTimeSetUp]
        public static void ExecutionSetUp()
        {
            TestConfigurationBuilder.BuildConfiguration();
            new FileUtil().SetFilePath();
        }
        //  [SetUp]
        public void SetUp()
        {
            //  TODO

        }
        /// <summary>
        /// This is a test tear down that gets called after each test and does the cleanup and reporting
        /// activity for the specific test.
        /// </summary>
        [TearDown]
        public void Teardown()
        {
         //  lock (this)
           {
                String testName = TestContext.CurrentContext.Test.Name;
                String message = TestContext.CurrentContext.Result.Message;
                String result = TestContext.CurrentContext.Result.Outcome.Status.ToString();
                DateTime endTime = DateTime.Now;
                TestControl tc = GlobalTestController.GetTestControl(testName);
                IWebDriver driver = tc.Driver;
                DateTime startTime = tc.TestStartTime;
                TimeSpan testDuration = endTime.Subtract(startTime).Duration();
                String screenShotFilePath = "";
                if (result.Contains("Fail"))
                {
                    tc.CaptureScreenShot(tc);
                    screenShotFilePath = tc.ScreenShotFileName;
                }
                GlobalTestController.AddLog(testName + "\t;" + message + "\t\t\t\t;" + result + "\t\t\t;" + startTime + "\t\t\t;" + endTime + ";" + testDuration + ";" + screenShotFilePath);
                tc.TearDown();
                GlobalTestController.RemoveTestControl(tc, testName);
           }
        }
        /// <summary>
        /// This is a one time tear down and gets triggered once after all the tests are run.
        /// This method is  used for global reporting such as HTML reports/email summary.
        /// </summary>
       [OneTimeTearDown]
        public static void TearDown()
        {
            FileUtil util = new FileUtil();
            util.CreateReport();
        }
    }

}