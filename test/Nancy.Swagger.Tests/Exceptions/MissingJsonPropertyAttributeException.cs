using System;

namespace Nancy.Swagger.Tests.Exceptions
{
    public class MissingJsonPropertyAttributeException : Exception
    {
        public MissingJsonPropertyAttributeException(string message) : base(message) { }
    }
}