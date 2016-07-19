// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LicenseBuilder.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The license builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel.Builders
{
    /// <summary>
    /// The license builder.
    /// </summary>
    public class LicenseBuilder
    {
        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The url.
        /// </summary>
        private string url;

        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseBuilder"/> class. 
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public LicenseBuilder(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="License"/>.
        /// </returns>
        public License Build()
        {
            if (string.IsNullOrWhiteSpace(this.name))
            {
                throw new RequiredFieldException("Name");
            }

            return new License { Name = this.name, Url = this.url, };
        }

        /// <summary>
        /// The name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="LicenseBuilder"/>.
        /// </returns>
        public LicenseBuilder Name(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// The url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="LicenseBuilder"/>.
        /// </returns>
        public LicenseBuilder Url(string url)
        {
            this.url = url;
            return this;
        }
    }
}