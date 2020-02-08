using System;
using System.Collections.Generic;
using GC.DataExchange.Providers.WordPress.Endpoint;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Processors.PipelineSteps;
using Sitecore.Services.Core.Diagnostics;

namespace GC.DataExchange.Providers.WordPress.Posts
{
    [RequiredEndpointPlugins(typeof(WordPressSettings))]
    public class ReadPostsStepProcessor : BaseReadDataStepProcessor
    {
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
                logger.Error($"No Host URL specified on the endpoint (pipeline step: { pipelineStep.Name }, endpoint: { endpoint.Name }");
                return;
            }

            var data = new List<string>() {"hello"};
            var dataSettings = new IterableDataSettings(data);

            pipelineContext.AddPlugin(dataSettings);
        }
    }
}
