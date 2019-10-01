using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Evoflare.API.Repositories;
using Evoflare.API.Models;
using Evoflare.API.Core.Models;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallationsController : BaseController
    {
        private readonly EvoflareDbContext _context;
        private readonly IRepository<Installation> _installationRepository;

        public InstallationsController(
            IRepository<Installation> installationRepository,
            EvoflareDbContext context)
        {
            _installationRepository = installationRepository;
            _context = context;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Get(Guid id)
        {
            var installation = await _installationRepository.GetByIdAsync(id);
            if (installation == null)
            {
                return NotFound();
            }
            return Ok(installation);
        }

        // [HttpPost("")]
        // [AllowAnonymous]
        // public async Task<ActionResult> Post([FromBody] InstallationRequestModel model)
        // {
        //     var installation = model.ToInstallation();
        //     await _installationRepository.CreateAsync(installation);
        //     return new InstallationResponseModel(installation, true);
        // }
    }
}
