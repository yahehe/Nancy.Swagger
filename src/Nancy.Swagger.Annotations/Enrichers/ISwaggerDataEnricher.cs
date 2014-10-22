using System.Reflection;

namespace Nancy.Swagger.Annotations.Enrichers
{
    public interface ISwaggerDataEnricher
    {
        void Enrich(SwaggerRouteData data);

        void Enrich(SwaggerModelData data);

        void Enrich(SwaggerModelPropertyData data, PropertyInfo propertyInfo);

        void Enrich(SwaggerParameterData parameterData, ParameterInfo parameterInfo);
    }
}