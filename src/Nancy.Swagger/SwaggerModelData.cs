using System;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Swagger
{
    [SwaggerApi]
    public class SwaggerModelData
    {
        public SwaggerModelData(Type type)
        {
            ModelType = type;
            Properties = GetPropertiesFromType(type).ToList();
        }

        public string Description { get; set; }

        public Type ModelType { get; set; }

        public IList<SwaggerModelPropertyData> Properties { get; set; }

        private IEnumerable<SwaggerModelPropertyData> GetPropertiesFromType(Type type)
        {
            return type.GetProperties()
                .Select(property => new SwaggerModelPropertyData
                {
                    Name = property.Name,
                    Type = property.PropertyType
                });
        }
    }
}