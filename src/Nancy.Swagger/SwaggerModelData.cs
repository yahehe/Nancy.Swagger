using System;
using System.Collections.Generic;

namespace Nancy.Swagger
{
    public class SwaggerModelData
    {
        public SwaggerModelData()
        {
            Properties = new List<SwaggerModelPropertyData>();
        }

        public string Description { get; set; }

        public Type ModelType { get; set; }

        public IList<SwaggerModelPropertyData> Properties { get; set; }
    }
}