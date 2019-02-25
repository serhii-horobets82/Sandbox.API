using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EcfRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}
