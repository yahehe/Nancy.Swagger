namespace Nancy.Swagger.Annotations.Attributes
{
    public class Delete : SwaggerRouteAttribute
    {
        public Delete(string path)
            : base("DELETE", path)
        {
        }
    }
}