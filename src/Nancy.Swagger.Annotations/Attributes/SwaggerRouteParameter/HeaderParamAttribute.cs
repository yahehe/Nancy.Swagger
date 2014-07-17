using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Attributes.SwaggerRouteParameter
{
    public class HeaderParamAttribute : SwaggerRouteParameterAttribute
    {
        public HeaderParamAttribute(string name)
            : base(name, ParameterType.Header)
        {
        }
    }
}