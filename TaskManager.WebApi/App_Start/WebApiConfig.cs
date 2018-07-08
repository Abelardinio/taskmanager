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
                "{controller}/{id}",
                new {id = RouteParameter.Optional});

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}