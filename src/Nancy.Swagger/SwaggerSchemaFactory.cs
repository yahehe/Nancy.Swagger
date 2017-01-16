using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swagger.ObjectModel;

namespace Nancy.Swagger
{
    public static class SwaggerSchemaFactory
    {
        private const string DefintionsRefLocation = "#/definitions/";

        public static Schema CreateSchema(this Model sModel, Type t)
        {
            if (typeof (IEnumerable).IsAssignableFrom(t))
            {
                return new EnumerableSchema(t, sModel);
            }
            if (t.IsEnum)
            {
                return new EnumSchema(t, sModel);
            }
            return new ObjectSchema(t, sModel);
        }

        private class EnumerableSchema : Schema
        {
            public EnumerableSchema(Type t, Model sModel)
            {
                Type = "array";
                Items = new Item();
                Type subType = t.GetGenericArguments().FirstOrDefault();
                Items.Type = "object";
                Items.Ref = DefintionsRefLocation + subType?.Name;
                Ref = DefintionsRefLocation + subType?.Name + "[]";
            }
        }

        private class EnumSchema : Schema
        {
            public EnumSchema(Type t, Model sModel)
            {
                Type = "string";
                Ref = DefintionsRefLocation + t.Name;
                Description = sModel.Description;
                Enum = t.GetEnumNames();
            }
        }

        private class ObjectSchema : Schema
        {
            public ObjectSchema(Type t, Model sModel)
            {
                Type = "object";
                Ref = DefintionsRefLocation + t.Name;
                Required = (sModel.Required as IList<string>)?.Select(x => x.ToCamelCase()).ToList();
                Description = sModel.Description;
                Properties = new Dictionary<string, Schema>();
                foreach (var member in sModel.Properties)
                {
                    Properties.Add(member.Key.ToCamelCase(), GenerateSchemaForProperty(member.Value));
                }
            }
        }

        private static Schema GenerateSchemaForProperty(ModelProperty property)
        {
            Schema schema = new Schema();
            schema.Type = property.Type?.ToCamelCase();

            if (schema.Type == null)
            {
                schema.Ref = DefintionsRefLocation + property.Ref;
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
                    schema.Items.Ref = DefintionsRefLocation + property.Items.Ref;
                }

            }
            return schema;
        }
    }
}
