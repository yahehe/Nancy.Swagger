using System;
using System.Collections.Generic;
using Nancy.Swagger.Annotations.Attributes;
using Newtonsoft.Json;

namespace Nancy.Swagger.Annotations.Demo.JsonNet.Models
{
    public class Pet
    {

        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }


        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("photoUrls", Required = Required.Always)]
        public List<string> PhotoUrls { get; set; }


        [JsonProperty("tags", Required = Required.Always)]
        public List<Tag> Tags { get; set; }
        
        [JsonProperty("status", Required = Required.Always)]
        [ModelProperty(Description = "pet status in the store")]
        [ModelProperty(Enum = new[] {"available", "pending", "sold"})]
        public String Status { get; set; }
    }
}