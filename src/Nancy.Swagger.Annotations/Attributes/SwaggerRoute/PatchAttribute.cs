namespace Nancy.Swagger.Annotations.Attributes
{
    public class Patch : SwaggerRouteAttribute
    {
        public Patch(string path)
            : base("PATCH", path)
        {
        }
    }
}