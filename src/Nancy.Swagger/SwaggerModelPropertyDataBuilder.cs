namespace Nancy.Swagger
{
    [SwaggerApi]
    public class SwaggerModelPropertyDataBuilder<TProperty>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerRouteDataBuilder"/> class.
        /// </summary>
        /// <param name="data">The <see cref="SwaggerModelPropertyData"/> to build.</param>
        public SwaggerModelPropertyDataBuilder(SwaggerModelPropertyData data)
        {
            Data = data;
        }

        /// <summary>
        /// Gets the <see cref="SwaggerModelPropertyData"/> instance.
        /// </summary>
        public SwaggerModelPropertyData Data { get; private set; }

        /// <summary>
        /// Specify the default value for the property.
        /// </summary>
        /// <param name="value">The default value.</param>
        /// <returns>The <see cref="SwaggerModelPropertyDataBuilder{TProperty}"/> instance.</returns>
        public SwaggerModelPropertyDataBuilder<TProperty> Default(TProperty value)
        {
            Data.DefaultValue = value;

            return this;
        }

        /// <summary>
        /// Specify the description for the property.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>The <see cref="SwaggerModelPropertyDataBuilder{TProperty}"/> instance.</returns>
        public SwaggerModelPropertyDataBuilder<TProperty> Description(string description)
        {
            Data.Description = description;

            return this;
        }

        /// <summary>
        /// Specify a fixed list of possible values for the property. 
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="SwaggerModelPropertyDataBuilder{TProperty}"/> instance.</returns>
        /// <remarks>If this field is used in conjunction with a default value, then the default value MUST be one of the values defined in the enum.</remarks>
        public SwaggerModelPropertyDataBuilder<TProperty> Enum(params string[] values)
        {
            Data.Enum = values;

            return this;
        }

        /// <summary>
        /// Specify the maximum valid value for the property, inclusive.
        /// </summary>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>The <see cref="SwaggerModelPropertyDataBuilder{TProperty}"/> instance.</returns>
        /// <remarks>If this field is used in conjunction with a default value, then the default value MUST be lower than or equal to this value.</remarks>
        public SwaggerModelPropertyDataBuilder<TProperty> Maximum(long maximum)
        {
            Data.Maximum = maximum;

            return this;
        }

        /// <summary>
        /// Specify the minimum valid value for the property, inclusive.
        /// </summary>
        /// <param name="minimum">The minimum value.</param>
        /// <returns>The <see cref="SwaggerModelPropertyDataBuilder{TProperty}"/> instance.</returns>
        /// <remarks>If this field is used in conjunction with a default value, then the default value MUST be higher than or equal to this value.</remarks>
        public SwaggerModelPropertyDataBuilder<TProperty> Minimum(long minimum)
        {
            Data.Minimum = minimum;

            return this;
        }

        /// <summary>
        /// Specify whether the property is required or not.
        /// </summary>
        /// <param name="required">A value indicating whether the property is required.</param>
        /// <returns>The <see cref="SwaggerModelPropertyDataBuilder{TProperty}"/> instance.</returns>
        public SwaggerModelPropertyDataBuilder<TProperty> Required(bool required)
        {
            Data.Required = required;

            return this;
        }

        /// <summary>
        /// Specify whether the container allows duplicate values or not.
        /// </summary>
        /// <param name="uniqueItems">A value indicating whether the container allows duplicate values or not</param>
        /// <returns></returns>
        public SwaggerModelPropertyDataBuilder<TProperty> UniqueItems(bool uniqueItems)
        {
            Data.UniqueItems = uniqueItems;

            return this;
        }
    }
}