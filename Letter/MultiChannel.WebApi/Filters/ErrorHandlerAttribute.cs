using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MultiChannel.WebApi.Filters
{
    /// <summary>
    /// Error handler attribute.
    /// </summary>
    public class ErrorHandlerAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlerAttribute"/> class.
        /// </summary>
        public ErrorHandlerAttribute()
        {
        }

        /// <summary>
        /// Override from exception.
        /// </summary>
        /// <param name="context">Original context.</param>
        public override void OnException(ExceptionContext context)
        {
            var code = HttpStatusCode.InternalServerError;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                error = new[] { $"Exception.Message: {context.Exception.Message}", $"InnerException.Message: {context.Exception.InnerException.Message}" }
            });
        }

        /// <summary>
        /// Override from Async context.
        /// </summary>
        /// <param name="context">Original context.</param>
        /// <returns>void task.</returns>
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var code = HttpStatusCode.InternalServerError;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                error = new[] { $"Exception.Message: {context.Exception.Message}", $"InnerException.Message: {context.Exception.InnerException.Message}" }
            });

            return base.OnExceptionAsync(context);
        }
    }
}
