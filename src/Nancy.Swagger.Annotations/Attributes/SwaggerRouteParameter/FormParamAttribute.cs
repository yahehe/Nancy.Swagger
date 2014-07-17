using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Attributes.SwaggerRouteParameter
{
    public class FormParamAttribute : SwaggerRouteParameterAttribute
    {
        public FormParamAttribute(string name)
            : base(name, ParameterType.Form)
        {
        }
    }
}