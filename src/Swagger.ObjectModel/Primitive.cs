using System;
using System.Collections.Generic;

namespace Swagger.ObjectModel
{
    /// <summary>
    /// Represents a primitive Swagger data type.
    /// </summary>
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

        /// <summary>
        /// The primitive type.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// The format of the type, or <c>null</c>.
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// Gets primitive data about the given type.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">If the given type isn't a primitive.</exception>
        /// <param name="type">The type to get primitive data from.</param>
        /// <returns>Primitive data about the given type.</returns>
        public static Primitive FromType(Type type)
        {
            var primitiveType = Nullable.GetUnderlyingType(type) ?? type;

            Primitive primitive;
            if (Primitives.TryGetValue(primitiveType, out primitive))
            {
                return primitive;
            }

            throw new ArgumentOutOfRangeException("type",
                string.Format("The given type, '{0}', is not a primitive.", type.FullName));
        }

        /// <summary>
        /// Determines whether the type is defined as a Swagger primitive.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><c>true</c> if the type is a primitive, otherwise <c>false</c>.</returns>
        public static bool IsPrimitive(Type type)
        {
            return Primitives.ContainsKey(Nullable.GetUnderlyingType(type) ?? type);
        }
    }
}