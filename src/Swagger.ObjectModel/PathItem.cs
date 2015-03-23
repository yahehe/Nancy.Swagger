using System.Linq;

namespace Swagger.ObjectModel
{
    using System.Collections.Generic;

    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The path item.
    /// </summary>
    public class PathItem : SwaggerModel
    {
        public PathItem()
        {
            Parameters = Enumerable.Empty<Parameter>();
        }

        /// <summary>
        /// Gets or sets the GET operation.
        /// </summary>
        [SwaggerProperty("get")]
        public Operation Get { get; set; }

        /// <summary>
        /// Gets or sets the PUT operation.
        /// </summary>
        [SwaggerProperty("put")]
        public Operation Put { get; set; }

        /// <summary>
        /// Gets or sets the POST operation.
        /// </summary>
        [SwaggerProperty("post")]
        public Operation Post { get; set; }

        /// <summary>
        /// Gets or sets the DELETE operation.
        /// </summary>
        [SwaggerProperty("delete")]
        public Operation Delete { get; set; }

        /// <summary>
        /// Gets or sets the OPTIONS operation.
        /// </summary>
        [SwaggerProperty("options")]
        public Operation Options { get; set; }

        /// <summary>
        /// Gets or sets the HEAD operation.
        /// </summary>
        [SwaggerProperty("head")]
        public Operation Head { get; set; }

        /// <summary>
        /// Gets or sets the HEAD operation.
        /// </summary>
        [SwaggerProperty("patch")]
        public Operation Patch { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        [SwaggerProperty("parameters")]
        public IEnumerable<Parameter> Parameters { get; set; }


        public PathItem Combine(PathItem other)
        {

            return new PathItem()
                   {
                       Delete = Delete ?? other.Delete,
                       Get = Get ?? other.Get,
                       Head = Head ?? other.Head,
                       Options = Options ?? other.Options,
                       Patch = Patch ?? other.Patch,
                       Post = Post ?? other.Post,
                       Put = Put ?? other.Put,
                       Ref = Ref ?? other.Ref,
                       Parameters = Parameters.Concat(other.Parameters).ToList()
                   };
        }
    }
}