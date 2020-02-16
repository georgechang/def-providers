using System;
using System.Collections.Generic;
using Sitecore.DataExchange.DataAccess;

namespace GC.DataExchange.Providers.Json.Readers
{
    public class SitecoreReferenceFieldValueReader : IValueReader
    {
        public Dictionary<string, Guid> MappingDictionary { get; private set; }

        public SitecoreReferenceFieldValueReader(Dictionary<string, Guid> mappingDictionary)
        {
            MappingDictionary = mappingDictionary;
        }

        public ReadResult Read(object source, DataAccessContext context)
        {
            var readResult = new ReadResult(DateTime.UtcNow)
            {
                WasValueRead = false
            };

            if (!(source is string sourceId)) return readResult;

            readResult.ReadValue = MappingDictionary[sourceId];
            readResult.WasValueRead = true;
            return readResult;
        }
    }
}
