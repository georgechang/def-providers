using System;
using System.Web;
using Newtonsoft.Json.Linq;
using Sitecore.DataExchange.DataAccess;

namespace GC.DataExchange.Providers.Json.Readers
{
    public class JsonValueReader : IValueReader
    {
        public string JsonPath { get; private set; }

        public JsonValueReader(string jsonPath)
        {
            if (string.IsNullOrWhiteSpace(jsonPath))
                throw new ArgumentOutOfRangeException(nameof(jsonPath), (object)jsonPath, "JSON path must be specified.");
            this.JsonPath = jsonPath;
        }

        public ReadResult Read(object source, DataAccessContext context)
        {
            if (!(source is JObject jsonObject)) return ReadResult.NegativeResult(DateTime.UtcNow);
            
            var property = jsonObject.SelectToken(this.JsonPath);
            return property == null ? ReadResult.NegativeResult(DateTime.UtcNow) : ReadResult.PositiveResult(HttpUtility.HtmlDecode(property.ToString()), DateTime.UtcNow);
        }
    }
}
