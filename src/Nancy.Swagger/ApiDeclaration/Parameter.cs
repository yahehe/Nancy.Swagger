namespace Nancy.Swagger.ApiDeclaration
{
    using Nancy.Swagger.Attributes;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

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
    [SwaggerDto]
    public class Parameter : DataType
    {
        /// <summary>
        /// The type of the parameter (that is, the location of the parameter in the request).
        /// </summary>
        [Required]
        [JsonProperty("paramType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ParameterType ParamType { get; set; }

        /// <summary>
        /// The unique name for the parameter. 
        /// Each name MUST be unique, even if they are associated with different <see cref="ParamType"/> values. 
        /// </summary>
        /// <remarks>
        /// Parameter names are case sensitive.
        /// </remarks>
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// A brief description of this parameter.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// A flag to note whether this parameter is required. 
        /// If this field is not included, it is equivalent to adding this field with the value false.
        /// </summary>
        /// <remarks>
        /// The field MUST be included if <see cref="ParamType"/> is <see cref="ParameterType.path"/> and MUST have the value true.
        /// </remarks>
        [JsonProperty("required")]
        public bool? Required { get; set; }

        /// <summary>
        /// Another way to allow multiple values for a <see cref="ParameterType.query"/> parameter. 
        /// If used, the query parameter may accept comma-separated values. 
        /// </summary>
        /// <remarks>
        /// The field may be used only if <see cref="ParamType"/> is <see cref="ParameterType.query"/>, <see cref="ParameterType.header"/> or <see cref="ParameterType.path"/>.
        /// </remarks>
        [JsonProperty("allowMultiple")]
        public bool? AllowMultiple { get; set; }
    }
}