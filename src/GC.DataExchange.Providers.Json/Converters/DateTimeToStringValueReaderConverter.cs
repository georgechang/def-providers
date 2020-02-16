using GC.DataExchange.Providers.Json.Readers;
using Sitecore.DataExchange;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.Json.Converters
{
    [SupportedIds(DateTimeToStringValueReaderTemplateId)]
    public class DateTimeToStringValueReaderConverter : BaseItemModelConverter<IValueReader>
    {
        public const string DateTimeToStringValueReaderTemplateId = "{9208EC7C-8E04-495A-855B-6855D507F565}";
        public DateTimeToStringValueReaderConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override ConvertResult<IValueReader> ConvertSupportedItem(ItemModel source)
        {
            var format = this.GetStringValue(source, "Format");
            return string.IsNullOrEmpty(format) ? this.NegativeResult(source, "A format string must be specified.", "field: Format") : this.PositiveResult(new DateTimeToStringValueReader(format));
        }
    }
}
