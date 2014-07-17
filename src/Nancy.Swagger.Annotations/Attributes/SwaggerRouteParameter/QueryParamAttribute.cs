using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Attributes.SwaggerRouteParameter
{
    public class QueryParamAttribute : SwaggerRouteParameterAttribute
    {
        public QueryParamAttribute(string name)
            : base(name, ParameterType.Query)
        {
        }
    }
}