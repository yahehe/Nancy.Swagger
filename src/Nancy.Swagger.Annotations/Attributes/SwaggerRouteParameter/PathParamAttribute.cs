using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Attributes.SwaggerRouteParameter
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