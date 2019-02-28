using System.Threading.Tasks;
using Evoflare.API.Auth.Models;
using Evoflare.API.Helpers;
using Evoflare.API.Models;
using Evoflare.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountsController(UserManager<ApplicationUser> userManager, ApplicationDbContext appDbContext)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdentity = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}