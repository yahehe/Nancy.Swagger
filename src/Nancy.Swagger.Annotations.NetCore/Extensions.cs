
namespace Nancy.Swagger.Annotations
{
    internal static class Extensions
    {
        public static string EnsureForwardSlash(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "/";
            }

            return value.StartsWith("/") ? value : "/" + value;
        }
    }
}