using System;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Sitecore.DataExchange.DataAccess;

namespace GC.DataExchange.Providers.Json.Readers
{
    public class JsonValueReader : IValueReader
    {
        public string PropertyName { get; private set; }

        public JsonValueReader(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentOutOfRangeException(nameof(propertyName), (object)propertyName, "Property name must be specified.");
            this.PropertyName = propertyName;
        }

        public ReadResult Read(object source, DataAccessContext context)
        {
            var readResult = new ReadResult(DateTime.UtcNow)
            {
                WasValueRead = false
            };

            if (!(source is JObject jsonObject)) return readResult;
            
            var property = jsonObject.Properties().FirstOrDefault(x => x.Name == this.PropertyName);
            if (property == null) return readResult;

            readResult.WasValueRead = true;
            readResult.ReadValue = HttpUtility.HtmlDecode((string)property.Value);
            return readResult;
        }
    }
}
