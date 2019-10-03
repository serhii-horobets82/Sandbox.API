using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Evoflare.API.Core.Permissions;
using Evoflare.API.Models;
using Evoflare.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InviteController : BaseController
    {
        private readonly EvoflareDbContext _context;
        private readonly IInviteManager _inviteManager;

        public InviteController(IInviteManager inviteManger, EvoflareDbContext ctx)
        {
            _context = ctx;
            _inviteManager = inviteManger;
        }

        [Authorize(Policy = PolicyTypes.AdminPolicy.Crud)]
        [HttpPost]
        public async Task<ActionResult> Invite(Invite invite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emails = invite.Email.Split(";");

            foreach (var email in emails)
            {
                invite.Email = email;
                var isUserExists = await _context.Users.AnyAsync(x => string.Equals(x.Email, invite.Email, StringComparison.CurrentCultureIgnoreCase));

                if (isUserExists)
                {
                    return StatusCode((int)HttpStatusCode.Conflict);
                }

                try
                {
                    await _inviteManager.InviteNewUser(invite);
                }
                catch (Exception)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }

            return Ok();
        }
    }

    public class Invite
    {
        [Required]
        //[EmailAddress] //TODO: uncomment after with removing storokhamode
        //[MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public int Role { get; set; }

        public string RoleName { get; set; }

        public string UserName { get; set; }
    }
}
