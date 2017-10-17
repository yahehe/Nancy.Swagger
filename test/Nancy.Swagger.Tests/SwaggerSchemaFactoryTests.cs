using System.Collections.Generic;
using System.Reflection;
using Should;
using Swagger.ObjectModel;
using Xunit;

namespace Nancy.Swagger.Tests
{
    public class SwaggerSchemaFactoryTests
    {
        private const string DefinitionsRefLocation = "#/definitions/";

        [Fact]
        public void EnumerableType_PrimitiveSubtype_ShouldCreateEnumerableSchema()
        {
            var model = new Model();

            var schema = model.CreateSchema(typeof(IEnumerable<int>), false);
            var item = schema.Items;
            Assert.Equal("array", schema.Type);
            Assert.Equal("int", item.Type);
        }

        [Fact]
        public void EnumerableType_NoSubtype_ShouldCreateEnumerableSchema()
        {
            var model = new Model();

            var schema = model.CreateSchema(typeof(IEnumerable<>), false);
            var item = schema.Items;
            Assert.Equal("array", schema.Type);
            Assert.Null(item.Type);
        }

        [Fact]
        public void EnumerableType_ReferenceSubtype_ShouldCreateEnumerableSchema()
        {
            var model = new Model();

            var schema = model.CreateSchema(typeof(IEnumerable<Schema>), false);
            var item = schema.Items;
            Assert.Equal("array", schema.Type);
            Assert.Equal("object", item.Type);
            Assert.Equal($"{DefinitionsRefLocation}Schema", item.Ref);
        }

        [Fact]
        public void EnumType_WithDefinition_ShouldCreateEnumSchema()
        {            
            var description = "Sample description";
            var model = new Model { Description = description };
            var enumType = typeof(HttpStatusCode);
            var enumNames = enumType.GetTypeInfo().GetEnumNames();

            var schema = model.CreateSchema(enumType, true);
            Assert.Equal("string", schema.Type);
            Assert.Equal(description, schema.Description);
            Assert.Equal(enumNames, schema.Enum);
            Assert.Null(schema.Ref);
        }

        [Fact]
        public void EnumType_WithoutDefinition_ShouldCreateEnumSchema()
        {            
            var model = new Model();
            var enumType = typeof(HttpStatusCode);

            var schema = model.CreateSchema(enumType, false);
            Assert.Equal($"{DefinitionsRefLocation}HttpStatusCode", schema.Ref);
        }

        [Fact]
        public void NullableEnumType_WithDefinition_ShouldCreateEnumSchema()
        {            
            var description = "Sample description";
            var model = new Model { Description = description };
            var enumType = typeof(HttpStatusCode);
            var enumNames = enumType.GetTypeInfo().GetEnumNames();

            var schema = model.CreateSchema(enumType, true);
            Assert.Equal("string", schema.Type);
            Assert.Equal(description, schema.Description);
            Assert.Equal(enumNames, schema.Enum);
            Assert.Null(schema.Ref);
        }

        [Fact]
        public void NullableEnumType_WithoutDefinition_ShouldCreateEnumSchema()
        {            
            var model = new Model();
            var enumType = typeof(HttpStatusCode);

            var schema = model.CreateSchema(enumType, false);
            Assert.Equal($"{DefinitionsRefLocation}HttpStatusCode", schema.Ref);
        }

        [Fact]
        public void ObjectType_WithDefinition_ShouldCreateObjectSchema()
        {            
            var description = "Sample description";
            var model = new Model { Description = description };

            var schema = model.CreateSchema(typeof(Schema), true);
            Assert.Equal("object", schema.Type);
            Assert.Equal(description, schema.Description);
            Assert.Empty(schema.Required);
            Assert.Empty(schema.Properties);
            Assert.Null(schema.Ref);
        }

        [Fact]
        public void ObjectType_WithoutDefinition_ShouldCreateObjectSchema()
        {            
            var model = new Model();

            var schema = model.CreateSchema(typeof(Schema), false);
            Assert.Equal($"{DefinitionsRefLocation}Schema", schema.Ref);
        }
    }
}