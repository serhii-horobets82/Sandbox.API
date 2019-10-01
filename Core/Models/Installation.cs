using System;

namespace Evoflare.API.Core.Models
{
    public class Installation
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Key { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreationDate { get; internal set; } = DateTime.UtcNow;

        public void SetNewId()
        {
            Id = Guid.NewGuid();
        }
    }
}