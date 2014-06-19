using Nancy.Swagger.Attributes;
using Newtonsoft.Json;

namespace Nancy.Swagger
{
    /// <summary>
    /// This object is used to describe the value types used inside an array.
    /// </summary>
    /// <example>
    /// For a primitive type:
    /// <code>
    /// {
    ///   "type": "string"
    /// }
    /// </code>
    /// For a complex type:
    /// <code>
    /// {
    ///   "$ref": "Pet"
    /// }
    /// </code>
    /// </example>
    [SwaggerData]
    public class Items
    {
        /// <summary>
        /// The return type of the operation.
        /// </summary>
        /// <remarks>
        /// The value MUST be one of the Primitves, array or a model's id.
        /// </remarks>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Fine-tuned primitive type definition.
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }

        /// <summary>
        /// The Model to be used.
        /// </summary>
        /// <remarks>
        /// The value MUST be a model's id.
        /// </remarks>
        [JsonProperty("$ref")]
        public string Ref { get; set; }
    }
}