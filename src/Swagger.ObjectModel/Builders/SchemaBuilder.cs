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
    public class SchemaBuilder<TModel> : DataTypeBuilder<SchemaBuilder<TModel>, Schema>
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

        private IDictionary<string, Schema> properties = new Dictionary<string, Schema>();

        private List<string> required = new List<string>();

        private List<string> allOf = new List<string>();

        protected override Schema DataTypeInstance
        {
            get
            {
                base.DataTypeInstance.Discriminator = this.discriminator;
                base.DataTypeInstance.ReadOnly = this.readOnly;
                base.DataTypeInstance.ExternalDocumentation = this.documentation;
                base.DataTypeInstance.Example = this.example;
                base.DataTypeInstance.Properties = this.properties;
                base.DataTypeInstance.AllOf = this.allOf;
                base.DataTypeInstance.Required = this.required;

                return base.DataTypeInstance;
            }
        }

        /// <summary>
        /// Access a <see cref="SchemaBuilder{TProperty}"/> for a given property of the model.
        /// </summary>
        /// <param name="expression">An <see cref="Expression{TDelegate}"/> for accessing the property.</param>
        /// <returns>The <see cref="SchemaBuilder{TProperty}"/> instance.</returns>
        public SchemaBuilder<TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException("Expression is not a member access", "expression");
            }
            
            var builder = new SchemaBuilder<TProperty>();
            this.properties.Add(member.Member.Name, builder.DataTypeInstance);

            builder.Type(typeof(TProperty).Name);

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
            return this.DataTypeInstance;
        }

        public SchemaBuilder<TModel> Discriminator(string discriminator)
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
        public SchemaBuilder<TModel> IsReadOnly()
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
        public SchemaBuilder<TModel> ExternalDocumentation(ExternalDocumentation documentation)
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
        public SchemaBuilder<TModel> ExternalDocumentation(ExternalDocumentationBuilder documentation)
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
        public SchemaBuilder<TModel> Example(object example)
        {
            this.example = example;
            return this;
        }
    }
}