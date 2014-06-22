using System.Collections.Generic;
using Swagger.Model.Attributes;

namespace Swagger.Model.ResourceListing
{
    /// <summary>
    /// The Resource Listing serves as the root document for the API description.
    /// It contains general information about the API and an inventory of the available resources. 
    /// </summary>
    /// <remarks>
    /// By default, this document SHOULD be served at the /api-docs path.
    /// </remarks>
    /// <example>
    /// <code>
    /// {
    ///   "apiVersion": "1.0.0",
    ///   "swaggerVersion": "1.2",
    ///   "apis": [
    ///     {
    ///       "path": "/pet",
    ///       "description": "Operations about pets"
    ///     },
    ///     {
    ///       "path": "/user",
    ///       "description": "Operations about user"
    ///     },
    ///     {
    ///       "path": "/store",
    ///       "description": "Operations about store"
    ///     }
    ///   ],
    ///   "authorizations": {
    ///     "oauth2": {
    ///       "type": "oauth2",
    ///       "scopes": [
    ///         {
    ///           "scope": "email",
    ///           "description": "Access to your email address"
    ///         },
    ///         {
    ///           "scope": "pets",
    ///           "description": "Access to your pets"
    ///         }
    ///       ],
    ///       "grantTypes": {
    ///         "implicit": {
    ///           "loginEndpoint": {
    ///             "url": "http://petstore.swagger.wordnik.com/oauth/dialog"
    ///           },
    ///           "tokenName": "access_token"
    ///         },
    ///         "authorization_code": {
    ///           "tokenRequestEndpoint": {
    ///             "url": "http://petstore.swagger.wordnik.com/oauth/requestToken",
    ///             "clientIdName": "client_id",
    ///             "clientSecretName": "client_secret"
    ///           },
    ///           "tokenEndpoint": {
    ///             "url": "http://petstore.swagger.wordnik.com/oauth/token",
    ///             "tokenName": "access_code"
    ///           }
    ///         }
    ///       }
    ///     }
    ///   },
    ///   "info": {
    ///     "title": "Swagger Sample App",
    ///     "description": "This is a sample server Petstore server.  You can find out more about Swagger \n    at <a href="http://swagger.wordnik.com">http://swagger.wordnik.com</a> or on irc.freenode.net, #swagger.  For this sample,\n    you can use the api key \"special-key\" to test the authorization filters",
    ///     "termsOfServiceUrl": "http://helloreverb.com/terms/",
    ///     "contact": "apiteam@wordnik.com",
    ///     "license": "Apache 2.0",
    ///     "licenseUrl": "http://www.apache.org/licenses/LICENSE-2.0.html"
    ///   }
    /// }
    /// </code>
    /// </example>
    public class ResourceListing : SwaggerModel
    {
        public ResourceListing()
        {
            SwaggerVersion = "1.2"; // TODO: Specify this centrally
            Apis = new Resource[0];
        }

        /// <summary>
        /// Specifies the Swagger Specification version being used.
        /// It can be used by the Swagger UI and other clients to interpret the API listing. 
        /// </summary>
        /// <remarks>
        /// The value MUST be an existing Swagger specification version.
        /// </remarks>
        [SwaggerProperty("swaggerVersion", true)]
        public string SwaggerVersion { get; set; }

        /// <summary>
        /// Lists the resources to be described by this specification implementation.
        /// </summary>
        [SwaggerProperty("apis", true)]
        public IEnumerable<Resource> Apis { get; set; }

        /// <summary>
        /// Provides the version of the application API.
        /// </summary>
        [SwaggerProperty("apiVersion")]
        public string ApiVersion { get; set; }

        /// <summary>
        /// Provides metadata about the API. 
        /// The metadata can be used by the clients if needed, 
        /// and can be presented in the Swagger-UI for convenience.
        /// </summary>
        [SwaggerProperty("info")]
        public Info Info { get; set; }

        /// <summary>
        /// Provides information about the the authorization schemes allowed on his API.
        /// </summary>
        [SwaggerProperty("authorizations")]
        public IDictionary<string, Authorization> Authorizations { get; set; }
    }
}