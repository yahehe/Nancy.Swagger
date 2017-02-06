using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nancy.Swagger.Annotations
{
    public static class SwaggerAnnotationsConfig
    {
        /// <summary>
        /// Hides routes that are not annotated, such as the /swagger.json and /api-docs routes
        /// Note: This may hide routes with malformed annotations
        /// </summary>
        public static bool ShowOnlyAnnotatedRoutes { get; set; }
    }
}
