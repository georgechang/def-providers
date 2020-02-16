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
            return !(source is string sourceId) || !MappingDictionary.ContainsKey(sourceId) ? ReadResult.NegativeResult(DateTime.UtcNow) : ReadResult.PositiveResult(MappingDictionary[sourceId], DateTime.UtcNow);
        }
    }
}
