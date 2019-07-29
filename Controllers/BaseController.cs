using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evoflare.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected virtual IActionResult InvokeHttp404()
        {
            Response.StatusCode = 404;
            return new EmptyResult();
        }

        protected virtual int? GetEmployeeId()
        {
            var employeeId = User.Claims.FirstOrDefault(x => x.Type == Constants.JwtClaimIdentifiers.EmployeeId)?.Value;
            if (!string.IsNullOrEmpty(employeeId))
                return Convert.ToInt32(employeeId);
            return null;
        }
    }
}