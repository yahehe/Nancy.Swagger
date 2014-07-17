namespace Nancy.Swagger.Annotations.Attributes
{
    public class Options : SwaggerRouteAttribute
    {
        public Options(string path)
            : base("OPTIONS", path)
        {
        }
    }
}