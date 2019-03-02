using System;

namespace Evoflare.API.Auth.Models
{
    public class ActivityLog
    {
        public ActivityLog()
        {
        }

        public ActivityLog(string user, string action, int level)
        {
            User = user;
            Action = action;
            Level = level;
        }

        public int Id { get; set; }

        public string User { get; set; }

        public string Action { get; set; }

        public int Level { get; set; }

        public DateTime ActivityDate { get; set; } = DateTime.Now;
    }
}