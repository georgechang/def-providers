using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using GC.DataExchange.Providers.Json.Endpoint;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Processors.PipelineSteps;
using Sitecore.Diagnostics;
using Sitecore.Services.Core.Diagnostics;

namespace GC.DataExchange.Providers.Json.Pipelines
{
    // this processor needs the JSON API endpoint configuration values
    [RequiredEndpointPlugins(typeof(JsonApiSettings))]
    // this processor needs the processor step configuration values
    [RequiredPipelineStepPlugins((typeof(ReadPaginatedJsonApiStepSettings)))]
    public class ReadPaginatedJsonApiStepProcessor : BaseReadDataStepProcessor
    {
        private static readonly HttpClient Client = new HttpClient();

        protected override void ReadData(Sitecore.DataExchange.Models.Endpoint endpoint, PipelineStep pipelineStep, PipelineContext pipelineContext, ILogger logger)
        {
            // do some null checks
            Assert.ArgumentNotNull(endpoint, nameof(endpoint));
            Assert.ArgumentNotNull(pipelineStep, nameof(pipelineStep));
            Assert.ArgumentNotNull(pipelineContext, nameof(pipelineContext));
            Assert.ArgumentNotNull(logger, nameof(logger));

            // get the configuration values for the endpoint
            var endpointSettings = endpoint.GetPlugin<JsonApiSettings>();
            if (endpointSettings == null) return;

            if (string.IsNullOrEmpty(endpointSettings.ApiUrl))
            {
                logger.Error($"No API URL is specified on the endpoint. (pipeline step: { pipelineStep.Name }, endpoint: { endpoint.Name }");
            }

            // get the configuration values for the processor step
            var pipelineStepSettings = pipelineStep.GetPlugin<ReadPaginatedJsonApiStepSettings>();
            if (pipelineStepSettings == null) return;

            logger.Debug("Executing pipeline step: ", $"MaxCount: { pipelineStepSettings.MaxCount }", $"ResultsPerPage: { pipelineStepSettings.ResultsPerPage }", $"Page: { pipelineStepSettings.Page }", $"Offset: { pipelineStepSettings.Offset }");

            // execute the API to retrieve the data
            var uri = new UriBuilder(endpointSettings.ApiUrl);
            var query = HttpUtility.ParseQueryString(uri.Query);
            query["per_page"] = pipelineStepSettings.ResultsPerPage.ToString();
            uri.Query = query.ToString();

            var data = new List<JObject>();
            logger.Debug($"Executing API call { uri.Uri.AbsoluteUri }. ");
            var batch = GetDataAsync(uri.Uri.AbsoluteUri).GetAwaiter().GetResult().ToList();
            var page = pipelineStepSettings.Page;

            while (batch.Any() && (pipelineStepSettings.MaxCount< 0 || pipelineStepSettings.MaxCount > 0 && data.Count < pipelineStepSettings.MaxCount))
            {
                data.AddRange(batch);
                page++;
                query["page"] = page.ToString();
                uri.Query = query.ToString();
                logger.Debug($"Executing API call { uri.Uri.AbsoluteUri }. ");
                batch = GetDataAsync(uri.Uri.AbsoluteUri).GetAwaiter().GetResult().ToList();
                logger.Debug($"Retrieved { batch.Count } items.");
            }

            // add this data as a plugin to the pipeline context
            var dataSettings = new IterableDataSettings(data);
            pipelineContext.AddPlugin(dataSettings);
        }

        private async Task<IEnumerable<JObject>> GetDataAsync(string url)
        {
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<JObject>>(body);
        }
    }
}
