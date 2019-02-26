using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {

        private readonly BaseAppContext db;

        public VersionController(BaseAppContext db)
        {
            this.db = db;
        }

        //[HttpGet(Name = "GetAssemblyVersion")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Version of application.", typeof(string))]
        //public string Get()
        //{
        //    return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //}

        [HttpGet(Name = "GetAppVersion")]
        [SwaggerResponse(StatusCodes.Status200OK, "Version of application in database", typeof(string))]
        public async Task<CoreAppVersion> GetAppVersion()
        {
            return await db.AppVersion.FirstOrDefaultAsync();
        }
    }
}
