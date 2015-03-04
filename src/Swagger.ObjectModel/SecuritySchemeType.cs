namespace Swagger.ObjectModel
{
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The security scheme type.
    /// </summary>
    [SwaggerData]
    public enum SecuritySchemeType
    {
        /// <summary>
        /// The basic.
        /// </summary>
        [SwaggerEnumValue("basic")]
        Basic,

        /// <summary>
        /// The api key.
        /// </summary>
        [SwaggerEnumValue("apiKey")]
        ApiKey,

        /// <summary>
        /// The oauth 2.
        /// </summary>
        [SwaggerEnumValue("oauth2")]
        Oauth2
    }
}