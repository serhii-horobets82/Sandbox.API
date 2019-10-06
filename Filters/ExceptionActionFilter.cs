using System;
using Evoflare.API.Core;
using Evoflare.API.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Evoflare.API.Filters
{
    public class ExceptionActionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ExceptionActionFilter(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        #region Overrides of ExceptionFilterAttribute

        public override void OnException(ExceptionContext context)
        {
            var errorMessage = "An error has occurred.";

            var exception = context.Exception;
            if (exception == null)
            {
                // Should never happen.
                return;
            }

            ErrorResponseModel internalErrorModel = null;
            //var actionDescriptor = (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor) context.ActionDescriptor;
            //Type controllerType = actionDescriptor.ControllerTypeInfo;

            if (exception is BadRequestException badRequestException)
            {
                context.HttpContext.Response.StatusCode = 400;
                if (badRequestException.ModelState != null)
                {
                    internalErrorModel = new ErrorResponseModel(badRequestException.ModelState);
                }
                else
                {
                    errorMessage = badRequestException.Message;
                }
            }
            else if (exception is ApplicationException)
            {
                context.HttpContext.Response.StatusCode = 402;
            }
            else if (exception is NotFoundException)
            {
                errorMessage = "Resource not found.";
                context.HttpContext.Response.StatusCode = 404;
            }
            else if (exception is SecurityTokenValidationException)
            {
                errorMessage = "Invalid token.";
                context.HttpContext.Response.StatusCode = 403;
            }
            else if (exception is UnauthorizedAccessException)
            {
                errorMessage = "Unauthorized.";
                context.HttpContext.Response.StatusCode = 401;
            }
            else
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ExceptionActionFilter>>();
                logger.LogError(0, exception, exception.Message);
                errorMessage = "An unhandled server error has occurred.";
                context.HttpContext.Response.StatusCode = 500;
            }

            var errorModel = internalErrorModel ?? new ErrorResponseModel(errorMessage);
            if (_hostingEnvironment.IsDevelopment())
            {
                errorModel.ExceptionMessage = exception.Message;
                errorModel.ExceptionStackTrace = exception.StackTrace;
                errorModel.InnerExceptionMessage = exception?.InnerException?.Message;
            }
            context.Result = new ObjectResult(errorModel);
        }

        #endregion
    }
}