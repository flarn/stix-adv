using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Stix.Core;
using System.Net;

namespace Stix.Web.Common.Filters
{
    //Dont like this, but cant find a nicer solution
    public class ValidationErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationErrorException validationErrorException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new ObjectResult(ResponseBase.ValidationFailure(validationErrorException.ValidationError));
            }
        }
    }
}