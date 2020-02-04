using System.Collections.Generic;

namespace Nancy.Swagger.Annotations.Demo.Models
{
    public class CafeMenu
    {
        public ICollection<MenuItem> Items { get; set; }
    }

    public class MenuItem
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public MenuCategory Category { get; set; }

        public bool IsVegan { get; set; }
        
    }

    public enum MenuCategory
    {
        All,
        Food,
        Beverage,
        Dessert
    }
}
