using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class PositionRole
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Position Position { get; set; }
        public virtual EcfRole Role { get; set; }
    }
}
