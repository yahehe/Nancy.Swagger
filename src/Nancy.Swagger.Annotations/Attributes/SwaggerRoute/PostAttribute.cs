namespace Nancy.Swagger.Annotations.Attributes.SwaggerRoute
{
    public class PostAttribute : SwaggerRouteAttribute
    {
        public PostAttribute(string path)
            : base("POST", path)
        {
        }
    }
}