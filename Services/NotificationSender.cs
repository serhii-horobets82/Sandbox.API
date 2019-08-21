using Evoflare.API.Hubs;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evoflare.API.Services
{
    public interface INotificationSender
    {
        Task NotifyEmployee(int employeeId, string message);
        Task NotifyEmployees(ICollection<int> ids, string message);
    }

    public class NotificationSender : INotificationSender
    {
        private readonly EvoflareDbContext context;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IHubContext<NotificationHub, INotificationHub> hubContext;

        public NotificationSender(
            EvoflareDbContext context,
            IHttpContextAccessor contextAccessor,
            IHubContext<NotificationHub, INotificationHub> hubContext)
        {
            this.context = context;
            this.contextAccessor = contextAccessor;
            this.hubContext = hubContext;
        }

        public async Task NotifyEmployee(int employeeId, string message)
        {
            await NotifyEmployeeInternal(new[] { employeeId }, message);
        }

        public async Task NotifyEmployees(ICollection<int> ids, string message)
        {
            await NotifyEmployeeInternal(ids, message);
        }

        private async Task NotifyEmployeeInternal(ICollection<int> ids, string message)
        {
            var currentId = contextAccessor.HttpContext.User.GetEmployeeId();
            var notifications = await SaveToDb(ids, currentId, message);
            foreach (var item in notifications)
            {
                SendHubNotification(item);
            }
        }

        private async Task<ICollection<Notification>> SaveToDb(ICollection<int> ids, int initiatorId, string message)
        {
            var notifications = ids.Select(id =>
                new Notification
                {
                    Message = message,
                    CreatedBy = initiatorId,
                    CreatedDate = DateTime.UtcNow,
                    EmployeeId = id,
                    Active = true,
                }).ToList();
            context.Notification.AddRange(notifications);
            await context.SaveChangesAsync();
            return notifications;
        }

        private void SendHubNotification(Notification notification)
        {
            var connectionId = NotificationHub.EmployeeToConnection[notification.EmployeeId];
            var data = new NotificationData
            {
                Message = notification.Message,
                Id = notification.Id,
                CreatedDate = notification.CreatedDate
            };
            hubContext.Clients.Client(connectionId).SendNotification(data);
        }

        //private void SendHubNotifications(ICollection<int> ids, int initiatorId)
        //{
        //    // TODO: pass Notification.Id for the specific record
        //    var connectionIds = ids.Select(id => NotificationHub.EmployeeToConnection[id]).ToList();
        //    hubContext.Clients.Clients(connectionIds).SendNotification("a");
        //}
    }
}
