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
    using System.Linq.Expressions;

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

        private string description;

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
                base.DataTypeInstance.Description = this.description;

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

        #region Building

        public SchemaBuilder<TModel> Discriminator(string discriminator)
        {
            this.discriminator = discriminator;
            return this;
        }

        private SchemaBuilder<TModel> Description(string description)
        {
            this.description = description;
            return this;
        }

        public SchemaBuilder<TModel> IsReadOnly()
        {
            this.readOnly = true;
            return this;
        }


        public SchemaBuilder<TModel> ExternalDocumentation(ExternalDocumentation documentation)
        {
            this.documentation = documentation;
            return this;
        }

        public SchemaBuilder<TModel> ExternalDocumentation(ExternalDocumentationBuilder documentation)
        {
            this.documentation = documentation.Build();
            return this;
        }

        public SchemaBuilder<TModel> Example(object example)
        {
            this.example = example;
            return this;
        }

        #endregion
    }
}