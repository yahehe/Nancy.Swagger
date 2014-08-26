using Newtonsoft.Json;

namespace Nancy.Swagger.Annotations.Tests.Testdata.JsonNetEnricher
{
    [JsonObject(Description = "Description of a model")]
    public class JsonNetEnricherModel
    {
        [JsonProperty("named")]
        public string Named { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public string RequiredAllowNull { get; set; }

        [JsonProperty(Required = Required.Default)]
        public string RequiredDefault { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string RequiredAlways { get; set; }
    }
}