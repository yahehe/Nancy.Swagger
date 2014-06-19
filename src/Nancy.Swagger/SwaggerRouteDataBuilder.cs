namespace Nancy.Swagger
{
    using System;

    using Nancy.Swagger.ApiDeclaration;

    /// <summary>
    /// Helper class for configuring an instance of <see cref="SwaggerRouteData"/>.
    /// </summary>
    public class SwaggerRouteDataBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerRouteDataBuilder"/> class.
        /// </summary>
        /// <param name="operationNickName">The nickname for the operation.</param>
        /// <param name="method">The method for the route.</param>
        /// <param name="apiPath">The path for the API.</param>
        public SwaggerRouteDataBuilder(string operationNickName, HttpMethod method, string apiPath)
        {
            this.Data = new SwaggerRouteData
                {
                    OperationNickname = operationNickName,
                    OperationMethod = method,
                    ApiPath = apiPath
                };
        }

        /// <summary>
        /// Gets the <see cref="SwaggerRouteData"/> instance.
        /// </summary>
        public SwaggerRouteData Data { get; set; }

        /// <summary>
        /// Specify the path for the resource.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder ResourcePath(string resourcePath)
        {
            this.Data.ResourcePath = resourcePath;

            return this;
        }

        /// <summary>
        /// Specify the summary for the operation.
        /// </summary>
        /// <param name="summary">The operation summary.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder Summary(string summary)
        {
            this.Data.OperationSummary = summary;

            return this;
        }

        /// <summary>
        /// Specify the notes for the operation.
        /// </summary>
        /// <param name="notes">The operation notes.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder Notes(string notes)
        {
            this.Data.OperationNotes = notes;

            return this;
        }

        /// <summary>
        /// Specify a query parameter for the operation.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="description">The description of the parameter.</param>
        /// <param name="required">A <see cref="Boolean"/> value indicating whether the parameter is required. The default is <c>false</c>.</param>
        /// <param name="allowMultiple">A <see cref="Boolean"/> value indicating whether the parameter is allowed to appear more than once. The default is <c>false</c>.</param>
        /// <param name="defaultValue">The default value to be used for the field.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder QueryParam<T>(
            string name,
            string description = null,
            bool required = false,
            bool allowMultiple = false,
            T defaultValue = default(T))
        {
            var param = new Parameter
                {
                    Name = name,
                    Type = ToSwaggerType(typeof(T)),
                    ParamType = ParameterType.Query,
                    Description = description,
                    Required = required,
                    AllowMultiple = allowMultiple,
                    DefaultValue = defaultValue
                };

            this.Data.OperationParameters.Add(param);

            return this;
        }

        private static string ToSwaggerType(Type type)
        {
            if (type == typeof(int) || type == typeof(long))
            {
                return "integer";
            }

            if (type == typeof(float) || type == typeof(double))
            {
                return "number";
            }

            if (type == typeof(bool))
            {
                return "boolean";
            }

            if (type == typeof(string) || type == typeof(byte) || type == typeof(DateTime))
            {
                return "string";
            }

            throw new NotSupportedException("The specified type could not be converted to a Swagger primitive.");
        }
    }
}