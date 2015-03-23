namespace Swagger.ObjectModel
{
    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The body parameter.
    /// </summary>
    public class BodyParameter : Parameter
    {
        public BodyParameter()
        {
            Required = true;
        }

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
        public Schema Schema { get; set; }
    }
}