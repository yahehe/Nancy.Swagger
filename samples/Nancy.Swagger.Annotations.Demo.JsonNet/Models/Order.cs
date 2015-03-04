using System;
using Nancy.Swagger.Annotations.Attributes;
using Newtonsoft.Json;

namespace Nancy.Swagger.Annotations.Demo.JsonNet.Models
{
    public class Order
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("complete", Required = Required.Always)]
        public bool Complete { get; set; }

        [JsonProperty("petId", Required = Required.Always)]
        public long PetId { get; set; }

        [JsonProperty("quantity", Required = Required.Always)]
        public int Quantity { get; set; }

        [JsonProperty("shipDate", Required = Required.Always)]
        public DateTime ShipDate { get; set; }

        [JsonProperty("shipDate", Required = Required.Always)]
        [ModelProperty(Description = "Order Status")]
        [ModelProperty(Enum = new[] { "placed", "approved", "delivered" })]
        public String Status { get; set; }
    }
}