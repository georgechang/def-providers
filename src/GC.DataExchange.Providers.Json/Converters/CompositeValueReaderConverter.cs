using GC.DataExchange.Providers.Json.Readers;
using Sitecore.DataExchange;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.Json.Converters
{
    [SupportedIds(CompositeValueReaderTemplateId)]
    public class CompositeValueReaderConverter : BaseItemModelConverter<IValueReader>
    {
        public const string CompositeValueReaderTemplateId = "{2A468299-1262-4C53-8A77-3DF205D1E05F}";

        public CompositeValueReaderConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override ConvertResult<IValueReader> ConvertSupportedItem(ItemModel source)
        {
            var readers = this.ConvertReferencesToModels<IValueReader>(source, "Value Readers");
            var reader = new CompositeValueReader(readers);
            return this.PositiveResult(reader);
        }
    }
}
