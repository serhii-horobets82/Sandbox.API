using System.Threading.Tasks;
using Evoflare.API.Auth.Models;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Evoflare.API.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly EvoflareDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public ActivityLogService(UserManager<ApplicationUser> userManager, EvoflareDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task AddActivityAsync(string user, string action, int level)
        {
            var activityLog = new ActivityLog(user, action, level);

            await context.ActivityLogs.AddAsync(activityLog);
            await context.SaveChangesAsync();
        }
    }
}