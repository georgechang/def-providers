using GC.DataExchange.Providers.Json.Readers;
using Sitecore.DataExchange;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.Json.Converters
{
    [SupportedIds(JsonValueReaderTemplateId)]
    public class JsonValueReaderConverter : BaseItemModelConverter<IValueReader>
    {
        public const string JsonValueReaderTemplateId = "{8A6A1293-7166-4455-B12B-379EB3B81EDB}";
        public JsonValueReaderConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override ConvertResult<IValueReader> ConvertSupportedItem(ItemModel source)
        {
            var jsonPath = this.GetStringValue(source, "JSON Path");

            if (string.IsNullOrEmpty(jsonPath))
            {
                this.NegativeResult(source, "This field requires a value.", "field: JSON Path");
            }

            return this.PositiveResult(new JsonValueReader(jsonPath));
        }
    }
}
