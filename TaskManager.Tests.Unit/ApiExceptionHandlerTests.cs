using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using FluentAssertions;
using NUnit.Framework;
using TaskManager.Common.Api;
using TaskManager.Common.Resources;
using TaskManager.Core.Exceptions;

namespace TaskManager.Tests.Unit
{
    [TestFixture]
    public class ApiExceptionHandlerTests
    {
        private const string Message = "TaskManagerExceptionMessage";
        private ExceptionHandlerContext _context;
        private readonly ApiExceptionHandler _handler = new ApiExceptionHandler();

        [Test]
        public async Task TaskManagerExceptionWasThrownTest()
        {
            SetExceptionHandlerContext(new TaskManagerException(Message));
            _handler.Handle(_context);
            await AssertResponseMessage(_context.Result, Message);
        }

        [Test]
        public async Task UnknownExceptionWasThrownTest()
        {
            SetExceptionHandlerContext(new Exception());
            _handler.Handle(_context);
            await AssertResponseMessage(_context.Result, ErrorMessages.Unknown);
        }

        private void SetExceptionHandlerContext(Exception ex)
        {
            var catchblock = new ExceptionContextCatchBlock("webpi", true, false);
            var configuration = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/test");
            request.SetConfiguration(configuration);
            var exceptionContext = new ExceptionContext(ex, catchblock, request);
            _context = new ExceptionHandlerContext(exceptionContext);
        }

        private static async Task AssertResponseMessage(IHttpActionResult result, string message)
        {
            var responseMessage = await result.ExecuteAsync(new CancellationToken());
            (await responseMessage.Content.ReadAsStringAsync()).Should().Contain(message);
        }
    }
}