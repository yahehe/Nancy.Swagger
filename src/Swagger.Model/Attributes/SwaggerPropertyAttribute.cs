using System;

namespace Swagger.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerPropertyAttribute : Attribute
    {
        public SwaggerPropertyAttribute(string name, bool required = false)
        {
            Name = name;
            Required = required;
        }

        public string Name { get; private set; }

        public bool Required { get; private set; }
    }
}