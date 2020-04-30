using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.Json.Endpoint
{
    // This converter is used with this specific template ID
    [SupportedIds(JsonApiEndpointTemplateId)]
    public class JsonApiEndpointConverter : BaseEndpointConverter
    {
        public const string JsonApiEndpointTemplateId = "{4FA3CC53-44DA-47C9-BB39-9FC5B33C23E8}";
        public JsonApiEndpointConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override void AddPlugins(ItemModel source, Sitecore.DataExchange.Models.Endpoint endpoint)
        {
            // convert the Sitecore item fields to a plugin object
            var settings = new JsonApiSettings
            {
                ApiUrl = this.GetStringValue(source, "API URL")
            };

            // add this plugin to the endpoint
            endpoint.AddPlugin(settings);
        }
    }
}
