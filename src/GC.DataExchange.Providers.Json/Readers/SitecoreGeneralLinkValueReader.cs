using System;
using Sitecore.DataExchange.DataAccess;

namespace GC.DataExchange.Providers.Json.Readers
{
    public class SitecoreGeneralLinkValueReader : IValueReader
    {
        public string Format { get; private set; }

        public SitecoreGeneralLinkValueReader(string format)
        {
            this.Format = !string.IsNullOrEmpty(format) ? format : "<link linktype=\"external\" url=\"{0}\" anchor=\"\" target=\"\" />";
        }

        public ReadResult Read(object source, DataAccessContext context)
        {
            var readResult = new ReadResult(DateTime.UtcNow)
            {
                WasValueRead = false
            };

            if (!(source is string url)) return readResult;
            
            readResult.ReadValue = string.Format(this.Format, url);
            readResult.WasValueRead = true;
            return readResult;
        }
    }
}
