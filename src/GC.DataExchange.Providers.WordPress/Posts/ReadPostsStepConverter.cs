using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.WordPress.Posts
{
    [SupportedIds(ReadPostStepTemplateId)]
    public class ReadPostsStepConverter : BasePipelineStepConverter
    {
        public const string ReadPostStepTemplateId = "{BDA4F18C-C791-4E90-AF5C-DF0A1735BA65}";
        public ReadPostsStepConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            var settings = new EndpointSettings
            {
                EndpointFrom = this.ConvertReferenceToModel<Sitecore.DataExchange.Models.Endpoint>(source, "EndpointFrom")
            };

            pipelineStep.AddPlugin(settings);
        }
    }
}
