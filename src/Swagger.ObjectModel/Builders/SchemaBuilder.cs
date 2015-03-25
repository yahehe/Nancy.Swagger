// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SchemaBuilder.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The schema builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Swagger.ObjectModel.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// The schema builder.
    /// </summary>
    public class SchemaBuilder : DataTypeBuilder<SchemaBuilder, Schema>
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

        private IDictionary<string, SchemaBuilder> properties = new Dictionary<string, SchemaBuilder>();

        private List<string> required = new List<string>();

        private List<string> allOf = new List<string>();

        private Schema providedSchema;

        public SchemaBuilder(Schema provided)
        {
            this.providedSchema = provided;
        }

        public SchemaBuilder()
        {
        }

        /// <summary>
        /// Access a <see cref="SwaggerModelPropertyDataBuilder{TProperty}"/> for a given property of the model.
        /// </summary>
        /// <param name="expression">An <see cref="Expression{TDelegate}"/> for accessing the property.</param>
        /// <returns>The <see cref="SwaggerModelPropertyDataBuilder{TProperty}"/> instance.</returns>
        public SchemaBuilder Property<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException("Expression is not a member access", "expression");
            }

            if (this.properties == null)
            {
                this.properties = new Dictionary<string, SchemaBuilder>();
            }

            var builder = new SchemaBuilder();
            this.properties.Add(member.Member.Name, builder);
            return builder;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="Schema"/>.
        /// </returns>
        public override Schema Build()
        {
            var schema = this.BuildBase();

            schema.Discriminator = this.discriminator;
            schema.ReadOnly = this.readOnly;
            schema.ExternalDocumentation = this.documentation;
            schema.Example = this.example;

            if (this.properties != null)
            {
                schema.Properties = this.properties.ToDictionary(x => x.Key, x => x.Value.Build());
            }

            schema.AllOf = this.allOf;
            schema.Required = this.required;

            return schema;
        }

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