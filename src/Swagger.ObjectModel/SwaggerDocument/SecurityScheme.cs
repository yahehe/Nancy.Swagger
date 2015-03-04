namespace Swagger.ObjectModel.SwaggerDocument
{
    using System.Collections;
    using System.Collections.Generic;

    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The security scheme.
    /// </summary>
    public class SecurityScheme : SwaggerModel
    {
        /// <summary>
        /// The type of the security scheme
        /// </summary>
        [SwaggerProperty("type", true)]
        public SecuritySchemeType Type { get; set; }

        /// <summary>
        /// A short description for security scheme.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The name of the header or query parameter to be used.
        /// </summary>
        /// <remarks>
        /// Valid and required when <see cref="Type"/> is <see cref="SecuritySchemeType.ApiKey"/>
        /// </remarks>
        [SwaggerProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The location of the API key.
        /// </summary>
        /// <remarks>
        /// Valid and required when <see cref="Type"/> is <see cref="SecuritySchemeType.ApiKey"/>
        /// </remarks>
        [SwaggerProperty("in")]
        public ApiKeyLocations? In { get; set; }

        /// <summary>
        /// The flow used by the OAuth2 security scheme.
        /// </summary>
        /// <remarks>
        /// Valid and required when <see cref="Type"/> is <see cref="SecuritySchemeType.Oauth2"/>
        /// and <see cref="Flow"/> is <see cref="Oauth2Flows.AccessCode"/> or <see cref="Oauth2Flows.Implicit"/>
        /// </remarks>
        [SwaggerProperty("flow")]
        public Oauth2Flows? Flow { get; set; }

        /// <summary>
        /// The authorization URL to be used for this flow. This SHOULD be in the form of a URL.
        /// </summary>
        /// <remarks>
        /// Valid and required when <see cref="Type"/> is <see cref="SecuritySchemeType.Oauth2"/>
        /// and <see cref="Flow"/> is <see cref="Oauth2Flows.AccessCode"/> or <see cref="Oauth2Flows.Implicit"/>
        /// </remarks>
        [SwaggerProperty("authorizationUrl")]
        public string AuthorizationUrl { get; set; }

        /// <summary>
        /// The token URL to be used for this flow. This SHOULD be in the form of a URL.
        /// </summary>
        /// <remarks>
        /// Valid and required when <see cref="Type"/> is <see cref="SecuritySchemeType.Oauth2"/>
        /// and <see cref="Flow"/> is <see cref="Oauth2Flows.AccessCode"/> or <see cref="Oauth2Flows.Application"/> or <see cref="Oauth2Flows.Password"/>
        /// </remarks>
        [SwaggerProperty("tokenUrl", true)]
        public string TokenUrl { get; set; }

        /// <summary>
        /// Lists the available scopes for an OAuth2 security scheme.
        /// </summary>
        /// <remarks>
        /// Maps between a name of a scope to a short description of it (as the value of the property).
        /// Valid and required when <see cref="Type"/> is <see cref="SecuritySchemeType.Oauth2"/>
        /// </remarks>
        [SwaggerProperty("scopes")]
        public IDictionary<string, string> Scopes { get; set; }
    }
}