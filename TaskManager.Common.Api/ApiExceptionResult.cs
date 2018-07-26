using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace TaskManager.Common.Api
{
    public class ApiExceptionResult : IHttpActionResult
    {
        private readonly HttpResponseMessage _responseMessage;

        public ApiExceptionResult(HttpResponseMessage responseMessage)
        {
            _responseMessage = responseMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_responseMessage);
        }
    }
}