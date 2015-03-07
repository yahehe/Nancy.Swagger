// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SchemaBuilder.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The schema builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel
{
    /// <summary>
    /// The schema builder.
    /// </summary>
    public class SchemaBuilder
    {
        /// <summary>
        /// The discriminator.
        /// </summary>
        private string discriminator;

        /// <summary>
        /// The read only.
        /// </summary>
        private bool? readOnly;

        /// <summary>
        /// The documentation.
        /// </summary>
        private ExternalDocumentation documentation;

        /// <summary>
        /// The example.
        /// </summary>
        private object example;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="Schema"/>.
        /// </returns>
        public Schema Build()
        {
            return new Schema
                       {
                           Discriminator = this.discriminator,
                           ReadOnly = this.readOnly,
                           ExternalDocumentation = this.documentation,
                           Example = this.example
                       };
        }

        /// <summary>
        /// The discriminator.
        /// </summary>
        /// <param name="discriminator">
        /// The discriminator.
        /// </param>
        /// <returns>
        /// The <see cref="SchemaBuilder"/>.
        /// </returns>
        public SchemaBuilder Discriminator(string discriminator)
        {
            this.discriminator = discriminator;
            return this;
        }

        /// <summary>
        /// The is read only.
        /// </summary>
        /// <returns>
        /// The <see cref="SchemaBuilder"/>.
        /// </returns>
        public SchemaBuilder IsReadOnly()
        {
            this.readOnly = true;
            return this;
        }

        /// <summary>
        /// The external documentation.
        /// </summary>
        /// <param name="documentation">
        /// The documentation.
        /// </param>
        /// <returns>
        /// The <see cref="SchemaBuilder"/>.
        /// </returns>
        public SchemaBuilder ExternalDocumentation(ExternalDocumentation documentation)
        {
            this.documentation = documentation;
            return this;
        }

        /// <summary>
        /// The external documentation.
        /// </summary>
        /// <param name="documentation">
        /// The documentation.
        /// </param>
        /// <returns>
        /// The <see cref="SchemaBuilder"/>.
        /// </returns>
        public SchemaBuilder ExternalDocumentation(ExternalDocumentationBuilder documentation)
        {
            this.documentation = documentation.Build();
            return this;
        }

        /// <summary>
        /// The example.
        /// </summary>
        /// <param name="example">
        /// The example.
        /// </param>
        /// <returns>
        /// The <see cref="SchemaBuilder"/>.
        /// </returns>
        public SchemaBuilder Example(object example)
        {
            this.example = example;
            return this;
        }
    }
}