using System;
using Nancy.Responses.Negotiation;
using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger
{
    /// <summary>
    /// Helper class for configuring an instance of <see cref="SwaggerRouteData"/>.
    /// </summary>
    [SwaggerApi]
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
        public SwaggerRouteData Data { get; private set; }

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
        /// Specify the path for the API.
        /// </summary>
        /// <param name="apiPath">The resource path.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder ApiPath(string apiPath)
        {
            Data.ApiPath = apiPath;

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
            T defaultValue = default(T))
        {
            return Param(ParameterType.Query, name, description, required, defaultValue);
        }

        /// <summary>
        /// Specify a body parameter for the operation.
        /// </summary>
        /// <param name="description">The description of the parameter.</param>
        /// <param name="required">A <see cref="Boolean"/> value indicating whether the parameter is required. The default is <c>false</c>.</param>
        /// <param name="defaultValue">The default value to be used for the field.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder BodyParam<T>(
            string description = null,
            bool required = false,
            T defaultValue = default(T))
        {
            return Param(ParameterType.Body, "body", description, required, defaultValue);
        }

        /// <summary>
        /// Specify a path parameter for the operation.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="description">The description of the parameter.</param>
        /// <param name="required">A <see cref="Boolean"/> value indicating whether the parameter is required. The default is <c>false</c>.</param>
        /// <param name="allowMultiple">A <see cref="Boolean"/> value indicating whether the parameter is allowed to appear more than once. The default is <c>false</c>.</param>
        /// <param name="defaultValue">The default value to be used for the field.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder PathParam<T>(
            string name,
            string description = null,
            bool required = false,
            T defaultValue = default(T))
        {
            return Param(ParameterType.Path, name, description, required, defaultValue);
        }

        /// <summary>
        /// Specify a parameter for the operation.
        /// </summary>
        /// <param name="paramType">The <see cref="ParameterType"/> of the parameter.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="description">The description of the parameter.</param>
        /// <param name="required">A <see cref="Boolean"/> value indicating whether the parameter is required. The default is <c>false</c>.</param>
        /// <param name="allowMultiple">A <see cref="Boolean"/> value indicating whether the parameter is allowed to appear more than once. The default is <c>false</c>.</param>
        /// <param name="defaultValue">The default value to be used for the field.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder Param<T>(
            ParameterType paramType,
            string name,
            string description = null,
            bool required = false,
            T defaultValue = default(T))
        {
            var param = new SwaggerParameterData
                {
                    Name = name,
                    ParamType = paramType,
                    Description = description,
                    Required = required,
                    DefaultValue = defaultValue,
                    ParameterModel = typeof(T)
                };

            Data.OperationParameters.Add(param);

            return this;
        }

        /// <summary>
        /// Specify a response message for the operation.
        /// </summary>
        /// <param name="code">The HTTP code of the response.</param>
        /// <param name="message">The message for the response.</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder Response(int code, string message)
        {
            var responseMessage = new ResponseMessage { Code = code, Message = message };
            // TODO: Populate responseModel

            Data.OperationResponseMessages.Add(responseMessage);

            return this;
        }

        /// <summary>
        /// Specify what media type(s) the operation produces.
        /// </summary>
        /// <param name="mediaTypes">The media type(s).</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder Produces(params MediaType[] mediaTypes)
        {
            foreach (var mediaType in mediaTypes)
            {
                Data.OperationProduces.Add(mediaType);
            }

            return this;
        }

        /// <summary>
        /// Specify what media type(s) the operation consumes.
        /// </summary>
        /// <param name="mediaTypes">The media type(s).</param>
        /// <returns>The <see cref="SwaggerRouteDataBuilder"/> instance.</returns>
        public SwaggerRouteDataBuilder Consumes(params MediaType[] mediaTypes)
        {
            foreach (var mediaType in mediaTypes)
            {
                Data.OperationConsumes.Add(mediaType);
            }

            return this;
        }

        public SwaggerRouteDataBuilder Model<T>()
        {
            Data.OperationModel = typeof(T);

            return this;
        }
    }
}