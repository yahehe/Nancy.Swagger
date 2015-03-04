using System.Collections.Generic;


namespace Swagger.ObjectModel.SwaggerDocument
{
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The API Object describes one or more operations on a single path.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "path": "/pet",
    ///   "operations": [
    ///     {
    ///       "method": "PUT",
    ///       "summary": "Update an existing pet",
    ///       "notes": "",
    ///       "type": "void",
    ///       "nickname": "updatePet",
    ///       "authorizations": {},
    ///       "parameters": [
    ///         {
    ///           "name": "body",
    ///           "description": "Pet object that needs to be updated in the store",
    ///           "required": true,
    ///           "type": "Pet",
    ///           "paramType": "body"
    ///         }
    ///       ],
    ///       "responseMessages": [
    ///         {
    ///           "code": 400,
    ///           "message": "Invalid ID supplied"
    ///         },
    ///         {
    ///           "code": 404,
    ///           "message": "Pet not found"
    ///         },
    ///         {
    ///           "code": 405,
    ///           "message": "Validation exception"
    ///         }
    ///       ]
    ///     },
    ///     {
    ///       "method": "POST",
    ///       "summary": "Add a new pet to the store",
    ///       "notes": "",
    ///       "type": "void",
    ///       "nickname": "addPet",
    ///       "consumes": [
    ///         "application/json",
    ///         "application/xml"
    ///       ],
    ///       "authorizations": {
    ///         "oauth2": [
    ///           {
    ///             "scope": "test:anything",
    ///             "description": "anything"
    ///           }
    ///         ]
    ///       },
    ///       "parameters": [
    ///         {
    ///           "name": "body",
    ///           "description": "Pet object that needs to be added to the store",
    ///           "required": true,
    ///           "type": "Pet",
    ///           "paramType": "body"
    ///         }
    ///       ],
    ///       "responseMessages": [
    ///         {
    ///           "code": 405,
    ///           "message": "Invalid input"
    ///         }
    ///       ]
    ///     }
    ///   ]
    /// }
    /// </code>
    /// </example>
    public class Api : SwaggerModel
    {
        /// <summary>
        /// The relative path to the operation, from the basePath, which this operation describes.
        /// </summary>
        /// <remarks>
        /// The value SHOULD be in a relative (URL) path format.
        /// </remarks>
        [SwaggerProperty("path", true)]
        public string Path { get; set; }

        /// <summary>
        /// A short description of the resource.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///  A list of the API operations available on this path.
        /// </summary>
        /// <remarks>
        /// There MUST NOT be more than one Operation Object per method in the array.
        /// </remarks>
        [SwaggerProperty("operations", true)]
        public IEnumerable<Operation> Operations { get; set; }
    }
}