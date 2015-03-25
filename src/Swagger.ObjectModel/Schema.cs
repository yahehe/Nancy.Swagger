namespace Swagger.ObjectModel
{
    using System;
    using System.Collections.Generic;

    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The schema.
    /// </summary>
    public class Schema : DataType
    {
        /// <summary>
        /// Adds support for polymorphism. The discriminator is the schema property name that is used to differentiate between other schema that inherit this schema. 
        /// The property name used MUST be defined at this schema and it MUST be in the required property list. 
        /// When used, the value MUST be the name of this schema or any schema that inherits it.
        /// </summary>
        [SwaggerProperty("discriminator")]
        public string Discriminator { get; set; }

        /// <summary>
        /// Relevant only for Schema "properties" definitions. Declares the property as "read only". 
        /// This means that it MAY be sent as part of a response but MUST NOT be sent as part of the request. 
        /// Properties marked as readOnly being true SHOULD NOT be in the required list of the defined schema. 
        /// </summary>
        [SwaggerProperty("readOnly")]
        public bool? ReadOnly { get; set; }

        /// <summary>
        /// Additional external documentation for this schema.
        /// </summary>
        [SwaggerProperty("externalDocs")]
        public ExternalDocumentation ExternalDocumentation { get; set; }

        /// <summary>
        /// A free-form property to include a an example of an instance for this schema.
        /// </summary>
        [SwaggerProperty("example")]
        public object Example { get; set; }

        /// <summary>
        /// Collection of child members
        /// </summary>
        [SwaggerProperty("properties")]
        public IDictionary<string, Schema> Properties { get; set; }

        /// <summary>
        /// An object instance is valid against this keyword if its property set contains all elements in this keyword's array value.
        /// </summary>
        [SwaggerProperty("required")]
        public IList<string> Required { get; set; }

        /// <summary>
        /// An instance validates successfully against this keyword if it validates successfully against all schemas defined by this keyword's value.
        /// </summary>
        [SwaggerProperty("allOf")]
        public IList<string> AllOf { get; set; }

        /// <summary>
        /// The System.Type of the object
        /// </summary>
        public Type ClrType { get; set; }

        /// <summary>
        /// A brief description of this schema.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }
    
    }
}