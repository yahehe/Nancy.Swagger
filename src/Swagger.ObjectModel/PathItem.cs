namespace Swagger.ObjectModel
{
    using System.Collections.Generic;

    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The path item.
    /// </summary>
    public class PathItem : SwaggerModel
    {
        /// <summary>
        /// Gets or sets the operations.
        /// </summary>
        [SwaggerProperty("operations")]
        public IDictionary<HttpMethod, Operation> Operations { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        [SwaggerProperty("parameters")]
        public IEnumerable<Parameter> Parameters { get; set; }
    }
}