using GC.DataExchange.Providers.Json.Readers;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.DataAccess.ValueAccessors;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.Json.ValueAccessors
{
    [SupportedIds(JsonValueAccessorTemplateId)]
    public class JsonValueAccessorConverter : ValueAccessorConverter
    {
        public const string JsonValueAccessorTemplateId = "{B6321E01-C334-43E4-887E-5859DF4A28F8}";
        public JsonValueAccessorConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override IValueReader GetValueReader(ItemModel source)
        {
            var reader = base.GetValueReader(source);
            if (reader != null) return reader;
            
            var name = this.GetStringValue(source, "Property");
            reader = new JsonValueReader(name);
            return reader;
        }
    }
}
