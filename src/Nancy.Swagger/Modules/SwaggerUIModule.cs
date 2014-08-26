using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nancy.Swagger.Modules
{
    [SwaggerApi]
    public class SwaggerUIModule : NancyModule
    {
        private static Assembly _executingAssembly = Assembly.GetExecutingAssembly();

        private static string[] _resourceNames = _executingAssembly.GetManifestResourceNames();

        public SwaggerUIModule()
            : base(SwaggerConfig.SwaggerUIPath)
        {
            Get["/"] = _ => GetResource();
            Get["/{path*}"] = _ => GetResource(_.path);
        }

        private Response GetResource(string path = null)
        {
            if (path == null)
            {
                return Response.AsRedirect(SwaggerConfig.SwaggerUIPath + "/index.html");
            }

            if (GetResourceName(path) == null)
            {
                return HttpStatusCode.NotFound;
            }

            return new Response
            {
                ContentType = GetContentType(path),
                Contents = stream => WriteResource(stream, path)
            };
        }

        private void WriteResource(Stream stream, string path)
        {
            using (var rs = GetResourceStream(path))
            {
                if (IsTextResource(path))
                {
                    using (var sr = new StreamReader(rs, Encoding.UTF8))
                    {
                        // Poor-man's templating for now
                        var content = sr.ReadToEnd();
                        content = content.Replace("{{ResourceListingPath}}", Request.Url.SiteBase + "/" + SwaggerConfig.ResourceListingPath);
                        using (var sw = new StreamWriter(stream))
                        {
                            sw.Write(content);
                        }
                    }
                }
                else
                {
                    rs.CopyTo(stream);
                }
            }
        }

        private static string GetContentType(string path)
        {
            switch (Path.GetExtension(path))
            {
                case ".html": return "text/html";
                case ".js": return "application/javascript";
                case ".css": return "text/css";
                case ".png": return "image/png";
                default: return "text/html";
            }
        }

        private static string GetResourceName(string path)
        {
            // HACK: linked files as embedded resources have a resource name without extension, so
            //       remove the extension from the name

            var name = path.Replace('/', '.');
            var nameWithoutExtension = name.Substring(0, path.LastIndexOf('.'));

            var defaultName = "Nancy.Swagger.Resources.swagger_ui." + nameWithoutExtension;
            var customName = "Nancy.Swagger.Resources.swagger_ui_custom." + name;

            return _resourceNames.FirstOrDefault(n => n == customName)
                ?? _resourceNames.FirstOrDefault(n => n == defaultName);
        }

        private static Stream GetResourceStream(string path)
        {
            return _executingAssembly.GetManifestResourceStream(GetResourceName(path));
        }

        private static bool IsTextResource(string path)
        {
            return new [] { ".html", ".css", ".js" }.Contains(Path.GetExtension(path));
        }
    }
}