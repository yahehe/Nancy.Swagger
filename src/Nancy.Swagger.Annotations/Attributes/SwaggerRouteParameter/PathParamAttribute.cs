using Swagger.Model.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Attributes
{
    public class PathParamAttribute : SwaggerRouteParameterAttribute
    {
        public PathParamAttribute(string name)
            : base(name, ParameterType.Path)
        {
            Required = true;
        }
    }
}