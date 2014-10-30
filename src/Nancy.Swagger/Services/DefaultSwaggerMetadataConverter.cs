using System;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;
using Swagger.ObjectModel.ApiDeclaration;
using Swagger.ObjectModel.ResourceListing;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public class DefaultSwaggerMetadataConverter : ISwaggerMetadataConverter
    {
        private ISwaggerMetadataProvider _metadataProvider;

        public DefaultSwaggerMetadataConverter(ISwaggerMetadataProvider metadataProvider)
        {
            _metadataProvider = metadataProvider;
        }

        public ResourceListing GetResourceListing()
        {
            return new ResourceListing
            {
                Apis = _metadataProvider.RetrieveSwaggerRouteData()
                    .Select(d => d.ResourcePath)
                    .Distinct()
                    .Select(path => new Resource { Path = path })
                    .OrderBy(resource => resource.Path)
            };
        }

        public ApiDeclaration GetApiDeclaration(string resourcePath)
        {
            var routeData = _metadataProvider.RetrieveSwaggerRouteData()
                .Where(d => d.ResourcePath == resourcePath)
                .ToList();

            var apiDeclaration = new ApiDeclaration
            {
                BasePath = new Uri("/", UriKind.Relative),
                Apis = routeData.GroupBy(d => d.ApiPath).Select(GetApi).OrderBy(api => api.Path)
            };

            var modelsData = _metadataProvider.RetrieveSwaggerModelData();
            var modelsForRoutes = GetModelsForRoutes(routeData, modelsData);

            apiDeclaration.Models = modelsForRoutes.SelectMany(m => m.ToModel(modelsData))
                .GroupBy(m => m.Id)
                .Select(g => g.First())
                .OrderBy(m => m.Id)                
                .ToDictionary(m => m.Id, m => m);

            return apiDeclaration;
        }

        protected IEnumerable<SwaggerModelData> GetModelsForRoutes(
            IList<SwaggerRouteData> routeData,
            IList<SwaggerModelData> modelData)
        {
            return routeData.GetDistinctModelTypes().Select(type => EnsureModelData(type, modelData));
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
    }
}