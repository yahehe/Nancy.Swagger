// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagBuilder.cs" company="CHS Health Services">
//   Copyright (c) 2015 CHS Health Services. All rights reserved.
// </copyright>
// <summary>
//   The tag builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Swagger.ObjectModel.Builders
{
    /// <summary>
    /// The tag builder.
    /// </summary>
    public class TagBuilder
    {
        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The documentation.
        /// </summary>
        private ExternalDocumentation documentation;

        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="Tag"/>.
        /// </returns>
        /// <exception cref="RequiredFieldException">
        /// </exception>
        public Tag Build()
        {
            if (string.IsNullOrWhiteSpace(this.name))
            {
                throw new RequiredFieldException("Name");
            }

            return new Tag { Description = this.description, ExternalDocumentation = this.documentation, Name = this.name, };
        }

        /// <summary>
        /// The name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        public TagBuilder Name(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// The description.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        public TagBuilder Description(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// The external documentation.
        /// </summary>
        /// <param name="externalDocumentation">
        /// The external documentation.
        /// </param>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        public TagBuilder ExternalDocumentation(ExternalDocumentation externalDocumentation)
        {
            this.documentation = externalDocumentation;
            return this;
        }

        /// <summary>
        /// The external documentation.
        /// </summary>
        /// <param name="externalDocumentation">
        /// The external documentation.
        /// </param>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        public TagBuilder ExternalDocumentation(ExternalDocumentationBuilder externalDocumentation)
        {
            this.documentation = externalDocumentation.Build();
            return this;
        }
    }
}