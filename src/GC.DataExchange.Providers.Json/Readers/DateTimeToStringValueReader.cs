using Sitecore.DataExchange.DataAccess;
using System;

namespace GC.DataExchange.Providers.Json.Readers
{
    public class DateTimeToStringValueReader : IValueReader
    {
        public string Format { get; private set; }

        public DateTimeToStringValueReader(string format)
        {
            this.Format = format;
        }

        public ReadResult Read(object source, DataAccessContext context)
        {
            return !(source is DateTime sourceDateTime) ? ReadResult.NegativeResult(DateTime.Now) : ReadResult.PositiveResult(sourceDateTime.ToString(this.Format), DateTime.Now);
        }
    }
}
