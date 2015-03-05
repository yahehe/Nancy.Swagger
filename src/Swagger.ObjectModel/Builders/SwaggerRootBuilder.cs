// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerRootBuilder.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The swagger root builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The swagger root builder.
    /// </summary>
    public class SwaggerRootBuilder
    {
        /// <summary>
        /// The paths.
        /// </summary>
        private IDictionary<string, PathItem> paths;

        /// <summary>
        /// The info.
        /// </summary>
        private Info info;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerRootBuilder"/> class. 
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public SwaggerRootBuilder(Info info)
        {
            this.info = info;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="SwaggerRoot"/>.
        /// </returns>
        public SwaggerRoot Build()
        {
            if (this.info == null)
            {
                throw new RequiredFieldException("Info");
            }

            if (this.paths == null || !this.paths.Any())
            {
                throw new RequiredFieldException("Paths");
            }

            return new SwaggerRoot { Info = this.info, Paths = this.paths };
        }

        SwaggerRootBuilder Info(Info info)
        {
            this.info = info;
            return this;
        }

        SwaggerRootBuilder Info(InfoBuilder info)
        {
            this.info = info.Build();
            return this;
        }

        SwaggerRootBuilder Path(string endpointPath)
        {
            this.info = info.Build();
            return this;
        }

    }
}