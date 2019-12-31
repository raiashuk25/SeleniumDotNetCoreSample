using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;

namespace SeleniumDotNetCoreSample
{
   public  static class WebDriverUtil
    {
        /// <summary>
        /// This method gets the webdriver  based on the browser type enum
        /// </summary>
        /// <param name="browserType"></param>
        /// <returns driver></returns>
        public static IWebDriver GetDriver(BrowserType browserType)
        {
            //Based on the configuarion file values specified for the 
            // Execution type as local or Remote corresoinding drivers get called.
            if (TestConfigurationBuilder.frameworkConfiguration.LocalExecution)
            {
                return GetLocalDriver( browserType);
            }else if (TestConfigurationBuilder.frameworkConfiguration.RemoteExecution)
            {
                string hubUrl = TestConfigurationBuilder.frameworkConfiguration.HubURL;
                return GetRemoteDriver(hubUrl,browserType);
            }
            else
            {
                throw new InvalidTestEnvironmentConfigurationException("The test run environment is not selected. Please selct either local exution or Remote execution as True.");
            }
        }
        /// <summary>
        /// This method gets the remote webdriver based on the desired configuration 
        /// </summary>
        /// <returns></returns>
        public static IWebDriver GetRemoteDriver(string hubUrl,BrowserType browserType)
        {
            // Gets the corresponding driver based on the value and the hub url specified.
            // It will throw an not implemented exception for any other browsers.
            IWebDriver driver=null;
            switch (browserType)           
            {
                case BrowserType.Chrome:
                   ChromeOptions options = new ChromeOptions();
                    driver= new RemoteWebDriver(new Uri(hubUrl), options.ToCapabilities(), TimeSpan.FromSeconds(120));
                    break;
                case BrowserType.IE:
                    InternetExplorerOptions internetExplorerOptions = new InternetExplorerOptions();
                    driver = new RemoteWebDriver(new Uri(hubUrl), internetExplorerOptions.ToCapabilities(), TimeSpan.FromSeconds(120));
                    break;
                default:
                     throw new NotImplementedException();  
            }
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            return driver;           
        }

        /// <summary>
        /// This method gets the locak webdriver based on the browser type enum
        /// </summary>
        /// <param name="browserType"></param>
        /// <returns></returns>
        private static IWebDriver GetLocalDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions options = new ChromeOptions();
                  //  options.AddArgument("no - sandbox");
                    IWebDriver driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory,options,TimeSpan.FromSeconds(120));
                    driver.Manage().Window.Maximize();
                    driver.Manage().Cookies.DeleteAllCookies();
                    return driver;
                    break;
                case BrowserType.IE:
                    InternetExplorerOptions internetExplorerOptions = new InternetExplorerOptions();
                    internetExplorerOptions.EnsureCleanSession = true;
                    internetExplorerOptions.IgnoreZoomLevel = true;
                    IWebDriver ieDriver=new InternetExplorerDriver(AppDomain.CurrentDomain.BaseDirectory, internetExplorerOptions);
                    ieDriver.Manage().Window.Maximize();
                    return ieDriver;
                    break;
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                    break;
                default:
                    throw new InvalidBrowserTypeException("Browser type selected is not valid" + browserType);
            }
            
        }

        /// <summary>
        /// This method returns the browser type enum
        /// </summary>
        /// <param name="browser"></param>
        /// <returns BrowserType></returns>
        public static BrowserType GetBrowserType(String browser)
        {
            BrowserType browserType;

            switch (browser)
            {
                case "Chrome":
                    browserType = BrowserType.Chrome;
                    break;
                case "IE":
                    browserType = BrowserType.IE;
                    break;
                case "Firefox":
                    browserType = BrowserType.Firefox;
                    break;
                default:
                    throw new InvalidBrowserTypeException("Invalid browser selection " + browser);
            }
            return browserType;
        }

    }
}
