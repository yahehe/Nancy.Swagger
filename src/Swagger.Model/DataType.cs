using System.Collections.Generic;
using Newtonsoft.Json;
using Swagger.Model.Attributes;

namespace Swagger.Model
{
    [SwaggerData]
    public class DataType
    {
        /// <summary>
        /// The return type of the operation.
        /// </summary>
        /// <remarks>
        /// The value MUST be one of the Primitves, array or a model's id.
        /// </remarks>
        [Required] // TODO: Only if Ref is null
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The Model to be used.
        /// </summary>
        /// <remarks>
        /// The value MUST be a model's id.
        /// </remarks>
        [Required] // TODO: Only if Type is null
        [JsonProperty("$ref")]
        public string Ref { get; set; }

        /// <summary>
        /// Fine-tuned primitive type definition.
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }

        /// <summary>
        /// The default value to be used for the field.
        /// </summary>
        /// <remarks>
        /// The value type MUST conform with the primitive's type value.
        /// </remarks>
        [JsonProperty("defaultValue")]
        public object DefaultValue { get; set; }

        /// <summary>
        /// A fixed list of possible values.
        /// </summary>
        /// <remarks>
        /// If this field is used in conjunction with the defaultValue field, 
        /// then the default value MUST be one of the values defined in the enum.
        /// </remarks>
        [JsonProperty("enum")]
        public IEnumerable<string> Enum { get; set; }

        /// <summary>
        /// The minimum valid value for the type, inclusive.
        /// </summary>
        /// <remarks>
        /// If this field is used in conjunction with the defaultValue field, 
        /// then the default value MUST be higher than or equal to this value.
        /// </remarks>
        [JsonProperty("minimum")]
        public long? Minimum { get; set; }

        /// <summary>
        /// The maximum valid value for the type, inclusive. If this field is used in conjunction with the defaultValue field, 
        /// then the default value MUST be lower than or equal to this value.
        /// </summary>
        [JsonProperty("maximum")]
        public long? Maximum { get; set; }

        /// <summary>
        /// The type definition of the values in the container. 
        /// </summary>
        /// <remarks>
        /// A container MAY NOT be nested in another container.
        /// </remarks>
        [Required]
        [JsonProperty("items")]
        public Items Items { get; set; }

        /// <summary>
        /// A flag to note whether the container allows duplicate values or not. 
        /// If the value is set to <c>true</c>, then the array acts as a set.
        /// </summary>
        [JsonProperty("uniqueItems")]
        public bool? UniqueItems { get; set; }
    }
}