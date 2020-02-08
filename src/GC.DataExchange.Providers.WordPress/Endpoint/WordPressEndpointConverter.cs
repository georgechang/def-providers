using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.WordPress.Endpoint
{
    [SupportedIds(WordPressEndpointTemplateId)]
    public class WordPressEndpointConverter : BaseEndpointConverter
    {
        public const string WordPressEndpointTemplateId = "{4FA3CC53-44DA-47C9-BB39-9FC5B33C23E8}";
        public WordPressEndpointConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override void AddPlugins(ItemModel source, Sitecore.DataExchange.Models.Endpoint endpoint)
        {
            var settings = new WordPressSettings
            {
                HostUrl = this.GetStringValue(source, "Host URL")
            };

            endpoint.AddPlugin(settings);
        }
    }
}
