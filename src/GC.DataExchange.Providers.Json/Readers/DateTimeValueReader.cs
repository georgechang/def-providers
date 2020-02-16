using Sitecore.DataExchange.DataAccess;
using System;

namespace GC.DataExchange.Providers.Json.Readers
{
    public class DateTimeValueReader : IValueReader
    {
        public ReadResult Read(object source, DataAccessContext context)
        {
            var readResult = new ReadResult(DateTime.UtcNow)
            {
                WasValueRead = false
            };

            if (!(source is string sourceString) || !DateTime.TryParse(sourceString, out var sourceDate)) return ReadResult.NegativeResult(DateTime.Now);
            
            readResult.ReadValue = sourceDate.ToString("yyyyMMddTHHmmssZ");
            readResult.WasValueRead = true;

            return ReadResult.PositiveResult(readResult, DateTime.Now);
        }
    }
}
