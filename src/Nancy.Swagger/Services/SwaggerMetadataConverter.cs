using System;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public abstract class SwaggerMetadataConverter : ISwaggerMetadataConverter
    {
        public SwaggerRoot GetSwaggerJson()
        {
            return new SwaggerRoot { Paths = RetrieveSwaggerRouteData().ToDictionary(x => x.Ref, x => x) };
        }

        protected abstract IList<PathItem> RetrieveSwaggerRouteData();

        protected abstract IList<SwaggerModelData> RetrieveSwaggerModelData();

        protected IEnumerable<SwaggerModelData> GetModelsForRoutes(
            IList<PathItem> routeData,
            IList<SwaggerModelData> modelData)
        {
            return GetDistinctModelTypes(routeData).Select(type => EnsureModelData(type, modelData));
        }

        protected IEnumerable<Type> GetDistinctModelTypes(IList<PathItem> routeData)
        {
            return GetOperationModels(routeData)
                .Union(GetParameterModels(routeData))
                .Select(GetType)
                .Where(type => !Primitive.IsPrimitive(type))
                .Distinct();
        }

        private static Type GetType(Type type)
        {
            if (type.IsContainer())
            {
                return type.GetElementType() ?? type.GetGenericArguments().First();
            }

            return type;
        }

        private SwaggerModelData EnsureModelData(Type type, IList<SwaggerModelData> modelData)
        {
            return modelData.FirstOrDefault(x => x.ModelType == type) ?? new SwaggerModelData(type);
        }

        private static IEnumerable<Type> GetOperationModels(IList<PathItem> metadata)
        {
            return metadata
                .Where(d => d.Operations != null)
                .Select(d => d.Operations);
        }

        private static IEnumerable<Type> GetParameterModels(IList<PathItem> metadata)
        {
            return metadata
                .SelectMany(d => d.OperationParameters)
                .Where(p => p.ParameterModel != null)
                .Select(p => p.ParameterModel);
        }
    }
}