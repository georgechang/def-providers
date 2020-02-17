using Sitecore.DataExchange.DataAccess;
using System;
using System.Collections.Generic;

namespace GC.DataExchange.Providers.Json.Readers
{
    public class CompositeValueReader : IValueReader
    {
        private IEnumerable<IValueReader> Readers { get; }

        public CompositeValueReader(IEnumerable<IValueReader> readers)
        {
            this.Readers = readers;
        }

        public ReadResult Read(object source, DataAccessContext context)
        {
            var result = ReadResult.NegativeResult(DateTime.UtcNow);
            var resultValue = source;

            foreach (var reader in this.Readers)
            {
                result = reader.Read(resultValue, new DataAccessContext());

                if (!result.WasValueRead)
                {
                    result = ReadResult.NegativeResult(DateTime.UtcNow);
                    break;
                }

                resultValue = result.ReadValue;
            }

            return result;
        }
    }
}
