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
        public const string ReadPaginatedJsonApiStepTemplateId = "{48444815-A3ED-416A-A9AE-2CD310DC747A}";
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
                //QueryParameters = HttpUtility.ParseQueryString(this.GetStringValue(source, "Query Parameters")),
                Page = this.GetIntValue(source, "Page"),
                Offset = this.GetIntValue(source, "Offset"),
                ResultsPerPage = this.GetIntValue(source, "Results Per Page")
            };

            pipelineStep.AddPlugin(pipelineSettings);
        }
    }
}
