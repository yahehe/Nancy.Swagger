//  <copyright file="BodyParameterBuilder.cs" company="Premise Health">
//      Copyright (c) 2015 Premise Health. All rights reserved.
//  </copyright>

using System;

namespace Swagger.ObjectModel.Builders
{
    /// <summary>
    /// The body parameter builder.
    /// </summary>
    public class BodyParameterBuilder
    {
        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The schema.
        /// </summary>
        private Schema schema;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="Parameter"/>.
        /// </returns>
        public BodyParameter Build()
        {
            if (string.IsNullOrWhiteSpace(this.name))
            {
                throw new RequiredFieldException("Name");
            }

            if (this.schema == null)
            {
                throw new RequiredFieldException("Schema");
            }

            var parameter = new BodyParameter { Name = this.name, In = ParameterIn.Body, Description = this.description, Required = true, Schema = this.schema };

            return parameter;
        }

        /// <summary>
        /// Add the name
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="ParameterBuilder"/>.
        /// </returns>
        public BodyParameterBuilder Name(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// Add the description
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="ParameterBuilder"/>.
        /// </returns>
        public BodyParameterBuilder Description(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// Add The schema defining the type used for the body parameter.
        /// </summary>
        /// <param name="schema">
        /// The schema.
        /// </param>
        /// <returns>
        /// The <see cref="BodyParameterBuilder"/>.
        /// </returns>
        public BodyParameterBuilder Schema(Schema schema)
        {
            this.schema = schema;
            return this;
        }

        /// <summary>
        /// Add The schema defining the type used for the body parameter.
        /// </summary>
        /// <param name="schema">
        /// The schema.
        /// </param>
        /// <returns>
        /// The <see cref="BodyParameterBuilder"/>.
        /// </returns>
        public BodyParameterBuilder Schema<T>(Action<SchemaBuilder<T>> schema)
        {
            var builder = new SchemaBuilder<T>();
            schema(builder);
            this.schema = builder.Build();
            return this;
        }

        /// <summary>
        /// Add The schema defining the type used for the body parameter.
        /// </summary>
        /// <returns>
        /// The <see cref="BodyParameterBuilder"/>.
        /// </returns>
        public BodyParameterBuilder Schema<T>()
        {
            var builder = new SchemaBuilder<T>();
            this.schema = builder.Build();
            return this;
        }
    }
}