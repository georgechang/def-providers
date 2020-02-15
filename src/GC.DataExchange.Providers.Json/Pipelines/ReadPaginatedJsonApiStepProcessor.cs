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
using Sitecore.Services.Core.Diagnostics;

namespace GC.DataExchange.Providers.Json.Pipelines
{
    [RequiredEndpointPlugins(typeof(JsonApiSettings))]
    [RequiredPipelineStepPlugins((typeof(ReadPaginatedJsonApiStepSettings)))]
    public class ReadPaginatedJsonApiStepProcessor : BaseReadDataStepProcessor
    {
        private static readonly HttpClient Client = new HttpClient();

        protected override void ReadData(Sitecore.DataExchange.Models.Endpoint endpoint, PipelineStep pipelineStep, PipelineContext pipelineContext, ILogger logger)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }
            if (pipelineStep == null)
            {
                throw new ArgumentNullException(nameof(pipelineStep));
            }
            if (pipelineContext == null)
            {
                throw new ArgumentNullException(nameof(pipelineContext));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            var endpointSettings = endpoint.GetPlugin<JsonApiSettings>();
            if (endpointSettings == null) return;

            if (string.IsNullOrEmpty(endpointSettings.ApiUrl))
            {
                logger.Error($"No API URL is specified on the endpoint. (pipeline step: { pipelineStep.Name }, endpoint: { endpoint.Name }");
            }

            var pipelineStepSettings = pipelineStep.GetPlugin<ReadPaginatedJsonApiStepSettings>();
            if (pipelineStepSettings == null) return;

            var uri = new UriBuilder(endpointSettings.ApiUrl);
            var query = HttpUtility.ParseQueryString(uri.Query);
            query["per_page"] = pipelineStepSettings.ResultsPerPage.ToString();
            uri.Query = query.ToString();

            var data = new List<JObject>();
            logger.Debug($"Executing API call { uri.Uri.AbsoluteUri }. ");
            var batch = GetDataAsync(uri.Uri.AbsoluteUri).GetAwaiter().GetResult().ToList();
            var page = pipelineStepSettings.Page;

            while (batch.Any())
            {
                data.AddRange(batch);
                page++;
                query["page"] = page.ToString();
                uri.Query = query.ToString();
                logger.Debug($"Executing API call { uri.Uri.AbsoluteUri }. ");
                batch = GetDataAsync(uri.Uri.AbsoluteUri).GetAwaiter().GetResult().ToList();
                logger.Debug($"Retrieved { batch.Count } items.");
            }

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
