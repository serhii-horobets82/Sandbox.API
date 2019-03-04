using System.Threading.Tasks;

namespace Evoflare.API.Services
{
    public interface IActivityLogService
    {
        Task AddActivityAsync(string User, string Action, int Level);
    }
}
