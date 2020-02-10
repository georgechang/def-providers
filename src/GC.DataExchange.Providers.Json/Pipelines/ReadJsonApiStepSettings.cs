using System.Collections.Specialized;
using Sitecore.DataExchange;

namespace GC.DataExchange.Providers.Json.Pipelines
{
    public class ReadJsonApiStepSettings : IPlugin
    {
        public int MaxCount { get; set; }
        public NameValueCollection QueryParameters { get; set; }
    }
}
