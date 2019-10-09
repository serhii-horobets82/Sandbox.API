using System;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Evoflare.API.Controllers
{
    public class MiscController : Controller
    {
        [HttpGet("~/alive")]
        [HttpGet("~/now")]
        public DateTime Get()
        {
            Log.Information("Calling /alive");
            return DateTime.UtcNow;
        }

        [HttpGet("~/echo/{param}")]
        public string Echo(string param)
        {
            return param;
        }

        [HttpGet("~/ip")]
        public JsonResult Ip()
        {
            return new JsonResult(new
            {
                Ip = HttpContext.Connection?.RemoteIpAddress?.ToString(),
                Headers = HttpContext.Request?.Headers,
            });
        }
    }
}
