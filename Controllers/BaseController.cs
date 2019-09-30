using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Evoflare.API.ClaimsExtensions;

namespace Evoflare.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public abstract class BaseController : Controller
    {

        protected virtual IActionResult InvokeHttp404()
        {
            Response.StatusCode = 404;
            return new EmptyResult();
        }

        protected virtual int GetEmployeeId()
        {
            return User.GetEmployeeId();
        }
        protected virtual int GetOrganizationId()
        {
            return User.GetOrganizationId();
        }
        protected virtual string GetOrganizationName()
        {
            return User.GetOrganizationName();
        }

        protected virtual IEnumerable<PermissionClaimValue> GetUserPermissions()
        {
            return User.GetUserPermissions();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("xxxx", new string[] { "yyyy" });
            base.OnActionExecuted(context);
        }
    }
}