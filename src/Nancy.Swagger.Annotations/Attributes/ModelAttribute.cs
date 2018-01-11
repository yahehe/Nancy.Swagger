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

        /// <summary>
        /// By default, only read/write props are shown, this 
        /// prop allows read only props to be shown.
        /// </summary>
        public bool ShowReadOnlyProps { get; set; }
    }
}