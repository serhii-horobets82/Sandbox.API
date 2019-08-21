using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evoflare.API.Models;
using Evoflare.API.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly EvoflareDbContext _context;
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

        public NotificationsController(EvoflareDbContext context, IHubContext<NotificationHub, INotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        //// GET: api/Notifications/types
        //[HttpGet("types")]
        //public async Task<ActionResult<IEnumerable<NotificationType>>> GetNotificationTypes()
        //{
        //    return await _context.NotificationType.ToListAsync();
        //}

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotification()
        {
            return await _context.Notification.ToListAsync();
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await _context.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        //// PUT: api/Notifications/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutNotification(int id, Notification notification)
        //{
        //    if (id != notification.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(notification).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!NotificationExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        public class M
        {
            public int NotificationTypeId { get; set; }
            public NotificationData Data { get; set; }
            public class NotificationData
            {
                public string Name { get; set; }
                public string Project { get; set; }
            }
        }
        // TODO: remove this method from front-end
        //// POST: api/Notifications/send
        //[HttpPost("send")]
        //public async Task<IActionResult> SendNotification(M notification)
        //{
        //    var type = await _context.NotificationType.FirstOrDefaultAsync(x => x.Id == notification.NotificationTypeId);

        //    await _hubContext
        //      .Clients
        //      .All
        //      .SendTestNotification(new { Template = type.Template, Data = notification.Data, Type = notification.NotificationTypeId });
        //    return new JsonResult(new { r = 2 });
        //}

        // POST: api/Notifications
        //[HttpPost]
        //public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        //{
        //    _context.Notification.Add(notification);
        //    await _context.SaveChangesAsync();
        //    await _hubContext
        //      .Clients
        //      .All
        //      .SendNotification("Message...");
        //    return CreatedAtAction("GetNotification", new { id = notification.Id }, notification);
        //}

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Notification>> DeleteNotification(int id)
        {
            var notification = await _context.Notification.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

        private bool NotificationExists(int id)
        {
            return _context.Notification.Any(e => e.Id == id);
        }
    }
}
