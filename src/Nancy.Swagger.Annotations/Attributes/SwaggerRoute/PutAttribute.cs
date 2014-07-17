namespace Nancy.Swagger.Annotations.Attributes
{
    public class Put : SwaggerRouteAttribute
    {
        public Put(string path)
            : base("PUT", path)
        {
        }
    }
}