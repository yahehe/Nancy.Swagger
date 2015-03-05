// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterBuilder.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The parameter builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel.Builders
{
    /// <summary>
    /// The parameter builder.
    /// </summary>
    public class ParameterBuilder : DataTypeBuilder<ParameterBuilder, Parameter>
    {
        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The parameter in.
        /// </summary>
        private ParameterIn? parameterIn;

        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The required.
        /// </summary>
        private bool? required;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="Parameter"/>.
        /// </returns>
        /// <exception cref="RequiredFieldException">
        /// </exception>
        public override Parameter Build()
        {
            if (string.IsNullOrWhiteSpace(this.name))
            {
                throw new RequiredFieldException("Name");
            }

            if (this.parameterIn == null)
            {
                throw new RequiredFieldException("In");
            }

            if (this.parameterIn == ParameterIn.Path)
            {
                this.IsRequired();
            }

            var parameter = this.BuildBase();

            parameter.Name = this.name;
            parameter.In = this.parameterIn.Value;
            parameter.Description = this.description;
            parameter.Required = this.required;

            return parameter;
        }

        /// <summary>
        /// The name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="ParameterBuilder"/>.
        /// </returns>
        private ParameterBuilder Name(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// The in.
        /// </summary>
        /// <param name="parameterIn">
        /// The parameter in.
        /// </param>
        /// <returns>
        /// The <see cref="ParameterBuilder"/>.
        /// </returns>
        private ParameterBuilder In(ParameterIn parameterIn)
        {
            this.parameterIn = parameterIn;
            return this;
        }

        /// <summary>
        /// The description.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="ParameterBuilder"/>.
        /// </returns>
        private ParameterBuilder Description(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// The is required.
        /// </summary>
        /// <returns>
        /// The <see cref="ParameterBuilder"/>.
        /// </returns>
        private ParameterBuilder IsRequired()
        {
            this.required = true;
            return this;
        }
    }
}