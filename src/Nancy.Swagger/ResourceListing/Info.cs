using System;
using Nancy.Swagger.Attributes;
using Newtonsoft.Json;

namespace Nancy.Swagger.ResourceListing
{
    /// <summary>
    /// The Info object provides metadata about the API. 
    /// The metadata can be used by the clients if needed, and can be presented in the Swagger-UI for convenience.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "title": "Swagger Sample App",
    ///   "description": "This is a sample server Petstore server.",
    ///   "termsOfServiceUrl": "http://helloreverb.com/terms/",
    ///   "contact": "apiteam@wordnik.com",
    ///   "license": "Apache 2.0",
    ///   "licenseUrl": "http://www.apache.org/licenses/LICENSE-2.0.html"
    /// }
    /// </code>
    /// </example>
    [SwaggerData]
    public class Info
    {
        /// <summary>
        /// The title of the application.
        /// </summary>
        [Required]
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// A short description of the application.
        /// </summary>
        [Required]
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// A URL to the Terms of Service of the API.
        /// </summary>
        [JsonProperty("termsOfServiceUrl")]
        public Uri TermsOfServiceUrl { get; set; }

        /// <summary>
        /// An email to be used for API-related correspondence.
        /// </summary>
        [JsonProperty("contact")]
        public string Contact { get; set; }

        /// <summary>
        /// The license name used for the API.
        /// </summary>
        [JsonProperty("license")]
        public string License { get; set; }

        /// <summary>
        /// A URL to the license used for the API.
        /// </summary>
        [JsonProperty("licenseUrl")]
        public Uri LicenseUrl { get; set; }
    }
}