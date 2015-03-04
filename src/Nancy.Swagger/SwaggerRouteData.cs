using System;
using System.Collections.Generic;
using Swagger.ObjectModel.SwaggerDocument;

namespace Nancy.Swagger
{
    /// <summary>
    /// Holds all the Swagger metadata pertaining to a specific route.
    /// </summary>
    [SwaggerApi]
    public class SwaggerRouteData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerRouteData"/> class.
        /// </summary>
        public SwaggerRouteData()
        {
            OperationParameters = new List<SwaggerParameterData>();
            OperationResponseMessages = new List<ResponseMessage>();
            OperationProduces = new List<string>();
            OperationConsumes = new List<string>();
        }

        public string ResourcePath { get; set; }

        public string ApiPath { get; set; }

        public string OperationNickname { get; set; }

        public string OperationSummary { get; set; }

        public HttpMethod OperationMethod { get; set; }

        public string OperationNotes { get; set; }

        public IList<SwaggerParameterData> OperationParameters { get; set; }

        public IList<ResponseMessage> OperationResponseMessages { get; set; }

        public IList<string> OperationProduces { get; set; }

        public IList<string> OperationConsumes { get; set; }

        public Type OperationModel { get; set; }
    }
}