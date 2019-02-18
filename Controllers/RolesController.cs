using Evoflare.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly TechnicalEvaluationContext _db;

        public RolesController(TechnicalEvaluationContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<List<Role>> Get()
        {
            return await _db.Role.ToListAsync();
        }
    }
}
