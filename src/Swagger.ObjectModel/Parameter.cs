

namespace Swagger.ObjectModel
{
    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The Parameter Object describes a single parameter to be sent in an operation and maps to the parameters field in the Operation Object.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   name: "body",
    ///   description: "Pet object that needs to be updated in the store",
    ///   required: true,
    ///   type: "Pet",
    ///   paramType: "body"
    /// }
    /// </code>
    /// </example>
    public class Parameter : DataType
    {
        /// <summary>
        /// The unique name for the parameter.
        /// Each name MUST be unique, even if they are associated with different <see cref="In"/> values.
        /// </summary>
        /// <remarks>
        /// Parameter names are case sensitive.
        /// If <see cref="In"/> is "path", the name field MUST correspond to the associated path segment from the path field in the Paths Object. See Path Templating for further information.
        /// For all other cases, the name corresponds to the parameter name used based on the in property.
        /// </remarks>
        [SwaggerProperty("name", true)]
        public string Name { get; set; }

        /// <summary>
        /// The type of the parameter (that is, the location of the parameter in the request).
        /// </summary>
        [SwaggerProperty("in", true)]
        public virtual ParameterIn In { get; set; }

        /// <summary>
        /// A brief description of this parameter.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// A flag to note whether this parameter is required.
        /// If this field is not included, it is equivalent to adding this field with the value false.
        /// </summary>
        /// <remarks>
        /// The field MUST be included if <see cref="In"/> is <see cref="ParameterIn.Path"/> and MUST have the value true.
        /// </remarks>
        [SwaggerProperty("required")]
        public bool? Required { get; set; }
    }
}