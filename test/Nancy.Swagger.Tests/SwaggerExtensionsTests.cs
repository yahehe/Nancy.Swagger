using Should;
using Swagger.ObjectModel.ApiDeclaration;
using Xunit;

namespace Nancy.Swagger.Tests
{
    public class SwaggerExtensionsTests
    {
        [Fact]
        public void ToParameter_PathParamNotExplicitlySetRequired_ShouldBeRequired()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Path,
                ParameterModel = typeof(string)
            }.ToParameter().Required.ShouldEqual(true, "If paramType is \"path\" then this field MUST be included and have the value true.");
        }

        [Fact]
        public void ToParameter_QueryParamWithContainerModel_ShouldAllowMultiple()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Query,
                ParameterModel = typeof(string[])
            }.ToParameter().AllowMultiple.ShouldEqual(true, "The field may be used only if paramType is \"query\", \"header\" or \"path\".");
        }

        [Fact]
        public void ToParameter_FormParamWithContainerModel_ShouldNotAllowMultiple()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Form,
                ParameterModel = typeof(string[])
            }.ToParameter().AllowMultiple.ShouldEqual(null, "The field may be used only if paramType is \"query\", \"header\" or \"path\".");
        }

        [Fact]
        public void ToParameter_BodyParamWithCustomName_ShouldHaveNameBody()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Body,
                ParameterModel = typeof(object),
                Name = "SomeName"
            }.ToParameter().Name.ShouldEqual("body", "If paramType is \"body\", the name is used only for Swagger-UI and Swagger-Codegen. In this case, the name MUST be \"body\".");
        }

        [Fact]
        public void ToParameter_BodyParamWithoutName_ShouldHaveNameBody()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Body,
                ParameterModel = typeof(object)
            }.ToParameter().Name.ShouldEqual("body", "If paramType is \"body\", the name is used only for Swagger-UI and Swagger-Codegen. In this case, the name MUST be \"body\".");
        }

        [Fact]
        public void ToParameter_BodyParam_ShouldHaveTypeSetToModelId()
        {
            new SwaggerParameterData
            {
                ParamType = ParameterType.Body,
                ParameterModel = typeof(TestModel)
            }.ToParameter().Type.ShouldEqual(typeof(TestModel).DefaultModelId(), "Type field MUST be used to link to other models.");
        }
    }
}