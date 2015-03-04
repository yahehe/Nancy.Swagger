using System;
using Nancy.Swagger.Annotations.Attributes;

namespace Nancy.Swagger.Annotations.Demo.Models
{
    public class Order
    {
        public long Id { get; set; }
        public bool Complete { get; set; }
        public long PetId { get; set; }
        public int Quantity { get; set; }
        public DateTime ShipDate { get; set; }

        [ModelProperty(Description = "Order Status")]
        [ModelProperty(Enum = new[] {"placed", "approved", "delivered"})]
        public String Status { get; set; }
    }
}