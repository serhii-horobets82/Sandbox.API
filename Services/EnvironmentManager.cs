using System;
using System.Threading.Tasks;
using Evoflare.API.Controllers;
using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evoflare.API.Services
{
    public class EnvironmentManager : IEnvironmentManager
    {
        private readonly ILogger<EnvironmentManager> _logger;
        private readonly IInviteManager _invite;
        private readonly EvoflareDbContext _context;

        public EnvironmentManager(ILogger<EnvironmentManager> logger, IInviteManager invite, EvoflareDbContext ctx)
        {
            _logger = logger;
            _invite = invite;
            _context = ctx;
        }

        public async Task StartNewEnvironment(EnvironmentDefinition payload)
        {
            try
            {
                var connectionString = await CreateNewDb(payload);
                await SeedDatabase(connectionString);
                var adminRoleId = await GetAdminRoleId(connectionString);
                await CreateAdmin(connectionString, adminRoleId, payload);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"{nameof(StartNewEnvironment)} fails", e);
                throw;
            }
        }

        private async Task SeedDatabase(string connectionString)
        {
            _logger.LogDebug("Seeding new database starts");
            _logger.LogDebug("Seeding new database starts");
            await Task.Run(() => { });
        }

        private async Task CreateAdmin(string connectionString, int adminRoleId, EnvironmentDefinition environment)
        {

            await Task.Run(() => { });
            //await _invite.InviteNewUser(new Invite
            //{
            //    Email = environment.Email,
            //    Role = adminRoleId,
            //    UserName = environment.UserName
            //});
        }

        private async Task<int> GetAdminRoleId(string connectionString)
        {
            var adminRole = await _context
                    .EmployeeType
                    .FirstOrDefaultAsync((x) => string.Equals(x.Type, "admin", StringComparison.InvariantCultureIgnoreCase));

            return adminRole.Id;
        }

        private async Task<string> CreateNewDb(EnvironmentDefinition payload)
        {
            _logger.LogDebug("Creating new database starts");
            try
            {
                var cs = await Task.FromResult("Fake connection string");
                _logger.LogDebug("Creating new database ends successfully");
                return cs;
            }
            catch (Exception e)
            {
                _logger.LogCritical("Creating new DB fails", e);
                throw;
            }
        }
    }
}
