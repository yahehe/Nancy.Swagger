using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;

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

        public Schema GetSchema()
        {
            var sModel = this.ToModel(null, false).FirstOrDefault();

            if (sModel == null) return new Schema();
            
            return sModel.CreateSchema(ModelType);
        }
    }
}