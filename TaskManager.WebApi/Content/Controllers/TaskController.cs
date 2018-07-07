using System.Collections.Generic;
using System.Web.Http;

namespace TaskManager.WebApi.Controllers
{
    public class TaskController : ApiController
    {
        public IReadOnlyList<string> Get()
        {
            return new List<string>{"s1", "2"};
        }
    }
}