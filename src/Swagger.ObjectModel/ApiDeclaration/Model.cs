using System.Collections.Generic;
using Swagger.ObjectModel.Attributes;

namespace Swagger.ObjectModel.ApiDeclaration
{
    /// <summary>
    /// A Model Object holds the definition of a new model for this API Declaration.
    /// </summary>
    /// <remarks>
    /// Models in Swagger allow for inheritance.
    /// The inheritance is controlled by two fields - <see cref="SubTypes"/> to give the name of
    /// the models extending this definition, and <see cref="Discriminator"/> to support polymorphism.
    /// </remarks>
    /// <example>
    /// <code>
    /// {
    ///   "id": "Order",
    ///   "properties": {
    ///     "id": {
    ///       "type": "integer",
    ///       "format": "int64"
    ///     },
    ///     "petId": {
    ///       "type": "integer",
    ///       "format": "int64"
    ///     },
    ///     "quantity": {
    ///       "type": "integer",
    ///       "format": "int32"
    ///     },
    ///     "status": {
    ///       "type": "string",
    ///       "description": "OrderStatus",
    ///       "enum": [
    ///         "placed",
    ///         "approved",
    ///         "delivered"
    ///       ]
    ///     },
    ///     "shipDate": {
    ///       "type": "string",
    ///       "format": "date-time"
    ///     }
    ///   }
    /// }
    /// </code>
    /// </example>
    public class Model : SwaggerModel
    {
        /// <summary>
        /// A unique identifier for the model.
        /// </summary>
        [SwaggerProperty("id", true)]
        public string Id { get; set; }

        /// <summary>
        /// A brief description of this model.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// A definition of which properties MUST exist when a model instance is produced.
        /// </summary>
        [SwaggerProperty("required")]
        public IEnumerable<string> Required { get; set; }

        /// <summary>
        /// A list of properties (fields) that are part of the model.
        /// </summary>
        [SwaggerProperty("properties", true)]
        public IDictionary<string, ModelProperty> Properties { get; set; }

        /// <summary>
        /// List of the model ids that inherit from this model.
        /// Sub models inherit all the properties of the parent model.
        /// Since inheritance is transitive, if the parent of a model inherits
        /// from another model, its sub-model will include all properties.
        /// As such, if you have Foo->Bar->Baz, then Baz will inherit the properties of Bar and Foo.
        /// </summary>
        /// <remarks>
        /// There MUST NOT be a cyclic definition of inheritance.
        /// There also MUST NOT be a case of multiple inheritance.
        /// A sub-model definition MUST NOT override the properties of any of its ancestors.
        /// All sub-models MUST be defined in the same API Declaration.
        /// </remarks>
        [SwaggerProperty("subTypes")]
        public IEnumerable<string> SubTypes { get; set; }

        /// <summary>
        /// This field allows for polymorphism within the described inherited models.
        /// </summary>
        /// <remarks>
        /// MUST be included only if <see cref="SubTypes"/> is included.
        /// This field MAY be included at any base model but MUST NOT be included in a sub-model.
        /// The value of this field MUST be a name of one of the properties in this model, and that field MUST be in the required list.
        /// When used, the value of the discriminator property MUST be the name of parent or any of its sub-models (to any depth of inheritance).
        /// </remarks>
        [SwaggerProperty("discriminator")]
        public string Discriminator { get; set; }
    }
}