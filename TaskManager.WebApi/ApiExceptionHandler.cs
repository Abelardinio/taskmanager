using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json;
using TaskManager.Core.Exceptions;

namespace TaskManager.WebApi
{
    public class ApiExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            string message;

            if (context.Exception is TaskManagerException)
            {

                message = context.Exception.Message;
            }
            else
            {
                message = "Unknown error occured. Contact your administrator for more details.";
            }

            var result = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { Message = message }))
            };

            context.Result = new ApiExceptionResult(result);
        }

        private class ApiExceptionResult : IHttpActionResult
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
}