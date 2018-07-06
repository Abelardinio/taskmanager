using System.Net.Http.Headers;
using System.Web.Http;

namespace TaskManager.WebApi
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional});

            config.Routes.MapHttpRoute(
                "Default",
                "{*anything}",
                new {controller = "Home", action = "Index"}
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}