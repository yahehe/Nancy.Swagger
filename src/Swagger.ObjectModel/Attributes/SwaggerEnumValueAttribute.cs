using System;

namespace Swagger.ObjectModel.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class SwaggerEnumValueAttribute : Attribute
    {
        public SwaggerEnumValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}