using System;

namespace Swagger.ObjectModel
{
    using System.Collections.Generic;

    using Swagger.ObjectModel.Attributes;

    public class Operation : SwaggerModel
    {
        /// <summary>
        /// A list of tags for API documentation control. Tags can be used for logical grouping of operations by resources or any other qualifier.
        /// </summary>
        [SwaggerProperty("tags")]
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// A short summary of what the operation does. For maximum readability in the swagger-ui, this field SHOULD be less than 120 characters.
        /// </summary>
        [SwaggerProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// A verbose explanation of the operation behavior. GFM syntax can be used for rich text representation.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Additional external documentation for this operation.
        /// </summary>
        [SwaggerProperty("externalDocs")]
        public ExternalDocumentation ExternalDocumentation { get; set; }

        /// <summary>
        /// A friendly name for the operation. The id MUST be unique among all operations described in the API. 
        /// Tools and libraries MAY use the operation id to uniquely identify an operation.
        /// </summary>
        /// <remarks>
        /// The value MUST be alphanumeric and may include underscores. Whitespsace characters are not allowed.
        /// </remarks>
        [SwaggerProperty("operationId", true)]
        public string OperationId { get; set; }

        /// <summary>
        /// A list of MIME types this operation can consume.
        /// This is overrides the global consumes definition at the root of the API Declaration.
        /// </summary>
        [SwaggerProperty("consumes")]
        public IEnumerable<string> Consumes { get; set; }

        /// <summary>
        /// A list of MIME types this operation can produce.
        /// This is overrides the global produces definition at the root of the API Declaration.
        /// </summary>
        [SwaggerProperty("produces")]
        public IEnumerable<string> Produces { get; set; }

        /// <summary>
        /// A list of parameters that are applicable for this operation. 
        /// If a parameter is already defined at the Path Item, the new definition will override it, but can never remove it. 
        /// The list MUST NOT include duplicated parameters. A unique parameter is defined by a combination of a name and location. 
        /// The list can use the Reference Object to link to parameters that are defined at the Swagger Object's parameters. 
        /// There can be one "body" parameter at most.
        /// </summary>
        [SwaggerProperty("parameters", true)]
        public IEnumerable<Parameter> Parameters { get; set; }

        /// <summary>
        /// Lists the possible response statuses that can return from the operation.
        /// </summary>
        [SwaggerProperty("responses")]
        public IDictionary<string, Response> Responses { get; set; }

        /// <summary>
        /// The transfer protocol for the operation.
        /// </summary>
        [SwaggerProperty("schemes")]
        public IEnumerable<Schemes> Schemes { get; set; }

        /// <summary>
        /// Declares this operation to be deprecated.
        /// Usage of the declared operation should be refrained.
        /// </summary>
        [SwaggerProperty("deprecated")]
        public bool? Deprecated { get; set; }

        /// <summary>
        /// A declaration of which security schemes are applied for this operation. 
        /// The list of values describes alternative security schemes that can be used (that is, there is a logical OR between the security requirements). 
        /// This definition overrides any declared top-level security. 
        /// To remove a top-level security declaration, an empty array can be used.
        /// </summary>
        [SwaggerProperty("security")]
        public IDictionary<SecuritySchemes, IEnumerable<string>> SecurityRequirements { get; set; }

        public Type OperationModel { get; set; }
    }
}