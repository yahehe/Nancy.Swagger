//  <copyright file="PathItemBuilder.cs" company="Premise Health">
//      Copyright (c) 2015 Premise Health. All rights reserved.
//  </copyright>

namespace Swagger.ObjectModel.Builders
{
    using System.Collections.Generic;

    /// <summary>
    /// The path item builder.
    /// </summary>
    public class PathItemBuilder
    {
        /// <summary>
        /// The operations.
        /// </summary>
        private IDictionary<HttpMethod, Operation> operations;

        /// <summary>
        /// The parameters.
        /// </summary>
        private List<Parameter> parameters;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="PathItem"/>.
        /// </returns>
        public PathItem Build()
        {
            return new PathItem { Operations = this.operations, Parameters = this.parameters };
        }

        /// <summary>
        /// Define an operation on the path
        /// </summary>
        /// <param name="httpMethod">
        /// The method.
        /// </param>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Operation(HttpMethod httpMethod, Operation operation)
        {
            if (this.operations == null)
            {
                this.operations = new Dictionary<HttpMethod, Operation>();
            }

            this.operations.Add(httpMethod, operation);
            return this;
        }

        /// <summary>
        /// Define a GET operation
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Get(Operation operation)
        {
            return this.Operation(HttpMethod.Get, operation);
        }


        /// <summary>
        /// Define a PUT operation
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Put(Operation operation)
        {
            return this.Operation(HttpMethod.Put, operation);
        }

        /// <summary>
        /// Define a Post operation
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Post(Operation operation)
        {
            return this.Operation(HttpMethod.Post, operation);
        }

        /// <summary>
        /// Define a Delete operation
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Delete(Operation operation)
        {
            return this.Operation(HttpMethod.Delete, operation);
        }

        /// <summary>
        /// Define a Options operation
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Options(Operation operation)
        {
            return this.Operation(HttpMethod.Options, operation);
        }

        /// <summary>
        /// Define a Head operation
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Head(Operation operation)
        {
            return this.Operation(HttpMethod.Head, operation);
        }

        /// <summary>
        /// Define a Patch operation
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Patch(Operation operation)
        {
            return this.Operation(HttpMethod.Patch, operation);
        }


        /// <summary>
        /// Add a parameter for this operation
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Parameter(Parameter parameter)
        {
            if (this.parameters == null)
            {
                this.parameters = new List<Parameter>();
            }

            this.parameters.Add(parameter);
            return this;
        }

        /// <summary>
        /// Add a parameter for this operation
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Parameter(ParameterBuilder parameter)
        {
            return this.Parameter(parameter.Build());
        }

        /// <summary>
        /// Add a body parameter for this operation
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Parameter(BodyParameterBuilder parameter)
        {
            return this.Parameter(parameter.Build());
        }

        /// <summary>
        /// Add parameters that are valid for this operation
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Parameters(IEnumerable<Parameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                this.Parameter(parameter);
            }

            return this;
        }

    }
}