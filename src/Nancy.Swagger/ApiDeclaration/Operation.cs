using System.Collections.Generic;
using Nancy.Swagger.Attributes;
using Nancy.Swagger.ResourceListing;
using Newtonsoft.Json;

namespace Nancy.Swagger.ApiDeclaration
{
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
    [SwaggerDto]
    public class Operation
    {
        /// <summary>
        /// The HTTP method required to invoke this operation.
        /// </summary>
        [Required]
        [JsonProperty("method")]
        public HttpMethod Method { get; set; }

        /// <summary>
        /// A short summary of what the operation does.
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// A verbose explanation of the operation behavior.
        /// </summary>
        [JsonProperty("notes")]
        public string Notes { get; set; }

        /// <summary>
        /// A unique id for the operation that can be used by tools reading the output for further and easier manipulation.
        /// </summary>
        /// <remarks>
        /// The value MUST be alphanumeric and may include underscores. Whitespsace characters are not allowed.
        /// </remarks>
        [Required]
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// A list of authorizations required to execute this operation.
        /// </summary>
        [JsonProperty("authorizations")]
        public IDictionary<string, Authorization> Authorizations { get; set; }

        /// <summary>
        /// The inputs to the operation.
        /// </summary>
        /// <remarks>
        /// If no parameters are needed, an empty array MUST be included.
        /// </remarks>
        [Required]
        [JsonProperty("parameters")]
        public IEnumerable<Parameter> Parameters { get; set; }

        /// <summary>
        /// Lists the possible response statuses that can return from the operation.
        /// </summary>
        [JsonProperty("responseMessages")]
        public IEnumerable<ResponseMessage> ResponseMessages { get; set; }

        /// <summary>
        /// A list of MIME types this operation can produce. 
        /// This is overrides the global produces definition at the root of the API Declaration.
        /// </summary>
        [JsonProperty("produces")]
        public IEnumerable<string> Produces { get; set; }

        /// <summary>
        /// A list of MIME types this operation can consume. 
        /// This is overrides the global consumes definition at the root of the API Declaration.
        /// </summary>
        [JsonProperty("consumes")]
        public IEnumerable<string> Consumes { get; set; }

        /// <summary>
        /// Declares this operation to be deprecated. 
        /// Usage of the declared operation should be refrained.
        /// </summary>
        [JsonProperty("deprecated")]
        public bool? Deprecated { get; set; }
    }
}