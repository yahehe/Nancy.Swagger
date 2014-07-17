using System.Collections.Generic;
using Swagger.ObjectModel.Attributes;

namespace Swagger.ObjectModel
{
    public class DataType : SwaggerModel
    {
        /// <summary>
        /// The return type of the operation.
        /// </summary>
        /// <remarks>
        /// The value MUST be one of the Primitves, array or a model's id.
        /// </remarks>
        [SwaggerProperty("type", true)] // TODO: Only required if Ref is null
        public string Type { get; set; }

        /// <summary>
        /// The Model to be used.
        /// </summary>
        /// <remarks>
        /// The value MUST be a model's id.
        /// </remarks>
        [SwaggerProperty("$ref", true)] // TODO: Only required if Type is null
        public string Ref { get; set; }

        /// <summary>
        /// Fine-tuned primitive type definition.
        /// </summary>
        [SwaggerProperty("format")]
        public string Format { get; set; }

        /// <summary>
        /// The default value to be used for the field.
        /// </summary>
        /// <remarks>
        /// The value type MUST conform with the primitive's type value.
        /// </remarks>
        [SwaggerProperty("defaultValue")]
        public object DefaultValue { get; set; }

        /// <summary>
        /// A fixed list of possible values.
        /// </summary>
        /// <remarks>
        /// If this field is used in conjunction with the defaultValue field, 
        /// then the default value MUST be one of the values defined in the enum.
        /// </remarks>
        [SwaggerProperty("enum")]
        public IEnumerable<string> Enum { get; set; }

        /// <summary>
        /// The minimum valid value for the type, inclusive.
        /// </summary>
        /// <remarks>
        /// If this field is used in conjunction with the defaultValue field, 
        /// then the default value MUST be higher than or equal to this value.
        /// </remarks>
        [SwaggerProperty("minimum")]
        public long? Minimum { get; set; }

        /// <summary>
        /// The maximum valid value for the type, inclusive. If this field is used in conjunction with the defaultValue field, 
        /// then the default value MUST be lower than or equal to this value.
        /// </summary>
        [SwaggerProperty("maximum")]
        public long? Maximum { get; set; }

        /// <summary>
        /// The type definition of the values in the container. 
        /// </summary>
        /// <remarks>
        /// A container MAY NOT be nested in another container.
        /// </remarks>
        [SwaggerProperty("items", true)]
        public Items Items { get; set; }

        /// <summary>
        /// A flag to note whether the container allows duplicate values or not. 
        /// If the value is set to <c>true</c>, then the array acts as a set.
        /// </summary>
        [SwaggerProperty("uniqueItems")]
        public bool? UniqueItems { get; set; }
    }
}