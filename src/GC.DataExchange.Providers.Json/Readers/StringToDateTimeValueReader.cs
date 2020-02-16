using Sitecore.DataExchange.DataAccess;
using System;

namespace GC.DataExchange.Providers.Json.Readers
{
    public class StringToDateTimeValueReader : IValueReader
    {
        public ReadResult Read(object source, DataAccessContext context)
        {
            return !(source is string sourceString) || !DateTime.TryParse(sourceString, out var sourceDate) ? ReadResult.NegativeResult(DateTime.Now) : ReadResult.PositiveResult(sourceDate, DateTime.Now);
        }
    }
}
