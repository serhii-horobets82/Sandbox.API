using System.Collections.Generic;
using Evoflare.API.Core.Models;
using Evoflare.API.Models;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evoflare.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityLogsController
    {
        readonly EvoflareDbContext context;

        public ActivityLogsController(EvoflareDbContext context)
        {
            this.context = context;
        }

        // GET api/ActivityLogs
        [HttpGet]
        public IEnumerable<ActivityLog> Get()
        {
            return  context.ActivityLogs
                                    .OrderByDescending(i => i.ActivityDate)
                                    .Take(200);
        }

        // GET api/ActivityLogs/5
        [HttpGet("{id}", Name = "GetActivityLogs")]
        public ActivityLog Get(int id)
        {
            return context.ActivityLogs.Find(id);
        }

        // GET api/ActivityLogs/?=
        [HttpGet("search")]
        public IEnumerable<ActivityLog> Search(string q)
        {
            return context.ActivityLogs
                .Where((c) => c.User.Contains(q) || c.Action.Contains(q))
                .OrderByDescending(i => i.ActivityDate);
        }
    }

}
