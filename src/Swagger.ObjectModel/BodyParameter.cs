namespace Swagger.ObjectModel
{
    using global::Swagger.ObjectModel.Attributes;

    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The body parameter.
    /// </summary>
    public class BodyParameter : Parameter
    {
        /// <summary>
        /// The type of the parameter (that is, the location of the parameter in the request).
        /// </summary>
        [SwaggerProperty("in", true)]
        public override ParameterIn In
        {
            get
            {
                return ParameterIn.Body;
            }
        }

        /// <summary>
        /// The schema defining the type used for the body parameter.
        /// </summary>
        [SwaggerProperty("schema", true)]
        public object Schema { get; set; }
    }
}