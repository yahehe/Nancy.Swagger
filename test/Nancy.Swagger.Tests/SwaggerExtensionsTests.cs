using Swagger.ObjectModel;
using Swagger.ObjectModel.ApiDeclaration;
using System.Linq;
using Xunit;

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
                    Ref = typeof(TestModel).DefaultModelId()
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
                    Items = new Items { Type = "string" }
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
                    Items = new Items { Ref = typeof(TestModel).DefaultModelId() }
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
                    Type = typeof(TestModel).DefaultModelId(),
                    Parameters = Enumerable.Empty<Parameter>()
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
                    Type = "array",
                    Items = new Items { Ref = typeof(TestModel).DefaultModelId() },
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
                    Type = "array",
                    Items = new Items { Type = "string" },
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
                ParamType = ParameterType.Body,
                ParameterModel = typeof(TestModel)
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Name = "body",
                    ParamType = ParameterType.Body,
                    Type = typeof(TestModel).DefaultModelId(),
                },
                "Type field MUST be used to link to other models."
            );
        }

        [Fact]
        public void ToParameter_BodyParamWithCustomName_ShouldHaveNameBody()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Body,
                ParameterModel = typeof(TestModel),
                Name = "SomeName"
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Name = "body",
                    ParamType = ParameterType.Body,
                    Type = typeof(TestModel).DefaultModelId(),
                },
                "If paramType is \"body\", the name is used only for Swagger-UI and Swagger-Codegen. In this case, the name MUST be \"body\"."
            );
        }

        [Fact]
        public void ToParameter_BodyParamWithoutName_ShouldHaveNameBody()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Body,
                ParameterModel = typeof(TestModel)
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Name = "body",
                    ParamType = ParameterType.Body,
                    Type = typeof(TestModel).DefaultModelId(),
                },
                "If paramType is \"body\", the name is used only for Swagger-UI and Swagger-Codegen. In this case, the name MUST be \"body\"."
            );
        }

        [Fact]
        public void ToParameter_FormParamWithContainerModel_ShouldNotAllowMultiple()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Form,
                ParameterModel = typeof(string[])
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Type = "string",
                    ParamType = ParameterType.Form,
                },
                "AllowMultiple is null when container type is used in Form param"
            );
        }

        [Fact]
        public void ToParameter_PathParamNotExplicitlySetRequired_ShouldBeRequired()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Path,
                ParameterModel = typeof(string)
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Type = "string",
                    Required = true,
                    ParamType = ParameterType.Path,
                },
                "If paramType is \"path\" then this field MUST be included and have the value true."
            );
        }

        [Fact]
        public void ToParameter_QueryParamWithContainerModel_ShouldAllowMultiple()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Query,
                ParameterModel = typeof(string[])
            }.ToParameter().ShouldEqual(
                new Parameter
                {
                    Type = "string",
                    AllowMultiple = true,
                    ParamType = ParameterType.Query,
                },
                "AllowMultiple is true when container type is used in Query param"
            );
        }
    }
}