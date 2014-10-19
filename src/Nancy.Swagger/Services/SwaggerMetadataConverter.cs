using System;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;
using Swagger.ObjectModel.ApiDeclaration;
using Swagger.ObjectModel.ResourceListing;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public abstract class SwaggerMetadataConverter : ISwaggerMetadataConverter
    {
        public ResourceListing GetResourceListing()
        {
            return new ResourceListing
            {
                Apis = RetrieveSwaggerRouteData()
                    .Select(d => d.ResourcePath)
                    .Distinct()
                    .Select(path => new Resource { Path = path })
                    .OrderBy(resource => resource.Path)
            };
        }

        public ApiDeclaration GetApiDeclaration(string resourcePath)
        {
            var routeData = RetrieveSwaggerRouteData()
                .Where(d => d.ResourcePath == resourcePath)
                .ToList();

            var apiDeclaration = new ApiDeclaration
            {
                BasePath = new Uri("/", UriKind.Relative),
                Apis = routeData.GroupBy(d => d.ApiPath).Select(GetApi).OrderBy(api => api.Path)
            };

            var modelsData = RetrieveSwaggerModelData();
            var modelsForRoutes = GetModelsForRoutes(routeData, modelsData);

            apiDeclaration.Models = modelsForRoutes.SelectMany(m => m.ToModel(modelsData))
                .GroupBy(m => m.Id)
                .Select(g => g.First())
                .OrderBy(m => m.Id)                
                .ToDictionary(m => m.Id, m => m);

            return apiDeclaration;
        }

        protected abstract IList<SwaggerRouteData> RetrieveSwaggerRouteData();

        protected abstract IList<SwaggerModelData> RetrieveSwaggerModelData();

        protected IEnumerable<SwaggerModelData> GetModelsForRoutes(
            IList<SwaggerRouteData> routeData,
            IList<SwaggerModelData> modelData)
        {
            return GetDistinctModelTypes(routeData).Select(type => EnsureModelData(type, modelData));
        }

        protected IEnumerable<Type> GetDistinctModelTypes(IList<SwaggerRouteData> routeData)
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
		
        private static Api GetApi(IGrouping<string, SwaggerRouteData> @group)
        {
            return new Api
            {
                Path = @group.Key,
                Operations = @group
                    .Select(d => d.ToOperation())
                    .OrderBy(o => o.Method)
            };
        }

        private static IEnumerable<Type> GetOperationModels(IEnumerable<SwaggerRouteData> metadata)
        {
            return metadata
                .Where(d => d.OperationModel != null)
                .Select(d => d.OperationModel);
        }

        private static IEnumerable<Type> GetParameterModels(IEnumerable<SwaggerRouteData> metadata)
        {
            return metadata
                .SelectMany(d => d.OperationParameters)
                .Where(p => p.ParameterModel != null)
                .Select(p => p.ParameterModel);
        }
    }
}