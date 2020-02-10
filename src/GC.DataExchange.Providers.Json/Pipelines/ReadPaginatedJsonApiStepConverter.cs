using System.Web;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.Json.Pipelines
{
    [SupportedIds(ReadPaginatedJsonApiStepTemplateId)]
    public class ReadPaginatedJsonApiStepConverter : BasePipelineStepConverter
    {
        public const string ReadPaginatedJsonApiStepTemplateId = "{7DB65221-D742-4AB1-B04E-46A88FBE087F}";
        public ReadPaginatedJsonApiStepConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            var settings = new EndpointSettings()
            {
                EndpointFrom = this.ConvertReferenceToModel<Sitecore.DataExchange.Models.Endpoint>(source, "Endpoint"),
            };

            pipelineStep.AddPlugin(settings);

            var pipelineSettings = new ReadPaginatedJsonApiStepSettings()
            {
                MaxCount = this.GetIntValue(source, "Max Count"),
                QueryParameters = HttpUtility.ParseQueryString(this.GetStringValue(source, "Query Parameters")),
                Page = this.GetStringValue(source, "Page"),
                Offset = this.GetStringValue(source, "Offset"),
                ResultsPerPage = this.GetStringValue(source, "Results Per Page")
            };

            pipelineStep.AddPlugin(pipelineSettings);
        }
    }
}
