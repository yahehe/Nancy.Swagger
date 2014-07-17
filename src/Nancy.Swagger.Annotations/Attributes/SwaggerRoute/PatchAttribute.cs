namespace Nancy.Swagger.Annotations.Attributes.SwaggerRoute
{
    public class PatchAttribute : SwaggerRouteAttribute
    {
        public PatchAttribute(string path)
            : base("PATCH", path)
        {
        }
    }
}