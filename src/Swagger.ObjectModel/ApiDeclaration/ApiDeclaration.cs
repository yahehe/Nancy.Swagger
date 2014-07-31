using System;
using System.Collections.Generic;
using Swagger.ObjectModel.Attributes;
using Swagger.ObjectModel.ResourceListing;

namespace Swagger.ObjectModel.ApiDeclaration
{
    /// <summary>
    /// The API Declaration provides information about an API exposed on a resource.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "apiVersion": "1.0.0",
    ///   "swaggerVersion": "1.2",
    ///   "basePath": "http://petstore.swagger.wordnik.com/api",
    ///   "resourcePath": "/store",
    ///   "produces": [
    ///     "application/json"
    ///   ],
    ///   "authorizations": {},
    ///   "apis": [
    ///     {
    ///       "path": "/store/order/{orderId}",
    ///       "operations": [
    ///         {
    ///           "method": "GET",
    ///           "summary": "Find purchase order by ID",
    ///           "notes": "For valid response try integer IDs with value less than or equal to 5. Anything above 5 or nonintegers will generate API errors",
    ///           "type": "Order",
    ///           "nickname": "getOrderById",
    ///           "authorizations": {},
    ///           "parameters": [
    ///             {
    ///               "name": "orderId",
    ///               "description": "ID of pet that needs to be fetched",
    ///               "required": true,
    ///               "type": "string",
    ///               "paramType": "path"
    ///             }
    ///           ],
    ///           "responseMessages": [
    ///             {
    ///               "code": 400,
    ///               "message": "Invalid ID supplied"
    ///             },
    ///             {
    ///               "code": 404,
    ///               "message": "Order not found"
    ///             }
    ///           ]
    ///         },
    ///         {
    ///           "method": "DELETE",
    ///           "summary": "Delete purchase order by ID",
    ///           "notes": "For valid response try integer IDs with value less than 1000.  Anything above 1000 or nonintegers will generate API errors",
    ///           "type": "void",
    ///           "nickname": "deleteOrder",
    ///           "authorizations": {
    ///             "oauth2": [
    ///               {
    ///                 "scope": "test:anything",
    ///                 "description": "anything"
    ///               }
    ///             ]
    ///           },
    ///           "parameters": [
    ///             {
    ///               "name": "orderId",
    ///               "description": "ID of the order that needs to be deleted",
    ///               "required": true,
    ///               "type": "string",
    ///               "paramType": "path"
    ///             }
    ///           ],
    ///           "responseMessages": [
    ///             {
    ///               "code": 400,
    ///               "message": "Invalid ID supplied"
    ///             },
    ///             {
    ///               "code": 404,
    ///               "message": "Order not found"
    ///             }
    ///           ]
    ///         }
    ///       ]
    ///     },
    ///     {
    ///       "path": "/store/order",
    ///       "operations": [
    ///         {
    ///           "method": "POST",
    ///           "summary": "Place an order for a pet",
    ///           "notes": "",
    ///           "type": "void",
    ///           "nickname": "placeOrder",
    ///           "authorizations": {
    ///             "oauth2": [
    ///               {
    ///                 "scope": "test:anything",
    ///                 "description": "anything"
    ///               }
    ///             ]
    ///           },
    ///           "parameters": [
    ///             {
    ///               "name": "body",
    ///               "description": "order placed for purchasing the pet",
    ///               "required": true,
    ///               "type": "Order",
    ///               "paramType": "body"
    ///             }
    ///           ],
    ///           "responseMessages": [
    ///             {
    ///               "code": 400,
    ///               "message": "Invalid order"
    ///             }
    ///           ]
    ///         }
    ///       ]
    ///     }
    ///   ],
    ///   "models": {
    ///     "Order": {
    ///       "id": "Order",
    ///       "properties": {
    ///         "id": {
    ///           "type": "integer",
    ///           "format": "int64"
    ///         },
    ///         "petId": {
    ///           "type": "integer",
    ///           "format": "int64"
    ///         },
    ///         "quantity": {
    ///           "type": "integer",
    ///           "format": "int32"
    ///         },
    ///         "status": {
    ///           "type": "string",
    ///           "description": "Order Status",
    ///           "enum": [
    ///             "placed",
    ///             " approved",
    ///             " delivered"
    ///           ]
    ///         },
    ///         "shipDate": {
    ///           "type": "string",
    ///           "format": "date-time"
    ///         }
    ///       }
    ///     }
    ///   }
    /// }
    /// </code>
    /// </example>
    public class ApiDeclaration : SwaggerModel
    {
        public ApiDeclaration()
        {
            Apis = new Api[0];
            SwaggerVersion = "1.2"; // TODO: Specify this centrally
        }

        /// <summary>
        /// Gets or sets the swagger version.
        /// </summary>
        /// <remarks>
        /// The value MUST be an existing Swagger specification version.
        /// </remarks>
        [SwaggerProperty("swaggerVersion", true)]
        public string SwaggerVersion { get; set; }

        /// <summary>
        /// Provides the version of the application API.
        /// </summary>
        [SwaggerProperty("apiVersion")]
        public string ApiVersion { get; set; }

        /// <summary>
        /// The root URL serving the API. 
        /// This field is important as while it is common to have the Resource Listing 
        /// and API Declarations on the server providing the APIs themselves, it is not a requirement. 
        /// The API specifications can be served using static files and not generated by the API server itself, 
        /// so the URL for serving the API cannot always be derived from the URL serving the API specification.
        /// </summary>
        [SwaggerProperty("basePath", true)]
        public Uri BasePath { get; set; }

        /// <summary>
        /// The relative path to the resource, from the basePath, which this API Specification describes.
        /// </summary>
        /// <remarks>
        /// The value MUST precede with a forward slash ("/").
        /// </remarks>
        [SwaggerProperty("resourcePath")]
        public string ResourcePath { get; set; }

        /// <summary>
        /// A list of the APIs exposed on this resource.
        /// </summary>
        [SwaggerProperty("apis", true)]
        public IEnumerable<Api> Apis { get; set; }

        /// <summary>
        /// A list of the models available to this resource. 
        /// Note that these need to be exposed separately for each API Declaration.
        /// </summary>
        [SwaggerProperty("models")]
        public IDictionary<string, Model> Models { get; set; }

        /// <summary>
        /// A list of MIME types the APIs on this resource can produce. 
        /// This is global to all APIs but can be overridden on specific API calls.
        /// </summary>
        [SwaggerProperty("produces")]
        public IEnumerable<string> Produces { get; set; }

        /// <summary>
        /// A list of MIME types the APIs on this resource can consume. 
        /// This is global to all APIs but can be overridden on specific API calls.
        /// </summary>
        [SwaggerProperty("consumes")]
        public IEnumerable<string> Consumes { get; set; }

        /// <summary>
        /// A list of authorizations schemes required for the operations listed in this API declaration. 
        /// Individual operations may override this setting. 
        /// If there are multiple authorization schemes described here, it means they're all applied.
        /// </summary>
        [SwaggerProperty("authorizations")]
        public IDictionary<string, Authorization> Authorizations { get; set; } 
    }
}