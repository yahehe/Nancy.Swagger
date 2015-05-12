using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SwaggerResponseAttribute : Attribute
    {
        public SwaggerResponseAttribute(HttpStatusCode code)
            : this(code, null, null) { }

        public SwaggerResponseAttribute(HttpStatusCode code, string message)
            : this(code, message, null) { }

        public SwaggerResponseAttribute(HttpStatusCode code, Type responseModel)
            : this(code, null, responseModel) { }

        public SwaggerResponseAttribute(HttpStatusCode code, string message, Type model)
        {
            Code = code;
            Message = message ?? code.ToString();
            Model = model;
        }

        public HttpStatusCode Code { get; private set; }

        public string Message { get; set; }

        public Type Model { get; set; }
    }
}