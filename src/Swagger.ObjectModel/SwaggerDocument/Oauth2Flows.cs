namespace Swagger.ObjectModel.SwaggerDocument
{
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The oauth 2 flows.
    /// </summary>
    [SwaggerData]
    public enum Oauth2Flows
    {
        /// <summary>
        /// The implicit.
        /// </summary>
        [SwaggerEnumValue("implicit")]
        Implicit,

        /// <summary>
        /// The password.
        /// </summary>
        [SwaggerEnumValue("password")]
        Password,

        /// <summary>
        /// The application.
        /// </summary>
        [SwaggerEnumValue("application")]
        Application,

        /// <summary>
        /// The access code.
        /// </summary>
        [SwaggerEnumValue("accesscode")]
        AccessCode
    }
}