using Nancy.Swagger.Annotations.Attributes;
using System.Collections.Generic;

namespace Nancy.Swagger.Annotations.Tests.Testdata
{
    [SwaggerModel("Description of a model")]
    public class TestModel
    {
        #region Default usages

        [SwaggerModelProperty(Description = "Some description")]
        public string Description { get; set; }

        [SwaggerModelProperty(Enum = new[] { "male", "female" })]
        public string Enum { get; set; }

        [SwaggerModelProperty(UniqueItems = true)]
        public IList<string> ListOfUniqueItems { get; set; }

        [SwaggerModelProperty(Maximum = 100)]
        public int? Maximum { get; set; }

        [SwaggerModelProperty(Minimum = 0)]
        public int? Minimum { get; set; }

        [SwaggerModelProperty("name-by-constructor")]
        public string NameByConstructor { get; set; }

        [SwaggerModelProperty(Name = "name-by-namedparameter")]
        public string NameByNamedParameter { get; set; }

        [SwaggerModelProperty(Required = true)]
        public string ExplicitRequired { get; set; }

        #endregion Default usages

        #region Non-public properties

        [SwaggerModelProperty(Description = "Properties without public setter are not part of the model")]
        public string PrivateSetter { get; private set; }

        [SwaggerModelProperty(Description = "Internal properties are not part of the model")]
        internal string Internal { get; set; }

        [SwaggerModelProperty(Description = "Protected properties are not part of the model")]
        protected string Protected { get; set; }

        [SwaggerModelProperty(Description = "Private properties are not part of the model")]
        private string Private { get; set; }

        #endregion Non-public properties

        #region Other usages

        [SwaggerModelProperty(Description = "Non-nullable value types are implicitly required")]
        public int ImplicitRequired { get; set; }

        [SwaggerModelProperty(Minimum = 0)]
        [SwaggerModelProperty(Maximum = 100)]
        [SwaggerModelProperty(Description = "Property with multiple annotations (minimum, maximum and description)")]
        public int? MultipleAnnotations { get; set; }
        
        // Adding two properties of the same non-primitive type to reproduce 
        // https://github.com/khellang/Nancy.Swagger/issues/49
        public InOtherNamespace.TestModel TwoPropertiesOfSameType_First { get; set; }
        public InOtherNamespace.TestModel TwoPropertiesOfSameType_Second { get; set; }

        #endregion Other usages
    }
}