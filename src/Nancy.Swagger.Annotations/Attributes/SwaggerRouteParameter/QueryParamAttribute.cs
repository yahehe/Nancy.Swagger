using Swagger.Model.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Attributes
{
    public class QueryParamAttribute : SwaggerRouteParameterAttribute
    {
        public QueryParamAttribute(string name)
            : base(name, ParameterType.Query)
        {
        }
    }
}