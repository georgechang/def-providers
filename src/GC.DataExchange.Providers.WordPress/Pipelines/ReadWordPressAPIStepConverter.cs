using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.WordPress.Pipelines
{
    [SupportedIds(ReadWordPressApiStepTemplateId)]
    public class ReadWordPressApiStepConverter : BasePipelineStepConverter
    {
        public const string ReadWordPressApiStepTemplateId = "{7DB65221-D742-4AB1-B04E-46A88FBE087F}";
        public ReadWordPressApiStepConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            var settings = new EndpointSettings()
            {
                EndpointFrom = this.ConvertReferenceToModel<Sitecore.DataExchange.Models.Endpoint>(source, "Endpoint"),
            };

            pipelineStep.AddPlugin(settings);

            var stepSettings = new ReadWordPressApiSettings
            {
                ApiRoute = this.GetStringValue(source, "API Route")
            };

            pipelineStep.AddPlugin(stepSettings);
        }
    }
}
