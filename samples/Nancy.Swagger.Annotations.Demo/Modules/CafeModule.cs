using System;
using System.Collections.Generic;
using Nancy.Swagger.Annotations.Attributes;
using Nancy.Swagger.Annotations.Demo.Models;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations.Demo.Modules
{
    public class CafeModule : NancyModule
    {
        public CafeModule(ISwaggerModelCatalog modelCatalog) : base("/cafe")
        {
            modelCatalog.AddModel<CafeMenu>();
            modelCatalog.AddModel<MenuItem>();

            Get(
                "/menu", 
                parameter =>
                {
                    var cafeMenuQuery = new CafeMenuQuery
                    {
                        Page = Request.Query.page ?? 1,
                        PageCount = Request.Query.pagecount ?? 10,
                        Sort = Request.Query.sort ?? null,
                        SortDirection = Request.Query.sortdirection ?? null,
                        MinumumPrice = Request.Query.minimumprice ?? Double.MinValue,
                        MaximumPrice = Request.Query.maximumprice ?? Double.MaxValue,
                        VeganOnly = Request.Query.veganonly ?? null,
                        Category = string.IsNullOrEmpty(Request.Query.category) ? MenuCategory.All  : Enum.Parse(typeof(MenuCategory), Request.Query.category),
                    };

                    return GetMenu(cafeMenuQuery);
                },
                null,
                "GetMenu");
        }

        [Route("GetMenu")]
        [Route(HttpMethod.Get, "/menu")]
        [Route(Summary = "Get Cafe Menu")]
        [SwaggerResponse(HttpStatusCode.OK, Message = "OK")]
        [Route(Tags = new[] { "Cafe Module" })]
        private CafeMenu GetMenu([RouteParam(ParameterIn.Query, Name = "GetMenu")]CafeMenuQuery cafeMenuQuery)
        {
            return new CafeMenu
            {
                Items = new List<MenuItem>
                {
                    new MenuItem
                    {
                        Name = "Pesto Pasta",
                        Price = 9.99,
                        Category = MenuCategory.Food,
                        IsVegan = false,
                    },
                    new MenuItem
                    {
                        Name = "Avocado Toast",
                        Price = 8.99,
                        Category = MenuCategory.Food,
                        IsVegan = true,
                    },
                    new MenuItem
                    {
                        Name = "Tiramisu",
                        Price = 5.99,
                        Category = MenuCategory.Dessert,
                        IsVegan = false,
                    },
                    new MenuItem
                    {
                        Name = "Flat white",
                        Price = 3.99,
                        Category = MenuCategory.Beverage,
                        IsVegan = false,
                    },
                }
            };
        }
    }
}
