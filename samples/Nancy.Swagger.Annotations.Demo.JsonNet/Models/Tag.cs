using System;
using Newtonsoft.Json;

namespace Nancy.Swagger.Annotations.Demo.JsonNet.Models
{
    public class Tag
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public String Name { get; set; }
    }
}