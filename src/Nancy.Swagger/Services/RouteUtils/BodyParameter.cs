using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Services.RouteUtils
{

    public class BodyParameter<T> : BodyParameter
    {
        public BodyParameter(ISwaggerModelCatalog modelCatalog)
        {
            this.AddBodySchema<T>(modelCatalog);
        }
    }
}
