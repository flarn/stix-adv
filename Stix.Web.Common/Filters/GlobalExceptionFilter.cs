using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Stix.Core;
using System.Net;

namespace Stix.Web.Common.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public GlobalExceptionFilter(IHostEnvironment hostEnvironment) => _hostEnvironment = hostEnvironment;
        public void OnException(ExceptionContext context)
        {
            if (_hostEnvironment.IsDevelopment())
                return;

            if (context.Exception is EntityNotFoundException entityNotFoundException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new NotFoundResult();
            }
            else if (context.Exception is ControlledUnhandledException controlledUnhandledException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new ObjectResult(ResponseBase.Failure(controlledUnhandledException.Message));
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new ObjectResult(ResponseBase.Failure("An unhandled exception occured, we will check on this!"));
            }
        }
    }
}
