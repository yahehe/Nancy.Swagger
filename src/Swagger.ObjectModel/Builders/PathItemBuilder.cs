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
        private readonly PathItem pathItem = new PathItem()
                                             {
                                                 Parameters = new List<Parameter>()
                                             };


        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="PathItem"/>.
        /// </returns>
        public PathItem Build()
        {
            return pathItem;
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
            pathItem.Get = operation;
            return this;
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
            pathItem.Put = operation;
            return this;
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
            pathItem.Post = operation;
            return this;
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
            pathItem.Delete = operation;
            return this;
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
            pathItem.Options = operation;
            return this;
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
            pathItem.Head = operation;
            return this;
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
            pathItem.Patch = operation;
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
        public PathItemBuilder Parameter(Parameter parameter)
        {
            ((List<Parameter>)pathItem.Parameters).Add(parameter);
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