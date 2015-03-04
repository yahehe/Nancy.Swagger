namespace Swagger.ObjectModel
{
    using System.Collections.Generic;

    using global::Swagger.ObjectModel;
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The path item.
    /// </summary>
    public class PathItem : SwaggerModel
    {
        /// <summary>
        /// Gets or sets the ref.
        /// </summary>
        [SwaggerProperty("$ref")]
        public string Ref { get; set; }

        /// <summary>
        /// Gets or sets the operations.
        /// </summary>
        [SwaggerProperty("operations")]
        private IEnumerable<Operation> Operations { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        [SwaggerProperty("parameters")]
        private IEnumerable<Parameter> Parameters { get; set; }
    }
}