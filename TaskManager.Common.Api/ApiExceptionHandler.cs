using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json;
using TaskManager.Common.Resources;
using TaskManager.Core.Exceptions;

namespace TaskManager.Common.Api
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
                message = ErrorMessages.Unknown;
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