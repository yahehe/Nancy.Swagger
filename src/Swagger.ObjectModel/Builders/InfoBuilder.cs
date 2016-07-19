// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoBuilder.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The info builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel.Builders
{
    using System;

    /// <summary>
    /// The info builder.
    /// </summary>
    public class InfoBuilder
    {
        /// <summary>
        /// The contact.
        /// </summary>
        private Contact contact;

        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The license.
        /// </summary>
        private License license;

        /// <summary>
        /// The terms of service.
        /// </summary>
        private string termsOfService;

        /// <summary>
        /// The title.
        /// </summary>
        private readonly string title;

        /// <summary>
        /// The version.
        /// </summary>
        private readonly string version;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoBuilder"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        public InfoBuilder(string title, string version)
        {
            this.title = title;
            this.version = version;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="Info"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Info Build()
        {
            if (string.IsNullOrWhiteSpace(this.title))
            {
                throw new RequiredFieldException("Title");

            }

            if (string.IsNullOrWhiteSpace(this.version))
            {
                throw new RequiredFieldException("Version");
            }

            return new Info
                       {
                           Title = this.title,
                           Version = this.version,
                           Contact = this.contact,
                           Description = this.description,
                           License = this.license,
                           TermsOfService = this.termsOfService,
                       };
        }

        /// <summary>
        /// The description.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="InfoBuilder"/>.
        /// </returns>
        public InfoBuilder Description(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// The terms of service.
        /// </summary>
        /// <param name="termsOfService">
        /// The terms of service.
        /// </param>
        /// <returns>
        /// The <see cref="InfoBuilder"/>.
        /// </returns>
        public InfoBuilder TermsOfService(string termsOfService)
        {
            this.termsOfService = termsOfService;
            return this;
        }

        /// <summary>
        /// The contact.
        /// </summary>
        /// <param name="contact">
        /// The contact.
        /// </param>
        /// <returns>
        /// The <see cref="InfoBuilder"/>.
        /// </returns>
        public InfoBuilder Contact(Contact contact)
        {
            this.contact = contact;
            return this;
        }

        /// <summary>
        /// The contact.
        /// </summary>
        /// <param name="contact">
        /// The contact.
        /// </param>
        /// <returns>
        /// The <see cref="InfoBuilder"/>.
        /// </returns>
        public InfoBuilder Contact(ContactBuilder contact)
        {
            this.contact = contact.Build();
            return this;
        }

        /// <summary>
        /// The license.
        /// </summary>
        /// <param name="license">
        /// The license.
        /// </param>
        /// <returns>
        /// The <see cref="InfoBuilder"/>.
        /// </returns>
        public InfoBuilder License(License license)
        {
            this.license = license;
            return this;
        }

        /// <summary>
        /// The license.
        /// </summary>
        /// <param name="license">
        /// The license.
        /// </param>
        /// <returns>
        /// The <see cref="InfoBuilder"/>.
        /// </returns>
        public InfoBuilder License(LicenseBuilder license)
        {
            this.license = license.Build();
            return this;
        }
    }
}