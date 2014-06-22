using System;

namespace Swagger.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SwaggerEnumValueAttribute : Attribute
    {
        public SwaggerEnumValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}