using System.Collections.Generic;

namespace Swagger.ObjectModel
{
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The Operation Object describes a single operation on a path.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "method": "GET",
    ///   "summary": "Find pet by ID",
    ///   "notes": "Returns a pet based on ID",
    ///   "type": "Pet",
    ///   "nickname": "getPetById",
    ///   "authorizations": {},
    ///   "parameters": [
    ///     {
    ///       "name": "petId",
    ///       "description": "ID of pet that needs to be fetched",
    ///       "required": true,
    ///       "type": "integer",
    ///       "format": "int64",
    ///       "paramType": "path",
    ///       "minimum": "1.0",
    ///       "maximum": "100000.0"
    ///     }
    ///   ],
    ///   "responseMessages": [
    ///     {
    ///       "code": 400,
    ///       "message": "Invalid ID supplied"
    ///     },
    ///     {
    ///       "code": 404,
    ///       "message": "Pet not found"
    ///     }
    ///   ]
    /// }
    /// </code>
    /// </example>
    public class Operation : DataType
    {
        /// <summary>
        /// The HTTP method required to invoke this operation.
        /// </summary>
        [SwaggerProperty("method", true)]
        public HttpMethod Method { get; set; }

        /// <summary>
        /// A list of tags for API documentation control. Tags can be used for logical grouping of operations by resources or any other qualifier.
        /// </summary>
        [SwaggerProperty("tags")]
        public string Tags { get; set; }

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
        /// A list of authorizations required to execute this operation.
        /// </summary>
        [SwaggerProperty("authorizations")]
        public IDictionary<string, Authorization> Authorizations { get; set; }


        /// <summary>
        /// Lists the possible response statuses that can return from the operation.
        /// </summary>
        [SwaggerProperty("responseMessages")]
        public IEnumerable<ResponseMessage> ResponseMessages { get; set; }

        /// <summary>
        /// Declares this operation to be deprecated.
        /// Usage of the declared operation should be refrained.
        /// </summary>
        [SwaggerProperty("deprecated")]
        public bool? Deprecated { get; set; }
    }
}