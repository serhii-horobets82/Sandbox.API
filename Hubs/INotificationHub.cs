﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evoflare.API.Hubs
{
    public interface INotificationHub
    {
        Task SendNotification(string message);
        Task SendTestNotification(object message);
    }
}
