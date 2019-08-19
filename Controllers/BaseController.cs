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
    }
}