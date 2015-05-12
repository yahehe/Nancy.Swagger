using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ModelAttribute : Attribute
    {
        public ModelAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}