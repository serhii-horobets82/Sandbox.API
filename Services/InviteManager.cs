using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Evoflare.API.Auth.Identity;
using Evoflare.API.Auth.Models;
using Evoflare.API.Configuration;
using Evoflare.API.Controllers;
using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Evoflare.API.Services
{
    public class InviteManager : IInviteManager
    {
        private readonly IUserManager _userManager;
        private readonly ILogger _logger;
        private readonly EvoflareDbContext _context;
        private readonly ClientSetting _clientConfig;
        private readonly IEmailSender _emailSender;

        public InviteManager(IUserManager userManager
            , ILogger<InviteManager> logger
            , EvoflareDbContext context
            , IEmailSender emailSender
            , IOptions<ClientSetting> clientConfig)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _clientConfig = clientConfig.Value;
            _emailSender = emailSender;
        }

        public async Task InviteNewUser(Invite invite)
        {
            ApplicationUser newUser = null;

            try
            {
                newUser = await CreateNewUser(invite);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"User invite fails on: {nameof(CreateNewUser)}");
                throw;
            }

            try
            {
                await CreateNewEmployee(newUser, invite);
                var welcomeLink = await GenerateWelcomeLink(newUser);
                await SendWelcomeLink(invite.Email, welcomeLink);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"User invite fails");
                await DropUser(newUser);
                throw;
            }
        }

        private async Task<ApplicationUser> CreateNewUser(Invite invite)
        {
            _logger.LogDebug($"{nameof(InviteManager.CreateNewUser)} starts");

            var user = new ApplicationUser
            {
                Email = invite.Email,
                EmailConfirmed = false,
                UserName = invite.UserName ?? invite.Email,
            };
            var randomPassword = Guid.NewGuid().ToString();
            var r = await _userManager.CreateAsync(user, randomPassword);
            // lock user
            await _userManager.SetLockoutEnabledAsync(user, false);

            // set aspnet role according to selected employee type 
            var roleName = _userManager.MapTypeToRole(invite.Role);
            await _userManager.AddToRoleAsync(user, roleName);

            _context.Profile.Add(new UserProfile
            {
                IdentityId = user.Id,
            });

            if (!r.Succeeded)
            {
                var error = r.Errors.Select(x => x.Description).Aggregate((x, y) => $"{x}{Environment.NewLine}");
                throw new Exception("User creatin fails");
            }

            var newUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == invite.Email);

            _logger.LogDebug($"{nameof(InviteManager.CreateNewUser)} ends");

            return newUser;
        }

        private async Task CreateNewEmployee(ApplicationUser user, Invite invite)
        {
            var organization = await _context.Organization.FirstOrDefaultAsync();
            var employee = new Employee
            {
                UserId = user.Id,
                EmployeeTypeId = invite.Role,
                Name = invite.Email,
                NameTemp = invite.Email, //TODO: Why is this field obligatory?
                OrganizationId = organization.Id //TODO: Do we allow multiple orgs per DB?
            };

            _context.Employee.Add(employee);

            await _context.SaveChangesAsync();
        }

        private async Task<string> GenerateWelcomeLink(ApplicationUser user)
        {
            _logger.LogDebug($"{nameof(InviteManager.GenerateWelcomeLink)} starts");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            _logger.LogDebug($"{nameof(InviteManager.GenerateWelcomeLink)} ends");

            return $"{_clientConfig.Host}/{_clientConfig.NewUserPage}/{user.Id}?code={HttpUtility.UrlEncode(token)}";
        }

        private async Task SendWelcomeLink(string email, string restorePasswordLink)
        {
            _logger.LogDebug($"{nameof(InviteManager.SendWelcomeLink)} starts");

            var welcomeEmail = new WelcomeEmail(restorePasswordLink);

            await _emailSender.SendEmailAsync(email, welcomeEmail.Subject, welcomeEmail.Body);

            _logger.LogDebug($"{nameof(InviteManager.SendWelcomeLink)} ends");
        }

        private async Task DropUser(ApplicationUser newUser)
        {
            _logger.LogDebug($"{nameof(InviteManager.DropUser)} starts");

            try
            {
                var employee = await _context.Employee.FirstOrDefaultAsync(x => x.UserId == newUser.Id);

                if (employee != null)
                {
                    _context.Employee.Remove(employee);
                }

                _context.Users.Remove(newUser);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Droping user fails on: {nameof(DropUser)}");
            }

            _logger.LogDebug($"{nameof(InviteManager.DropUser)} ends");
        }
    }


    public class WelcomeEmail
    {
        public readonly string Subject = "Welcome, friend!";
        public readonly string Body = string.Empty;

        private const string _template = "Please use this <a href=\"{resetPasswordLink}\">link</a> to enter the app";

        public WelcomeEmail(string resetPasswordLink)
        {
            if (string.IsNullOrEmpty(resetPasswordLink))
            {
                throw new ArgumentException($"{nameof(resetPasswordLink)} can't be null");
            }

            Body = _template.Replace("{resetPasswordLink}", resetPasswordLink);
        }
    }
}
