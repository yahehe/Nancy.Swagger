using Nancy.Swagger.Annotations.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nancy.Swagger.Demo.Models
{
    /// <summary>
    /// Show the use of ShowReadOnlyProps to expose properties without a public setter.
    /// </summary>
    [Model("Widget", Description = "A Thing that does something", ShowReadOnlyProps = true)]
    public class Widget
    {
        public Widget(string name, double price)
        {
            Name = name;
            Price = price;
        }

        [ModelProperty(Description = "The Name of the Widget")]
        public string Name { get; protected set; }

        [ModelProperty(Description = "The Price of the Widget")]
        public double Price { get; protected set; }
    }
}
