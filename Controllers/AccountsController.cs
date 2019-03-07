using System.Threading.Tasks;
using Evoflare.API.Auth.Models;
using Evoflare.API.Helpers;
using Evoflare.API.Models;
using Evoflare.API.Services;
using Evoflare.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IActivityLogService activityLogService;
        private readonly ApplicationDbContext appDbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountsController(UserManager<ApplicationUser> userManager, ApplicationDbContext appDbContext,
            IActivityLogService activityLogService)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
            this.activityLogService = activityLogService;
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
                LastName = model.LastName,
                Age = model.Age,
                Gender = model.Gender
            };

            var result = await userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await activityLogService.AddActivityAsync("",
                $"Register new user: username {model.Email}, id={userIdentity.Id} ", 0);

            await appDbContext.Profile.AddAsync(new UserProfile {IdentityId = userIdentity.Id, Locale = model.Locale});

            //await userManager.AddClaimAsync(userIdentity, new Claim(ClaimTypes.Gender, model.Gender));

            await appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}