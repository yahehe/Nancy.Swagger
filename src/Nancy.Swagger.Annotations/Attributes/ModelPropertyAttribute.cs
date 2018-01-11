using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ModelPropertyAttribute : SwaggerDataTypeAttribute
    {
        public ModelPropertyAttribute()
            : this(null)
        {
        }

        public ModelPropertyAttribute(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Ignore this property when generating swagger model.
        /// </summary>
        public bool Ignore { get; set; }
    }
}