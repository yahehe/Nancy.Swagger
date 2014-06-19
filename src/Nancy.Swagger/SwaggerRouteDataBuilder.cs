using System;

using Nancy.Swagger.ApiDeclaration;

namespace Nancy.Swagger
{
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
            Data = new SwaggerRouteData
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
            Data.ResourcePath = resourcePath;

            return this;
        }

        /// <summary>
        /// Specify the summary for the operation.
        /// </summary>
        /// <param name="summary">The operation summary.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder Summary(string summary)
        {
            Data.OperationSummary = summary;

            return this;
        }

        /// <summary>
        /// Specify the notes for the operation.
        /// </summary>
        /// <param name="notes">The operation notes.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder Notes(string notes)
        {
            Data.OperationNotes = notes;

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
            var primitive = Primitive.FromType(typeof(T));

            var param = new Parameter
                {
                    Name = name,
                    Type = primitive.Type,
                    Format = primitive.Format,
                    ParamType = ParameterType.Query,
                    Description = description,
                    Required = required,
                    AllowMultiple = allowMultiple,
                    DefaultValue = defaultValue
                };

            Data.OperationParameters.Add(param);

            return this;
        }
    }
}