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
            var readResult = new ReadResult(DateTime.UtcNow)
            {
                WasValueRead = false
            };

            if (!(source is JObject jsonObject)) return readResult;
            
            var property = jsonObject.SelectToken(this.JsonPath);
            if (property == null) return readResult;

            readResult.WasValueRead = true;
            readResult.ReadValue = HttpUtility.HtmlDecode(property.ToString());
            return readResult;
        }
    }
}
