using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class CustomerContact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int ProjectId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual Project Project { get; set; }
    }
}
