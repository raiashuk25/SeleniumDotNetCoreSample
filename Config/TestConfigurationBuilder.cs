using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.Extensions.Configuration.Json;


namespace SeleniumDotNetCoreSample
{
    public static class TestConfigurationBuilder
    {
        public static IConfiguration Configuration;
        public static FrameworkConfiguration frameworkConfiguration;
        /// <summary>
        /// This method loads the test configuration 
        /// </summary>
        public static void BuildConfiguration()
        {
            String FrameworkConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "FrameworkConfig.json");
            String AppConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "ApplicationConfig.json");
            Configuration = new ConfigurationBuilder().AddJsonFile(FrameworkConfigFilePath)
                .AddJsonFile(AppConfigFilePath)
                .Build();
            frameworkConfiguration = new FrameworkConfiguration();
            Configuration.Bind("FrameworkSettings", frameworkConfiguration);
        }

        /// <summary>
        /// This method returns the configuarion value for the corresponding key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static dynamic GetConfigurationValue(String key)
        {
            return Configuration[key];
        }

    }
}
