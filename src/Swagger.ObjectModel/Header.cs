namespace Swagger.ObjectModel
{
    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The header.
    /// </summary>
    public class Header : DataType
    {
        /// <summary>
        /// A short description of the header.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }
    }
}