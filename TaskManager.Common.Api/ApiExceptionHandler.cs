using System.Net;
using System.Net.Http;
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
    }
}