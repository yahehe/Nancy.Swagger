using System.Collections.Generic;
using Swagger.ObjectModel.Attributes;

namespace Swagger.ObjectModel.ResourceListing
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
        /// <summary>
        /// Creates an instance of a <see cref="ResourceListing"/>.
        /// </summary>
        public ResourceListing()
        {
            SwaggerVersion = "2.0"; // TODO: Specify this centrally
            Apis = new Resource[0];
        }

        /// <summary>
        /// Specifies the Swagger Specification version being used.
        /// It can be used by the Swagger UI and other clients to interpret the API listing.
        /// </summary>
        /// <remarks>
        /// The value MUST be an existing Swagger specification version.
        /// </remarks>
        [SwaggerProperty("swagger", true)]
        public string SwaggerVersion { get; set; }

        /// <summary>
        /// Provides metadata about the API. The metadata can be used by the clients if needed, and can be presented in the Swagger-UI for convenience.
        /// </summary>
        [SwaggerProperty("info", true)]
        public Info Info { get; set; }

        /// <summary>
        /// The host (name or ip) serving the API. This MUST be the host only and does not include the scheme nor sub-paths. 
        /// It MAY include a port. If the host is not included, the host serving the documentation is to be used (including the port). 
        /// The host does not support path templating.
        /// </summary>
        [SwaggerProperty("host")]
        public string Host { get; set; }

        /// <summary>
        /// The base path on which the API is served, which is relative to the host. If it is not included, the API is served directly under the host. 
        /// The value MUST start with a leading slash (/). The basePath does not support path templating.
        /// </summary>
        [SwaggerProperty("basePath")]
        public string BasePath { get; set; }

        /// <summary>
        /// The transfer protocol of the API. Values MUST be from the list: "http", "https", "ws", "wss". 
        /// If the schemes is not included, the default scheme to be used is the one used to access the specification.
        /// </summary>
        [SwaggerProperty("schemes")]
        public IEnumerable<Schemes> Schemes { get; set; }

        /// <summary>
        /// A list of MIME types the APIs can consume. This is global to all APIs but can be overridden on specific API calls. 
        /// Value MUST be as described under Mime Types.
        /// </summary>
        [SwaggerProperty("consumes")]
        public IEnumerable<string> Consumes { get; set; }

        /// <summary>
        /// A list of MIME types the APIs can produce. This is global to all APIs but can be overridden on specific API calls. 
        /// Value MUST be as described under Mime Types.
        /// </summary>
        [SwaggerProperty("produces")]
        public IEnumerable<string> Produces { get; set; }

        /// <summary>
        /// The available paths and operations for the API.
        /// Key: A relative path to an individual endpoint. The field name MUST begin with a slash. 
        /// The path is appended to the basePath in order to construct the full URL. Path templating is allowed.
        /// </summary>
        [SwaggerProperty("path", true)]
        public IDictionary<string, PathItem> Paths { get; set; }

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
        /// Provides information about the the authorization schemes allowed on his API.
        /// </summary>
        [SwaggerProperty("authorizations")]
        public IDictionary<string, Authorization> Authorizations { get; set; }
    }
}