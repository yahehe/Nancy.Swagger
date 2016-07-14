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
        private const string DefintionsRefLocation = "#/definitions/";

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
            Schema s = new Schema();
            IEnumerable<Model> sModels = this.ToModel();
            Model sModel = sModels.FirstOrDefault();

            if (sModel == null) return s;

            if (typeof (IEnumerable).IsAssignableFrom(this.ModelType))
            {
                s.Type = "array";
                s.Items = new Item();
                Type t = ModelType;
                Type subType = t.GetGenericArguments().FirstOrDefault();
                s.Items.Type = "object";
                s.Items.Ref = DefintionsRefLocation + subType?.Name;
                s.Ref = DefintionsRefLocation + subType?.Name + "[]";
            }
            else
            {
                s.Type = "object";
                s.Ref = DefintionsRefLocation + this.ModelType.Name;
                s.Required = (sModel.Required as IList<string>)?.Select(x => x.ToCamelCase()).ToList();
                s.Description = sModel.Description;
                s.Properties = new Dictionary<string, Schema>();
                foreach (var member in sModel.Properties)
                {
                    s.Properties.Add(member.Key.ToCamelCase(), GenerateSchemaForProperty(member.Value));
                }
            }
            return s;
        }

        private Schema GenerateSchemaForProperty(ModelProperty property)
        {
            Schema schema = new Schema();
            schema.Type = property.Type.ToCamelCase();
            if (schema.Type.Equals("array"))
            {
                schema.Items = new Item();
                if (!string.IsNullOrEmpty(property.Items.Type))
                {
                    schema.Items.Type = property.Items.Type;
                }
                else
                {
                    schema.Items.Type = "object";
                    schema.Items.Ref = DefintionsRefLocation + property.Items.Ref;
                }

            }
            return schema;
        }
    }
}