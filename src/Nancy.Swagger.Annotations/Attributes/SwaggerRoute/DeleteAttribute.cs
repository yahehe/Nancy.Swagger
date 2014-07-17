namespace Nancy.Swagger.Annotations.Attributes.SwaggerRoute
{
    public class DeleteAttribute : SwaggerRouteAttribute
    {
        public DeleteAttribute(string path)
            : base("DELETE", path)
        {
        }
    }
}