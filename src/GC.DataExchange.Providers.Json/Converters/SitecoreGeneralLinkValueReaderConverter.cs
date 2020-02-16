using GC.DataExchange.Providers.Json.Readers;
using Sitecore.DataExchange;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.Json.Converters
{
    [SupportedIds(SitecoreGeneralLinkValueReaderTemplateId)]
    public class SitecoreGeneralLinkValueReaderConverter : BaseItemModelConverter<IValueReader>
    {
        public const string SitecoreGeneralLinkValueReaderTemplateId = "{AFC56763-0A1E-4E3E-9ED3-137D92C1919E}";

        public SitecoreGeneralLinkValueReaderConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override ConvertResult<IValueReader> ConvertSupportedItem(ItemModel source)
        {
            var reader = this.GetValueReader(source);
            return reader == null ? this.NegativeResult(source, "Unable to get a value reader for the source item.") : this.PositiveResult(reader);
        }

        protected virtual IValueReader GetValueReader(ItemModel source)
        {
            var format = this.GetStringValue(source, "Format");
            return format == null ? null : new SitecoreGeneralLinkValueReader(format);
        }
    }
}
