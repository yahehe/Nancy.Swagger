using System;
using System.Collections.Generic;

namespace Nancy.Swagger
{
    public class SwaggerModelPropertyData
    {
        public SwaggerModelPropertyData()
        {
            Enum = new List<string>();
        }

        public object DefaultValue { get; set; }

        public string Description { get; set; }

        public IList<string> Enum { get; set; }

        public long? Maximum { get; set; }

        public long? Minimum { get; set; }

        public string Name { get; set; }

        public bool Required { get; set; }

        public Type Type { get; set; }

        public bool UniqueItems { get; set; }
    }
}