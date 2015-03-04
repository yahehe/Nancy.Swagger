namespace Swagger.ObjectModel
{
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The contact.
    /// </summary>
    public class Contact : SwaggerModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [SwaggerProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [SwaggerProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [SwaggerProperty("email")]
        public string EmailAddress { get; set; }
    }
}