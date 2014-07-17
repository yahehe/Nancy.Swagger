namespace Nancy.Swagger.Annotations.Attributes.SwaggerRoute
{
    public class GetAttribute : SwaggerRouteAttribute
    {
        public GetAttribute(string path)
            : base("GET", path)
        {
        }
    }
}