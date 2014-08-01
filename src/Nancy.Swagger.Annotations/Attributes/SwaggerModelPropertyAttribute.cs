using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class SwaggerModelPropertyAttribute : SwaggerDataTypeAttribute
    {
        public SwaggerModelPropertyAttribute()
            : this(null)
        {
        }

        public SwaggerModelPropertyAttribute(string name)
            : base(name)
        {
        }
    }
}