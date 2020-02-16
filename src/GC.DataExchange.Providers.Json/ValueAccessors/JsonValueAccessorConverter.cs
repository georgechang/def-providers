using GC.DataExchange.Providers.Json.Readers;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.DataAccess.ValueAccessors;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.Json.ValueAccessors
{
    [SupportedIds(JsonValueAccessorConverterTemplateId)]
    public class JsonValueAccessorConverter : ValueAccessorConverter
    {
        public const string JsonValueAccessorConverterTemplateId = "{C9361839-C516-4973-A533-3F52F7E1A244}";

        public JsonValueAccessorConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override IValueReader GetValueReader(ItemModel source)
        {
            var reader = base.GetValueReader(source);
            if (reader != null) return reader;

            var jsonPath = this.GetStringValue(source, "JSON Path");

            return string.IsNullOrEmpty(jsonPath) ? null : new JsonValueReader(jsonPath);
        }
    }
}
