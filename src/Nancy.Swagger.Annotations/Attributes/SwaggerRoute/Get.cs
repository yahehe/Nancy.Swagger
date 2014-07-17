namespace Nancy.Swagger.Annotations.Attributes
{
    public class Get : SwaggerRouteAttribute
    {
        public Get(string path)
            : base("GET", path)
        {
        }
    }
}