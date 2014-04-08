using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Nancy.Swagger
{
    /// <summary>
    /// Class SwaggerPrimitive.
    /// </summary>
    [PublicAPI]
    public class SwaggerPrimitive
    {
        private static readonly IDictionary<Type, SwaggerPrimitive> Primitives = new Dictionary<Type, SwaggerPrimitive>
        {
            // Integers
            { typeof(int), new SwaggerPrimitive("integer", "int32") },
            { typeof(long), new SwaggerPrimitive("integer", "int64") },

            // Numbers
            { typeof(float), new SwaggerPrimitive("number", "float") },
            { typeof(double), new SwaggerPrimitive("number", "double") },

            // Boolean
            { typeof(bool), new SwaggerPrimitive("boolean") },

            // Strings
            { typeof(string), new SwaggerPrimitive("string") },
            { typeof(byte), new SwaggerPrimitive("string", "byte") },
            { typeof(DateTime), new SwaggerPrimitive("string", "date-time") },
        };

        private SwaggerPrimitive(string type, string format = null)
        {
            Type = type;
            Format = format;
        }

        public string Type { get; private set; }

        public string Format { get; private set; }

        public static SwaggerPrimitive FromType(Type type)
        {
            return Primitives[type];
        }
    }
}