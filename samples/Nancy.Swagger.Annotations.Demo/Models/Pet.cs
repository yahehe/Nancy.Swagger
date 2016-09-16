using System;
using System.Collections.Generic;
using Nancy.Swagger.Annotations.Attributes;

namespace Nancy.Swagger.Annotations.Demo.Models
{
    public class Pet
    {
        public long Id { get; set; }
        public Category Category { get; set; }

        [ModelProperty(Required = true)]
        public string Name { get; set; }

        [ModelProperty(Required = true)]
        public List<string> PhotoUrls { get; set; }

        public List<Tag> Tags { get; set; }

        [ModelProperty(Description = "pet status in the store")]
        [ModelProperty(Enum = new[] {"available", "pending", "sold"})]
        public String Status { get; set; }
    }
}