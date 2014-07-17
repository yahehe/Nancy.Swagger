using Swagger.ObjectModel.Attributes;

namespace Swagger.ObjectModel.ApiDeclaration
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
    public class ModelProperty : DataType
    {
        /// <summary>
        /// A brief description of this property.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }
    }
}