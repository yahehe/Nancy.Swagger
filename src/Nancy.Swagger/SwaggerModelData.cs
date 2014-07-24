using System;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Swagger
{
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

        /// <summary>
        /// Returns an instance of <see cref="SwaggerModelData"/> representing this type.
        /// </summary>
        /// <param name="action">An <see cref="Action{SwaggerModelDataBuilder}"/> for building the <see cref="SwaggerModelData"/>.</param>
        /// <returns>An instance of <see cref="SwaggerModelData"/> constructed using the type and by invoking <paramref name="action"/>.</returns>
        public static SwaggerModelData ForType<T>(Action<SwaggerModelDataBuilder<T>> action)
        {
            var builder = new SwaggerModelDataBuilder<T>();

            action.Invoke(builder);

            return builder.Data;
        }

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