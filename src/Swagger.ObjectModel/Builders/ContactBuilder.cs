// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContactBuilder.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The contact builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel.Builders
{
    /// <summary>
    /// The contact builder.
    /// </summary>
    public class ContactBuilder
    {
        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The email address.
        /// </summary>
        private string emailAddress;

        /// <summary>
        /// The url.
        /// </summary>
        private string url;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="Contact"/>.
        /// </returns>
        public Contact Build()
        {
            return new Contact { Name = this.name, EmailAddress = this.emailAddress, Url = this.url, };
        }

        /// <summary>
        /// The name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="ContactBuilder"/>.
        /// </returns>
        public ContactBuilder Name(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// The email address.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="ContactBuilder"/>.
        /// </returns>
        public ContactBuilder EmailAddress(string emailAddress)
        {
            this.emailAddress = emailAddress;
            return this;
        }

        /// <summary>
        /// The url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="ContactBuilder"/>.
        /// </returns>
        public ContactBuilder Url(string url)
        {
            this.url = url;
            return this;
        }
    }
}