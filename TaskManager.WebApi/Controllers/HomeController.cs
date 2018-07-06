using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace TaskManager.WebApi.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            var path = HttpContext.Current.Server.MapPath("~/Content/index.html");
            var response = new HttpResponseMessage {Content = new StringContent(File.ReadAllText(path))};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
