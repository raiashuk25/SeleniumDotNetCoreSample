using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumDotNetCoreSample
{
    [TestFixture]
    [Parallelizable]
    class TestClassOne :BaseTest
    {
        public By searchField = By.Id("search_query_top");
        public By seachButton = By.XPath("//button[@name='submit_search']");
        public By alertMessage = By.XPath("//div[@id='center_column']//p[@class='alert alert-warning']");

        [Test]
        public void abc()
        {
            TestControl tc = new TestControl().GetTestResources();
            tc.WebElementHelper.NavigateToURL(TestConfigurationBuilder.GetConfigurationValue("ApplicationSettings:URL"));

        }

        [Property("TestType", "Smoke")]
        [TestCaseSource(typeof(CSVDataHandler), "GetCSVData", new object[] { "DataFile1" })]
        public void VerifySearchTShirt(Dictionary<string, string> dataObject)
        {
            TestControl tc = new TestControl().GetTestResources(dataObject);
            tc.WebElementHelper.NavigateToURL(TestConfigurationBuilder.GetConfigurationValue("ApplicationSettings:URL"));
            tc.WebElementHelper.SelectByText(searchField, Guid.NewGuid().ToString());
            tc.WebElementHelper.Click(seachButton);
            string messageAfterClickingSearch = tc.WebElementHelper.GetText(alertMessage);
            Assert.That(messageAfterClickingSearch, Does.Contain("No results were found for your search"));
        }

    }
    [TestFixture]
    [Parallelizable]
    class TestClassTwo : BaseTest
    {
        public By searchField = By.Id("search_query_top");
        public By seachButton = By.XPath("//button[@name='submit_search']");
        public By alertMessage = By.XPath("//div[@id='center_column']//p[@class='alert alert-warning']");

        [Test]
        public void abc()
        {
            TestControl tc = new TestControl().GetTestResources();
            tc.WebElementHelper.NavigateToURL(TestConfigurationBuilder.GetConfigurationValue("ApplicationSettings:URL"));

        }

    }

}
