using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class SwaggerModelAttribute : Attribute
    {
        public SwaggerModelAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}