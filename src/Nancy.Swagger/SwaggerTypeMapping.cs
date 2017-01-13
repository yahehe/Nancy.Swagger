using System;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Swagger
{
    /// <summary>
    /// This class allows you to customize how you want a type to be represented in the swagger output. 
    /// For example, if a service serializes/deserializes DateTime as a string when being sent/received, 
    /// there should be a SwaggerTypeMapping from DateTime to string in order to make the swagger output match what is actually expected.
    /// </summary>
    public class SwaggerTypeMapping
    {
        private static readonly List<SwaggerTypeMapping> TypeMappings = new List<SwaggerTypeMapping>();

        public Type SourceType { get; }
        public Type TargetType { get; }

        private SwaggerTypeMapping(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        public static void AddTypeMapping(Type source, Type target)
        {
            TypeMappings.Add(new SwaggerTypeMapping(source, target));
        }

        public static bool IsMappedType(Type type)
        {
            return TypeMappings.Exists(x => x.SourceType == type);
        }

        public static Type GetMappedType(Type type, List<Type> previousTypes = null)
        {
            var returnType = TypeMappings.FirstOrDefault(x => x.SourceType == type)?.TargetType;

            //Check to see if there are any indirect mappings
            if (IsMappedType(returnType))
            {
                if (previousTypes == null)
                {
                    previousTypes = new List<Type>();
                }

                //Only perform recursion if there is no cycle yet
                if (!previousTypes.Contains(type))
                {
                    previousTypes.Add(type);
                    returnType = GetMappedType(returnType, previousTypes);
                }
            }

            return returnType;
        }
    }
}
