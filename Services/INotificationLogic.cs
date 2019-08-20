using System.Threading.Tasks;

namespace Evoflare.API.Services
{
    public interface INotificationLogic
    {
        Task ManagerAssignedToProject(int managerId, int projectId);
    }
}