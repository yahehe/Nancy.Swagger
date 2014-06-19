using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Nancy.Swagger
{
    [PublicAPI]
    public class Primitive
    {
        private static readonly IDictionary<Type, Primitive> Primitives = new Dictionary<Type, Primitive>
        {
            // Integers
            { typeof(int), new Primitive("integer", "int32") },
            { typeof(long), new Primitive("integer", "int64") },

            // Numbers
            { typeof(float), new Primitive("number", "float") },
            { typeof(double), new Primitive("number", "double") },

            // Boolean
            { typeof(bool), new Primitive("boolean") },

            // Strings
            { typeof(string), new Primitive("string") },
            { typeof(byte), new Primitive("string", "byte") },
            { typeof(DateTime), new Primitive("string", "date-time") },
        };

        private Primitive(string type, string format = null)
        {
            Type = type;
            Format = format;
        }

        public string Type { get; private set; }

        public string Format { get; private set; }

        public static Primitive FromType(Type type)
        {
            return Primitives[Nullable.GetUnderlyingType(type) ?? type];
        }

        public static bool IsPrimitive(Type type)
        {
            return Primitives.ContainsKey(Nullable.GetUnderlyingType(type) ?? type);
        }
    }
}