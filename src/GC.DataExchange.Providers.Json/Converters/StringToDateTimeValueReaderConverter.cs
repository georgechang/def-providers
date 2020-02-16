using GC.DataExchange.Providers.Json.Readers;
using Sitecore.DataExchange;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.Json.Converters
{
    [SupportedIds(DateTimeValueReaderTemplateId)]
    public class StringToDateTimeValueReaderConverter : BaseItemModelConverter<IValueReader>
    {
        public const string DateTimeValueReaderTemplateId = "{0A2E2D72-CA72-4C68-81E6-85DF8B4EF276}";
        public StringToDateTimeValueReaderConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override ConvertResult<IValueReader> ConvertSupportedItem(ItemModel source)
        {
            return this.PositiveResult(new StringToDateTimeValueReader());
        }
    }
}
