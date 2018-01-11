using Nancy.Swagger.Annotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nancy.Swagger.Demo.Models
{
    [Model("User")]
    public class User
    {
        [Required]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Address Address { get; set; }

        public Role Role { get; set; }

        public IList<string> Tags { get; set; }

        [ModelProperty(Ignore = true)]
        public string IgnoreMe { get; set; }
    }
}