using Swagger.Model.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Attributes
{
    public class FormParamAttribute : SwaggerRouteParameterAttribute
    {
        public FormParamAttribute(string name)
            : base(name, ParameterType.Form)
        {
        }
    }
}