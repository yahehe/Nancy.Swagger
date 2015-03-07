using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ResponseAttribute : Attribute
    {
        public ResponseAttribute(HttpStatusCode code)
            : this(code, null, null) { }

        public ResponseAttribute(HttpStatusCode code, string message)
            : this(code, message, null) { }

        public ResponseAttribute(HttpStatusCode code, Type responseModel)
            : this(code, null, responseModel) { }

        public ResponseAttribute(HttpStatusCode code, string message, Type model)
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