using NUnit.Framework;

namespace SeleniumDotNetCoreSample
{
    public class FrameworkConfiguration
    {
        public string Browser { get; set; }
        public bool HtmlReport { get; set; }
        public bool LocalExecution { get; set; }
        public bool RemoteExecution { get; set; }
        public string HubURL { get; set; }
        public bool TextReport { get; set; }
        public string EmailRecepientList { get; set; }
        public int DriverWait { get; set; }
        public int MaxWait { get; set; }
        public int WaitFrequency { get; set; }
        public int NumberOfRetrys { get; set; }
    }
}
