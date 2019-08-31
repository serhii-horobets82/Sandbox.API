using System.Threading.Tasks;
using Evoflare.API.Controllers;

namespace Evoflare.API.Services
{
    public interface IEnvironmentManager
    {
        Task StartNewEnvironment(EnvironmentDefinition payload);
    }
}