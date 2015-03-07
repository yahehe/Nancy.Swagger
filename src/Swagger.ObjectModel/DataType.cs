using System.Collections.Generic;


namespace Swagger.ObjectModel
{
    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// Base class for Swagger data types.
    /// </summary>
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
        /// Fine-tuned primitive type definition.
        /// </summary>
        [SwaggerProperty("format")]
        public string Format { get; set; }

        /// <summary>
        /// The type definition of the values in the container.
        /// </summary>
        /// <remarks>
        /// A container MAY NOT be nested in another container.
        /// </remarks>
        [SwaggerProperty("items", true)]
        public Item Items { get; set; }

        /// <summary>
        /// Determines the format of the array if type array is used. Default value is csv
        /// </summary>
        [SwaggerProperty("collectionFormat")]
        public CollectionFormats? CollectionFormat { get; set; }

        /// <summary>
        /// Sets a default value to the parameter. The type of the value depends on the defined type. See http://json-schema.org/latest/json-schema-validation.html#anchor101.
        /// </summary>
        [SwaggerProperty("default")]
        public object Default { get; set; }

        /// <summary>
        /// The maximum valid value for the type, inclusive. If this field is used in conjunction with the defaultValue field,
        /// then the default value MUST be lower than or equal to this value.
        /// </summary>
        [SwaggerProperty("maximum")]
        public long? Maximum { get; set; }

        /// <summary>
        /// If null or false, then the instance is valid if it is less than, or equal to, the value of <see cref="Maximum"/>
        /// if true, the instance is valid if it is strictly less than the value of <see cref="Maximum"/>.
        /// </summary>
        [SwaggerProperty("exclusiveMinimum")]
        public bool? ExclusiveMaximum { get; set; }

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
        /// If null or false, then the instance is valid if it is greater than, or equal to, the value of <see cref="Minimum"/>
        /// if true, the instance is valid if it is strictly greater than the value of <see cref="Minimum"/>.
        /// </summary>
        [SwaggerProperty("exclusiveMinimum")]
        public bool? ExclusiveMinimum { get; set; }

        /// <summary>
        /// Max length of string value
        /// </summary>
        [SwaggerProperty("maxLength")]
        public long? MaxLength { get; set; }

        /// <summary>
        /// Min length of string value
        /// </summary>
        [SwaggerProperty("minLength")]
        public long? MinLength { get; set; }

        /// <summary>
        /// Regular expression
        /// </summary>
        [SwaggerProperty("pattern")]
        public string Pattern { get; set; }

        /// <summary>
        /// Max array length
        /// </summary>
        [SwaggerProperty("maxItems")]
        public int? MaxItems { get; set; }

        /// <summary>
        /// Min array length
        /// </summary>
        [SwaggerProperty("minItems")]
        public int? MinItems { get; set; }

        /// <summary>
        /// A flag to note whether the container allows duplicate values or not.
        /// If the value is set to <c>true</c>, then the array acts as a set.
        /// </summary>
        [SwaggerProperty("uniqueItems")]
        public bool? UniqueItems { get; set; }

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
        /// A numeric instance is valid against "multipleOf" if the result of the division of the instance by this keyword's value is an integer.
        /// </summary>
        [SwaggerProperty("multipleOf")]
        public int? MultipleOf { get; set; }

    }
}