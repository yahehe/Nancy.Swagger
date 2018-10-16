using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Swagger.ObjectModel;

namespace Nancy.Swagger
{
    public static class SwaggerSchemaFactory
    {
        private const string DefinitionsRefLocation = "#/definitions/";

        public static Schema CreateSchema(this Model sModel, Type t, bool isDefinition)
        {
            if (typeof (IEnumerable).GetTypeInfo().IsAssignableFrom(t))
            {
                return new EnumerableSchema(t, sModel);
            }
            if (t.GetTypeInfo().IsEnum)
            {
                return new EnumSchema(t, sModel, isDefinition);
            }
            if (t.IsNullable() && Nullable.GetUnderlyingType(t).GetTypeInfo().IsEnum)
            {
                return new EnumSchema(Nullable.GetUnderlyingType(t), sModel, isDefinition);
            }
            return new ObjectSchema(t, sModel, isDefinition);
        }

        private class EnumerableSchema : Schema
        {
            public EnumerableSchema(Type t, Model sModel)
            {
                Type = "array";
                Items = new Item();
                Type subType = t.GetTypeInfo().GetGenericArguments().FirstOrDefault();
                if (subType != null && Primitive.IsPrimitive(subType))
                {
                    var primitive = Primitive.FromType(subType);
                    Items.Type = primitive.Type;
                    if (!string.IsNullOrEmpty(primitive.Format))
                    {
                        Items.Format = primitive.Format;
                    }
                }
                else
                {
                    Items.Type = "object";
                    Items.Ref = DefinitionsRefLocation + subType?.Name;
                }
            }
        }

        private class EnumSchema : Schema
        {
            public EnumSchema(Type t, Model sModel, bool isDefinition)
            {
                if (isDefinition)
                {
                    Type = "string";
                    Description = sModel.Description;
                    Enum = t.GetTypeInfo().GetEnumNames();
                }
                else
                {
                    Ref = DefinitionsRefLocation + t.Name;
                }
            }
        }

        private class ObjectSchema : Schema
        {
            public ObjectSchema(Type t, Model sModel, bool isDefinition)
            {
                if (isDefinition)
                {
                    Type = "object";
                    Required = (sModel.Required as IList<string>)?.Select(x => x.ToCamelCase()).ToList();
                    Description = sModel.Description;
                    Properties = new Dictionary<string, Schema>();
                    foreach (var member in sModel.Properties)
                    {
                        Properties.Add(member.Key.ToCamelCase(), GenerateSchemaForProperty(member.Value));
                    }
                }
                else
                {
                    Ref = DefinitionsRefLocation + SwaggerConfig.ModelIdConvention(t);
                }
            }
        }

        private static Schema GenerateSchemaForProperty(ModelProperty property)
        {
            Schema schema = new Schema();
            schema.Type = property.Type?.ToCamelCase();
            schema.Format = property.Format?.ToCamelCase();
            schema.Description = property.Description;

            if (schema.Type == null)
            {
                schema.Ref = DefinitionsRefLocation + property.Ref;
            }
            else if (schema.Type.Equals("array"))
            {
                schema.Items = new Item();
                if (!string.IsNullOrEmpty(property.Items.Type))
                {
                    schema.Items.Type = property.Items.Type;
                }
                else
                {
                    schema.Items.Type = "object";
                    schema.Items.Ref = DefinitionsRefLocation + property.Items.Ref;
                }

            }
            return schema;
        }
    }
}
