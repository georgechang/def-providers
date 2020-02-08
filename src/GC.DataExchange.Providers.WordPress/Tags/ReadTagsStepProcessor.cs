using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Processors.PipelineSteps;
using Sitecore.Services.Core.Diagnostics;

namespace GC.DataExchange.Providers.WordPress.Tags
{
    public class ReadTagsStepProcessor : BaseReadDataStepProcessor
    {
        protected override void ReadData(Sitecore.DataExchange.Models.Endpoint endpoint, PipelineStep pipelineStep, PipelineContext pipelineContext, ILogger logger)
        {
        }
    }
}
