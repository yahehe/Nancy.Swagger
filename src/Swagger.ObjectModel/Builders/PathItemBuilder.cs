//  <copyright file="PathItemBuilder.cs" company="Premise Health">
//      Copyright (c) 2015 Premise Health. All rights reserved.
//  </copyright>

using System;

namespace Swagger.ObjectModel.Builders
{
    using System.Collections.Generic;

    /// <summary>
    /// The path item builder.
    /// </summary>
    public class PathItemBuilder
    {
        private readonly List<Parameter> parameters = new List<Parameter>();
        private Operation operation = new Operation();
        private readonly string method;

        public PathItemBuilder(string method)
        {
            this.method = method;
        }


        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="PathItem"/>.
        /// </returns>
        public PathItem Build()
        {
            var item = new PathItem()
                       {
                           Parameters = parameters
                       };

            switch (method.ToLowerInvariant())
            {
                case "get":
                    item.Get = operation;
                    break;
                case "post":
                    item.Post = operation;
                    break;
                case "patch":
                    item.Patch = operation;
                    break;
                case "delete":
                    item.Delete = operation;
                    break;
                case "put":
                    item.Put = operation;
                    break;
                case "head":
                    item.Head = operation;
                    break;
                case "options":
                    item.Options = operation;
                    break;
            }
            return item;
        }
        /// <summary>
        /// Defines the operation for the path and method;
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// The <see cref="PathItemBuilder"/>.
        /// </returns>
        public PathItemBuilder Operation(Action<OperationBuilder> action)
        {
            var builder = new OperationBuilder();
            action(builder);
            this.operation = builder.Build();
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
            parameters.Add(parameter);
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