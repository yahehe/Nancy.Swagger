using Swagger.Model.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Attributes
{
    public class HeaderParamAttribute : SwaggerRouteParameterAttribute
    {
        public HeaderParamAttribute(string name)
            : base(name, ParameterType.Header)
        {
        }
    }
}