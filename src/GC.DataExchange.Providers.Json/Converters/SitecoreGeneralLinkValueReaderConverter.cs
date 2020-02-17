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
            var format = this.GetStringValue(source, "Format");
            return format == null ? this.NegativeResult(source, "Field value not specified", "field: Format") : this.PositiveResult(new SitecoreGeneralLinkValueReader(format));
        }
    }
}
