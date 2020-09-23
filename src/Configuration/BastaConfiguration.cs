using System;
namespace Thinktecture.AKS.Sample.Configuration
{
    public class BastaConfiguration
    {
        public const string SECTION_NAME = "basta";

        public BastaConfiguration()
        {
        }

        public int ListLimit { get; set; }
    }
}
