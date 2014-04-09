using Nancy.Swagger.Attributes;

namespace Nancy.Swagger.ApiDeclaration
{
    /// <summary>
    /// The Response Message Object describes a single possible response message that can be 
    /// returned from the operation call, and maps to the responseMessages field in the Operation Object. 
    /// Each Response Message allows you to give further details as to why the HTTP status code may be received.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "code": 404,
    ///   "message": "no project found",
    ///   "responseType": "ErrorModel"
    /// }
    /// </code>
    /// </example>
    [SwaggerDto]
    public class ResponseMessage
    {
        /// <summary>
        /// The HTTP status code returned.
        /// </summary>
        [Required]
        public int Code { get; set; }

        /// <summary>
        /// The explanation for the status code.
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// The return type for the given response.
        /// </summary>
        public string ResponseModel { get; set; }
    }
}