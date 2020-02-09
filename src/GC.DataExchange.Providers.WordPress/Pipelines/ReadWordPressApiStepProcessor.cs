using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GC.DataExchange.Providers.WordPress.Endpoint;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Processors.PipelineSteps;
using Sitecore.Services.Core.Diagnostics;

namespace GC.DataExchange.Providers.WordPress.Pipelines
{
    [RequiredEndpointPlugins(typeof(WordPressSettings))]
    [RequiredPipelineStepPlugins(typeof(ReadWordPressApiSettings))]
    public class ReadWordPressApiStepProcessor : BaseReadDataStepProcessor
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

            var settings = endpoint.GetPlugin<WordPressSettings>();
            if (settings == null) return;

            if (string.IsNullOrEmpty(settings.HostUrl))
            {
                logger.Error($"No Host URL is specified on the endpoint. (pipeline step: { pipelineStep.Name }, endpoint: { endpoint.Name }");
            }

            var pipelineSettings = pipelineStep.GetPlugin<ReadWordPressApiSettings>();
            if (pipelineSettings == null) return;

            if (string.IsNullOrEmpty(pipelineSettings.ApiRoute))
            {
                logger.Error($"No API Route is specified on the pipeline step. (pipeline step: { pipelineStep.Name }, endpoint: { endpoint.Name }");
            }

            var apiRoute = pipelineSettings.ApiRoute.StartsWith("/") ? pipelineSettings.ApiRoute : $"/{pipelineSettings.ApiRoute}";
            var data = GetDataAsync($"{settings.HostUrl}{apiRoute}").GetAwaiter().GetResult();
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
