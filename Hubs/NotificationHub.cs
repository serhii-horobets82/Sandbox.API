using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Evoflare.API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationHub: Hub<INotificationHub>
    {
        public static readonly ConcurrentDictionary<string, string> UserToConnection 
            = new ConcurrentDictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            UserToConnection.TryAdd(Context.UserIdentifier, Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserToConnection.TryRemove(Context.UserIdentifier, out var t);
            return base.OnDisconnectedAsync(exception);
        }
        //public async Task SendNotification(string message)
        //{
        //    await Clients.All.SendAsync("MyEvent", new { a = 1, b="AAsdassdfads df"});
        //}
    }
}
