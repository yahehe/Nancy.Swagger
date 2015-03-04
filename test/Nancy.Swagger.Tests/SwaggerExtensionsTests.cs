using Should;
using Swagger.ObjectModel;
using Swagger.ObjectModel.SwaggerDocument;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace Nancy.Swagger.Tests
{
    public class SwaggerExtensionsTests
    {
        [Fact]
        public void ToModelProperty_NonPrimitive_ShouldHaveRefSet()
        {
            new SwaggerModelPropertyData
            {
                Type = typeof(TestModel)
            }.ToModelProperty().ShouldEqual(
                new ModelProperty
                {
                    Ref = SwaggerConfig.ModelIdConvention(typeof(TestModel))
                }
            );
        }

        [Fact]
        public void ToModelProperty_Primitive_ShouldHaveTypeSet()
        {
            new SwaggerModelPropertyData
            {
                Type = typeof(string)
            }.ToModelProperty().ShouldEqual(
                new ModelProperty
                {
                    Type = "string"
                }
            );
        }

        [Fact]
        public void ToModelProperty_PrimitiveCollection_ShouldHaveTypeArrayAndItemsTypeSet()
        {
            new SwaggerModelPropertyData
            {
                Type = typeof(string[])
            }.ToModelProperty().ShouldEqual(
                new ModelProperty
                {
                    Type = "array",
                    Items = new Item { Type = "string" }
                }
            );
        }

        [Fact]
        public void ToModelPropertyNonPrimitiveCollection_ShouldHaveTypeArrayAndItemsRefSet()
        {
            new SwaggerModelPropertyData
            {
                Type = typeof(TestModel[])
            }.ToModelProperty().ShouldEqual(
                new ModelProperty
                {
                    Type = "array",
                    Items = new Item { Ref = SwaggerConfig.ModelIdConvention(typeof(TestModel)) }
                },
                "String return type"
            );
        }

        [Fact]
        public void ToOperation_ModelIsNonPrimitive_ShouldHaveTypeSet()
        {
            new SwaggerRouteData
            {
                OperationMethod = HttpMethod.Get,
                OperationModel = typeof(TestModel),
            }.ToOperation().ShouldEqual(
                new Operation
                {
                    Method = HttpMethod.Get,
                    OperationId = "get",
                    Type = SwaggerConfig.ModelIdConvention(typeof(TestModel)),                    Parameters = Enumerable.Empty<Parameter>()
                }
            );
        }

        [Fact]
        public void ToOperation_ModelIsNonPrimitiveCollection_ShouldHaveTypeArrayAndItemsRefSet()
        {
            new SwaggerRouteData
            {
                OperationMethod = HttpMethod.Get,
                OperationModel = typeof(TestModel[]),
            }.ToOperation().ShouldEqual(
                new Operation
                {
                    Method = HttpMethod.Get,
                    OperationId = "get",
                    Type = "array",
                    Items = new Item { Ref = SwaggerConfig.ModelIdConvention(typeof(TestModel)) },
                    Parameters = Enumerable.Empty<Parameter>()
                },
                "String return type"
            );
        }

        [Fact]
        public void ToOperation_ModelIsPrimitive_ShouldHaveTypeSet()
        {
            new SwaggerRouteData
            {
                OperationMethod = HttpMethod.Get,
                OperationModel = typeof(string),
            }.ToOperation().ShouldEqual(
                new Operation
                {
                    Method = HttpMethod.Get,
                    OperationId = "get",
                    Type = "string",
                    Parameters = Enumerable.Empty<Parameter>()
                }
            );
        }

        [Fact]
        public void ToOperation_ModelIsPrimitiveCollection_ShouldHaveTypeArrayAndItemsTypeSet()
        {
            new SwaggerRouteData
            {
                OperationMethod = HttpMethod.Get,
                OperationModel = typeof(string[]),
            }.ToOperation().ShouldEqual(
                new Operation
                {
                    Method = HttpMethod.Get,
                    OperationId = "get",
                    Type = "array",
                    Items = new Item { Type = "string" },
                    Parameters = Enumerable.Empty<Parameter>()
                }
            );
        }

        [Fact]
        public void ToOperation_NoModel_ShouldHaveTypeVoid()
        {
            new SwaggerRouteData
            {
                OperationMethod = HttpMethod.Get
            }.ToOperation().ShouldEqual(
                new Operation
                {
                    Method = HttpMethod.Get,
                    OperationId = "get",
                    Type = "void",
                    Parameters = Enumerable.Empty<Parameter>()
                }
            );
        }

        [Fact]
        public void ToParameter_BodyParam_ShouldHaveTypeSetToModelId()
        {
            new SwaggerParameterData
            {
                ParamIn = ParameterIn.Body,
                ParameterModel = typeof(TestModel)
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Name = "body",
                    In = ParameterIn.Body,
                    Type = SwaggerConfig.ModelIdConvention(typeof(TestModel)),
                },
                "Type field MUST be used to link to other models."
            );
        }

        [Fact]
        public void ToParameter_BodyParamWithCustomName_ShouldHaveNameBody()
        {
            new SwaggerParameterData
            {
                ParamIn = ParameterIn.Body,
                ParameterModel = typeof(TestModel),
                Name = "SomeName"
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Name = "body",
                    In = ParameterIn.Body,
                    Type = SwaggerConfig.ModelIdConvention(typeof(TestModel)),
                },
                "If paramType is \"body\", the name is used only for Swagger-UI and Swagger-Codegen. In this case, the name MUST be \"body\"."
            );
        }

        [Fact]
        public void ToParameter_BodyParamWithoutName_ShouldHaveNameBody()
        {
            new SwaggerParameterData
            {
                ParamIn = ParameterIn.Body,
                ParameterModel = typeof(TestModel)
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Name = "body",
                    In = ParameterIn.Body,
                    Type = SwaggerConfig.ModelIdConvention(typeof(TestModel)),
                },
                "If paramType is \"body\", the name is used only for Swagger-UI and Swagger-Codegen. In this case, the name MUST be \"body\"."
            );
        }

        [Fact]
        public void ToParameter_FormParamWithContainerModel_ShouldNotAllowMultiple()
        {
            new SwaggerParameterData
            {
                ParamIn = ParameterIn.Form,
                ParameterModel = typeof(string[])
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Type = "string",
                    In = ParameterIn.Form,
                },
                "AllowMultiple is null when container type is used in Form param"
            );
        }

        [Fact]
        public void ToParameter_PathParamNotExplicitlySetRequired_ShouldBeRequired()
        {
            new SwaggerParameterData
            {
                ParamIn = ParameterIn.Path,
                ParameterModel = typeof(string)
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Type = "string",
                    Required = true,
                    In = ParameterIn.Path,
                },
                "If paramType is \"path\" then this field MUST be included and have the value true."
            );
        }

        [Fact]
        public void ToParameter_QueryParamWithContainerModel_ShouldAllowMultiple()
        {
            new SwaggerParameterData
            {
                ParamIn = ParameterIn.Query,
                ParameterModel = typeof(string[])
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Type = "string",
                    AllowMultiple = true,
                    In = ParameterIn.Query,
                },
                "AllowMultiple is true when container type is used in Query param"
            );
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("a", "a")]
        [InlineData("ab", "ab")]
        [InlineData("aB", "aB")]
        [InlineData("AB", "aB")]
        [InlineData("Ab", "ab")]
        [InlineData(" a", "a")]
        [InlineData(" A", "a")]
        [InlineData("a b", "aB")]
        [InlineData("1", "_1")]
        [InlineData("a1b", "a1B")]
        public void ToCamelCase_TestCases(string value, string expected)
        {
            var valueString = value == null ? "null" : string.Format("\"{0}\"", value);
            var expectedString = expected == null ? "null" : string.Format("\"{0}\"", expected);
            value.ToCamelCase().ShouldEqual(expected, string.Format("{0}.ToCamelCase() should equal {1}", valueString, expectedString));
        }
    }
}