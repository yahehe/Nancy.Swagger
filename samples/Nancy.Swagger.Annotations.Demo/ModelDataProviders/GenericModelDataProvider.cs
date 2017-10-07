using Nancy.Swagger.Demo.Models;
using Nancy.Swagger.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nancy.Swagger.Demo.ModelDataProviders
{
    public class GenericModelDataProvider : ISwaggerModelDataProvider
    {
        public SwaggerModelData GetModelData()
        {
            return SwaggerModelData.ForType<ApiResponse<string>>(with =>
            {
                with.Description("An API response containing a string");
            });
        }
    }

    public class GenericModelDataProvider2 : ISwaggerModelDataProvider
    {
        public SwaggerModelData GetModelData()
        {
            return SwaggerModelData.ForType<ApiResponse<ApiResponse<string>>>(with =>
            {
                with.Description("An oddly double nested API response object");
            });
        }
    }
}
