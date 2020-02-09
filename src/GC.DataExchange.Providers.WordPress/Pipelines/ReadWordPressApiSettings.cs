using Sitecore.DataExchange;

namespace GC.DataExchange.Providers.WordPress.Pipelines
{
    public class ReadWordPressApiSettings : IPlugin
    {
        public string ApiRoute { get; set; }
    }
}
