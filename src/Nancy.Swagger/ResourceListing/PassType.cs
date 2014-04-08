using JetBrains.Annotations;

namespace Nancy.Swagger.ResourceListing
{
    /// <summary>
    /// Used in <see cref="Authorization"/> to denote how an API key should be passed.
    /// </summary>
    [PublicAPI]
    public enum PassType
    {
        Header,

        Query
    }
}