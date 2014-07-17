namespace Nancy.Swagger.Annotations.Attributes.SwaggerRoute
{
    public class OptionsAttribute : SwaggerRouteAttribute
    {
        public OptionsAttribute(string path)
            : base("OPTIONS", path)
        {
        }
    }
}