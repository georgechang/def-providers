using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.DataAccess.ValueAccessors;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.WordPress.Posts
{
    [SupportedIds(PostValueAccessorTemplateId)]
    public class PostValueAccessorConverter : ValueAccessorConverter
    {
        public const string PostValueAccessorTemplateId = "{B6321E01-C334-43E4-887E-5859DF4A28F8}";
        public PostValueAccessorConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override IValueReader GetValueReader(ItemModel source)
        {
            var reader = base.GetValueReader(source);
            if (reader == null)
            {

            }

            return reader;
        }
    }
}
