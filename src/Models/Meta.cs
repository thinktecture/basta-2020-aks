using System;
using System.Net;

namespace Thinktecture.AKS.Sample.Models
{
    public class Meta
    {
        public string HostName => Dns.GetHostName();
        public string OperatingSystem => Environment.OSVersion.Platform.ToString();
        public string OperatingSystemVersion => Environment.OSVersion.VersionString;
    }
}
