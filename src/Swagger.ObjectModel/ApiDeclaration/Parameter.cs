using Swagger.ObjectModel.Attributes;

namespace Swagger.ObjectModel.ApiDeclaration
{
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
        /// The type of the parameter (that is, the location of the parameter in the request).
        /// </summary>
        [SwaggerProperty("paramType", true)]
        public ParameterType ParamType { get; set; }

        /// <summary>
        /// The unique name for the parameter. 
        /// Each name MUST be unique, even if they are associated with different <see cref="ParamType"/> values. 
        /// </summary>
        /// <remarks>
        /// Parameter names are case sensitive.
        /// </remarks>
        [SwaggerProperty("name", true)]
        public string Name { get; set; }

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
        /// The field MUST be included if <see cref="ParamType"/> is <see cref="ParameterType.Path"/> and MUST have the value true.
        /// </remarks>
        [SwaggerProperty("required")]
        public bool? Required { get; set; }

        /// <summary>
        /// Another way to allow multiple values for a <see cref="ParameterType.Query"/> parameter. 
        /// If used, the query parameter may accept comma-separated values. 
        /// </summary>
        /// <remarks>
        /// The field may be used only if <see cref="ParamType"/> is <see cref="ParameterType.Query"/>, <see cref="ParameterType.Header"/> or <see cref="ParameterType.Path"/>.
        /// </remarks>
        [SwaggerProperty("allowMultiple")]
        public bool? AllowMultiple { get; set; }
    }
}