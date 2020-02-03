using Nancy.Swagger.Annotations.Attributes;

namespace Nancy.Swagger.Annotations.Demo.Models
{
    [Model("Querystring for cafe menu")]
    public class CafeMenuQuery
    {
        [ModelProperty(Description = "for paging", Required = false, Minimum = 0, Maximum = 20)]
        public int Page { get; set; }

        [ModelProperty(Description = "for paging", Required = false, Minimum = 0, Maximum = 20)]
        public int PageCount { get; set; }

        [ModelProperty(Description = "field to sort, available field: name, price, category", Required = false)]
        public string Sort { get; set; }

        [ModelProperty(Description = "sorting direction, asc or desc", Required = false)]
        public string SortDirection { get; set; }

        [ModelProperty(Description = "query for item that has price higher than this", Required = false)]
        public double MinumumPrice { get; set; }

        [ModelProperty(Description = "query for item that has price lower than this", Required = false)]
        public double MaximumPrice { get; set; }

        [ModelProperty(Description = "query for item that is vegan", Required = false)]
        public bool? VeganOnly { get; set; }

        [ModelProperty(Description = "query for item that belongs to the specific category", Required = false)]
        public MenuCategory Category { get; set; }
    }


}
