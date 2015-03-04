using Nancy.Swagger.Annotations.Attributes;
using System.Collections.Generic;

namespace Nancy.Swagger.Annotations.Tests
{
    [Model("Description of a model")]
    public class TestModel
    {
        #region Default usages

        [ModelProperty(Description = "Some description")]
        public string Description { get; set; }

        [ModelProperty(Enum = new[] { "male", "female" })]
        public string Enum { get; set; }

        [ModelProperty(UniqueItems = true)]
        public IList<string> ListOfUniqueItems { get; set; }

        [ModelProperty(Maximum = 100)]
        public int? Maximum { get; set; }

        [ModelProperty(Minimum = 0)]
        public int? Minimum { get; set; }

        [ModelProperty("name-by-constructor")]
        public string NameByConstructor { get; set; }

        [ModelProperty(Name = "name-by-namedparameter")]
        public string NameByNamedParameter { get; set; }

        [ModelProperty(Required = true)]
        public string ExplicitRequired { get; set; }

        #endregion Default usages

        #region Non-public properties

        [ModelProperty(Description = "Properties without public setter are not part of the model")]
        public string PrivateSetter { get; private set; }

        [ModelProperty(Description = "Internal properties are not part of the model")]
        internal string Internal { get; set; }

        [ModelProperty(Description = "Protected properties are not part of the model")]
        protected string Protected { get; set; }

        [ModelProperty(Description = "Private properties are not part of the model")]
        private string Private { get; set; }

        #endregion Non-public properties

        #region Other usages

        [ModelProperty(Description = "Non-nullable value types are implicitly required")]
        public int ImplicitRequired { get; set; }

        [ModelProperty(Minimum = 0)]
        [ModelProperty(Maximum = 100)]
        [ModelProperty(Description = "Property with multiple annotations (minimum, maximum and description)")]
        public int? MultipleAnnotations { get; set; }
        
        // Adding two properties of the same non-primitive type to reproduce 
        // https://github.com/khellang/Nancy.Swagger/issues/49
        public InOtherNamespace.TestModel TwoPropertiesOfSameType_First { get; set; }
        public InOtherNamespace.TestModel TwoPropertiesOfSameType_Second { get; set; }

        #endregion Other usages
    }
}