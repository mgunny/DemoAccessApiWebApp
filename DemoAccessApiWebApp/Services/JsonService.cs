using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace DemoAccessApiWebApp.Services
{
    /// <summary>
    /// Json Helper Class to serialise object to Json string and vice versa
    /// </summary>
    public class JsonService : IJsonService
    {
        /// <summary>
        /// De-serialise json string to specified object
        /// </summary>
        public T FromJson<T>(string json) => JsonConvert.DeserializeObject<T>(json, Settings);

        /// <summary>
        /// Serialise a specified object to a json string
        /// </summary>
        public string ToJson<T>(T objectToSerialize) => JsonConvert.SerializeObject(objectToSerialize, Settings);

        /// <summary>
        /// Serialize specified object to Json StringContent
        /// </summary>
        public StringContent ToJsonStringContent<T>(T objectToSerialize)
        {
            string jsonString = JsonConvert.SerializeObject(objectToSerialize, Settings);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return content;
        }

        /// <summary>
        /// Set up default values for the json serialiser
        /// </summary>
        private readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = { new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal } }
        };
    }

}