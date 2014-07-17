namespace Nancy.Swagger.Annotations.Attributes.SwaggerRoute
{
    public class PutAttribute : SwaggerRouteAttribute
    {
        public PutAttribute(string path)
            : base("PUT", path)
        {
        }
    }
}