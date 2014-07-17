using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Attributes.SwaggerRouteParameter
{
    public class BodyParamAttribute : SwaggerRouteParameterAttribute
    {
        public BodyParamAttribute()
            : base("body", ParameterType.Body)
        {
        }
    }
}