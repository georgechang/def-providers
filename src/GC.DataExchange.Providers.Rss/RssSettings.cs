using Sitecore.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.DataExchange.Providers.Rss
{
    public class RssSettings : IPlugin
    {
        public RssSettings()
        { 
        }

        public string Url { get; set; }
    }
}
