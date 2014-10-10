using System;

namespace Swagger.ObjectModel.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class SwaggerPropertyAttribute : Attribute
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