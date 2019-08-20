using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evoflare.API.Services
{
    public class NotificationLogic : INotificationLogic
    {
        private readonly EvoflareDbContext context;
        private readonly INotificationSender notificationSender;

        public NotificationLogic(EvoflareDbContext context, INotificationSender notificationSender)
        {
            this.context = context;
            this.notificationSender = notificationSender;
        }

        public async Task ManagerAssignedToProject(int managerId, int projectId)
        {
            // TODO: get employee name from httpContext
            var manager = await context.Employee.FirstOrDefaultAsync(e => e.Id == managerId);
            var project = await context.Project.FirstOrDefaultAsync(p => p.Id == projectId);
            var message = $"{manager.Name} {manager.Surname} has been assigned as a Manager to a Project {project.Name}";

            var managers = (await GetAllManagersIds()).Where(id => id != managerId);
            var employees = await GetAllEmployeesInProject(projectId);
            var ids = managers.Concat(employees).ToList();

            await notificationSender.NotifyEmployees(ids, message);

            var messageToManager = $"You have been assigned as a Manager to a Project {project.Name}";
            await notificationSender.NotifyEmployee(managerId, messageToManager);
        }

        private async Task<ICollection<int>> GetAllManagersIds()
        {
            return await context.Employee
                .Where(e => e.IsManager)
                .Select(e => e.Id)
                .ToListAsync();
        }

        private async Task<ICollection<int>> GetAllEmployeesInProject(int projectId)
        {
            return await context.EmployeeRelations
                .Where(e => e.ProjectId == projectId && e.EmployeeId.HasValue)
                .Select(e => e.EmployeeId.Value)
                .ToListAsync();
        }
    }
}
