// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerRoot.cs" company="CHS Health Services">
//   Copyright (c) 2015 CHS Health Services. All rights reserved.
// </copyright>
// <summary>
//   The swagger root.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Swagger.ObjectModel
{
    using System.Collections.Generic;

    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The swagger root.
    /// </summary>
    public class SwaggerRoot : SwaggerModel
    {
        /// <summary>
        /// The swagger version.
        /// </summary>
        private const string Version = "3.0.0";

        /// <summary>
        /// Specifies the Swagger Specification version being used.
        /// It can be used by the Swagger UI and other clients to interpret the API listing.
        /// </summary>
        /// <remarks>
        /// The value MUST be an existing Swagger specification version.
        /// </remarks>
        [SwaggerProperty("openapi", true)]
        public string OpenAPIVersion
        {
            get
            {
                return Version;
            }
        }

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
        [SwaggerProperty("paths", true)]
        public IDictionary<string, PathItem> Paths { get; set; }

        /// <summary>
        /// An object to hold data types produced and consumed by operations.
        /// </summary>
        [SwaggerProperty("definitions")]
        public IDictionary<string, Schema> Definitions { get; set; }

        /// <summary>
        /// An object to hold parameters that can be used across operations. This property does not define global parameters for all operations.
        /// </summary>
        [SwaggerProperty("parameters")]
        public IDictionary<string, Parameter> Parameters { get; set; }

        /// <summary>
        /// An object to hold responses that can be used across operations. This property does not define global responses for all operations.
        /// A single response definition, mapping a "name" to the response it defines.
        /// </summary>
        [SwaggerProperty("responses")]
        public IDictionary<string, Response> Responses { get; set; }

        /// <summary>
        /// A declaration of the security schemes available to be used in the specification. 
        /// This does not enforce the security schemes on the operations and only serves to provide the relevant details for each scheme.
        /// </summary>
        [SwaggerProperty("securityDefinitions")]
        public IDictionary<string, SecurityScheme> SecurityDefinitions { get; set; }

        /// <summary>
        /// Lists the required security schemes to execute this operation. 
        /// The object can have multiple security schemes declared in it which are all required (that is, there is a logical AND between the schemes).
        /// The name used for each property MUST correspond to a security scheme declared in the Security Definitions.
        /// </summary>
        [SwaggerProperty("security")]
        public IDictionary<SecuritySchemes, IEnumerable<string>> Security { get; set; }

        /// <summary>
        /// A list of tags used by the specification with additional metadata. 
        /// The order of the tags can be used to reflect on their order by the parsing tools. 
        /// Not all tags that are used by the Operation Object must be declared. 
        /// The tags that are not declared may be organized randomly or based on the tools' logic. 
        /// Each tag name in the list MUST be unique.
        /// </summary>
        [SwaggerProperty("tags")]
        public IEnumerable<Tag> Tags { get; set; }

        /// <summary>
        /// Additional external documentation.
        /// </summary>
        [SwaggerProperty("externalDocs")]
        public ExternalDocumentation ExternalDocumentation { get; set; }
    }
}