// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderBuilder.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The header builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel
{
    using System.Collections.Generic;

    /// <summary>
    /// The header builder.
    /// </summary>
    public class HeaderBuilder : DataTypeBuilder<HeaderBuilder, Header>
    {
        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="Header"/>.
        /// </returns>
        public override Header Build()
        {
            var dataType = this.BuildBase();

            dataType.Description = this.description;

            return dataType;
        }

        /// <summary>
        /// The description.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="HeaderBuilder"/>.
        /// </returns>
        public HeaderBuilder Description(string description)
        {
            this.description = description;
            return this;
        }
    }
}