using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evoflare.API.Hubs
{
    public class NotificationData
    {
        public string Message { get; set; }
        public int NotificationId { get; set; }
        public DateTime Date { get; set; }
    }
}
