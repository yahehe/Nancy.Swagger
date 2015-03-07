// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExternalDocumentationBuilder.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The external documentation builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel
{
    using Swagger.ObjectModel.Builders;

    /// <summary>
    /// The external documentation builder.
    /// </summary>
    public class ExternalDocumentationBuilder
    {
        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The url.
        /// </summary>
        private string url;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="ExternalDocumentation"/>.
        /// </returns>
        public ExternalDocumentation Build()
        {
            if (string.IsNullOrWhiteSpace(this.url))
            {
                throw new RequiredFieldException("Url");
            }

            return new ExternalDocumentation { Description = this.description, Url = this.url };
        }

        /// <summary>
        /// The description.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="ExternalDocumentationBuilder"/>.
        /// </returns>
        public ExternalDocumentationBuilder Description(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// The url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="ExternalDocumentationBuilder"/>.
        /// </returns>
        public ExternalDocumentationBuilder Url(string url)
        {
            this.url = url;
            return this;
        }
    }
}