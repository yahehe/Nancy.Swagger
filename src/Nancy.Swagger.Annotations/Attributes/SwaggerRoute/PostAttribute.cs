namespace Nancy.Swagger.Annotations.Attributes
{
    public class Post : SwaggerRouteAttribute
    {
        public Post(string path)
            : base("POST", path)
        {
        }
    }
}