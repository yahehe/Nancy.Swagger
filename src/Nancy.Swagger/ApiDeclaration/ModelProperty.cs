using Nancy.Swagger.Attributes;

namespace Nancy.Swagger.ApiDeclaration
{
    /// <summary>
    /// A Property Object holds the definition of a new property for a model.
    /// </summary>
    /// <example>
    /// <code>
    /// "id": {
    ///     "type": "integer",
    ///     "format": "int64",
    ///     "description": "unique identifier for the pet",
    ///     "minimum": "0.0",
    ///     "maximum": "100.0"
    /// }
    /// </code>
    /// </example>
    [SwaggerDto]
    public class ModelProperty : DataType
    {
        /// <summary>
        /// A brief description of this property.
        /// </summary>
        public string Description { get; set; }
    }
}