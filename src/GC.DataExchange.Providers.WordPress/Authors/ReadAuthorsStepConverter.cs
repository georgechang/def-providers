using Sitecore.Services.Core.Model;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Repositories;

namespace GC.DataExchange.Providers.WordPress.Authors
{
    public class ReadAuthorsStepConverter : BasePipelineStepConverter
    {
        public ReadAuthorsStepConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
        }
    }
}
