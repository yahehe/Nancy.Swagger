using Nancy.Swagger.Annotations.Attributes;
using System.Collections.Generic;

namespace Nancy.Swagger.Annotations.Tests
{
    [SwaggerModel("Description of a model")]
    public class TestModel
    {
        [SwaggerModelProperty(Description = "Some description")]
        public string Description { get; set; }

        [SwaggerModelProperty(Enum = new[] { "male", "female" })]
        public string Enum { get; set; }

        [SwaggerModelProperty(Maximum = 0)]
        public int? Maximum { get; set; }

        [SwaggerModelProperty(Minimum = 0)]
        public int? Minimum { get; set; }

        [SwaggerModelProperty(Minimum = 0)]
        [SwaggerModelProperty(Description = "Property with minimum and description in separate annotations")]
        public int MultipleAnnotations { get; set; }

        [SwaggerModelProperty("name-by-constructor")]
        public string NamedByConstructor { get; set; }

        [SwaggerModelProperty(Name = "name-by-namedparameter")]
        public string NamedByParameter { get; set; }

        [SwaggerModelProperty(Description = "Properties without public setter are not part of the model")]
        public string NoSetter { get; private set; }

        [SwaggerModelProperty(Required = true)]
        public string Required { get; set; }

        [SwaggerModelProperty(UniqueItems = true)]
        public IList<string> ListOfUniqueItems { get; set; }

        [SwaggerModelProperty(Description = "Private properties without setter are not part of the model")]
        private string Private { get; set; }
    }
}