using System;
using Swagger.ObjectModel.Attributes;

namespace Swagger.ObjectModel.ResourceListing
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
    public class Info : SwaggerModel
    {
        /// <summary>
        /// The title of the application.
        /// </summary>
        [SwaggerProperty("title", true)]
        public string Title { get; set; }

        /// <summary>
        /// A short description of the application.
        /// </summary>
        [SwaggerProperty("description", true)]
        public string Description { get; set; }

        /// <summary>
        /// The Terms of Service for the API.
        /// </summary>
        [SwaggerProperty("termsOfService")]
        public Uri TermsOfService { get; set; }

        /// <summary>
        /// The contact information for the exposed API.
        /// </summary>
        [SwaggerProperty("contact")]
        public Contact Contact { get; set; }

        /// <summary>
        /// The license information for the exposed API.
        /// </summary>
        [SwaggerProperty("license")]
        public License License { get; set; }

        /// <summary>
        /// Provides the version of the application API (not to be confused by the specification version).
        /// </summary>
        public string Version { get; set; }
    }
}