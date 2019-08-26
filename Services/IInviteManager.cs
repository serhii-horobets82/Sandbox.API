using System.Threading.Tasks;
using Evoflare.API.Controllers;

namespace Evoflare.API.Services
{
    public interface IInviteManager
    {
        Task InviteNewUser(Invite invite);
    }
}