using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumDotNetCoreSample
{
    [TestFixture]
    [Parallelizable]
    class Test2  : BaseTest
    {
        
        [Test]
        [Property("TestType", "Smoke")]
        [TestCaseSource(typeof(CSVDataHandler), "GetCSVData", new object[] { "DataFile1" })]
        public void VerifySearchTShirt(Dictionary<string, string> dataObject)
        {
            TestControl tc = new TestControl().GetTestResources(dataObject);

        }

    }
}
