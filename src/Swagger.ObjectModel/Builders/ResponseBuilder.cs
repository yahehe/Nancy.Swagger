//  <copyright file="ResponseBuilder.cs" company="Premise Health">
//      Copyright (c) 2015 Premise Health. All rights reserved.
//  </copyright>

namespace Swagger.ObjectModel.Builders
{
    using System.Collections.Generic;

    /// <summary>
    /// The response builder.
    /// </summary>
    public class ResponseBuilder
    {
        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The schema.
        /// </summary>
        private Schema schema;

        /// <summary>
        /// The headers.
        /// </summary>
        private IDictionary<string, Header> headers;

        /// <summary>
        /// The examples.
        /// </summary>
        private IDictionary<string, object> examples;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="Response"/>.
        /// </returns>
        /// <exception cref="RequiredFieldException">
        /// </exception>
        public Response Build()
        {
            if (string.IsNullOrWhiteSpace(this.description))
            {
                throw new RequiredFieldException("Description");
            }

            return new Response { Description = this.description, Schema = this.schema, Headers = this.headers, Examples = this.examples };
        }

        /// <summary>
        /// The description.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="ResponseBuilder"/>.
        /// </returns>
        public ResponseBuilder Description(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// The schema.
        /// </summary>
        /// <param name="schema">
        /// The schema.
        /// </param>
        /// <returns>
        /// The <see cref="ResponseBuilder"/>.
        /// </returns>
        public ResponseBuilder Schema(Schema schema)
        {
            this.schema = schema;
            return this;
        }

        /// <summary>
        /// The schema.
        /// </summary>
        /// <param name="schema">
        /// The schema.
        /// </param>
        /// <returns>
        /// The <see cref="ResponseBuilder"/>.
        /// </returns>
        public ResponseBuilder Schema(SchemaBuilder schema)
        {
            this.schema = schema.Build();
            return this;
        }

        /// <summary>
        /// The header.
        /// </summary>
        /// <param name="headerName">
        /// The header name.
        /// </param>
        /// <param name="header">
        /// The header.
        /// </param>
        /// <returns>
        /// The <see cref="ResponseBuilder"/>.
        /// </returns>
        public ResponseBuilder Header(string headerName, Header header)
        {
            if (this.headers == null)
            {
                this.headers = new Dictionary<string, Header>();
            }

            this.headers.Add(headerName, header);
            return this;
        }

        /// <summary>
        /// The header.
        /// </summary>
        /// <param name="headerName">
        /// The header name.
        /// </param>
        /// <param name="header">
        /// The header.
        /// </param>
        /// <returns>
        /// The <see cref="ResponseBuilder"/>.
        /// </returns>
        public ResponseBuilder Header(string headerName, HeaderBuilder header)
        {
            return this.Header(headerName, header.Build());
        }

        /// <summary>
        /// The example.
        /// </summary>
        /// <param name="mimeType">
        /// The mime type.
        /// </param>
        /// <param name="example">
        /// The example.
        /// </param>
        /// <returns>
        /// The <see cref="ResponseBuilder"/>.
        /// </returns>
        public ResponseBuilder Example(string mimeType, object example)
        {
            if (this.examples == null)
            {
                this.examples = new Dictionary<string, object>();
            }

            this.examples.Add(mimeType, example);
            return this;
        }
    }
}