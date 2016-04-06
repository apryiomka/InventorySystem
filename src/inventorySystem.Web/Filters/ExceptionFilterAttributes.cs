using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using inventorySyctem.Services.Validation;

namespace inventorySystem.Web.Filters
{
    /// <summary>
    /// Handles <see cref="ArgumentNullException"/> errors and returns bad request
    /// </summary>
    public class ArgumentNullExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ArgumentNullException)
            {
                //return bad request
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                //context.Response.Content - set the content of the message
            }
        }
    }

    /// <summary>
    /// Handles <see cref="EntityValidationException"/> errors and returns bad request
    /// </summary>
    public class EntityValidationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is EntityValidationException)
            {
                //return bad request
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                //context.Response.Content - set the content of the message
            }
        }
    }
}