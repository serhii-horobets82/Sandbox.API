using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EcfRole
    {
        public EcfRole()
        {
            EcfRoleCompetence = new HashSet<EcfRoleCompetence>();
            PositionRole = new HashSet<PositionRole>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        public virtual ICollection<EcfRoleCompetence> EcfRoleCompetence { get; set; }
        public virtual ICollection<PositionRole> PositionRole { get; set; }
    }
}
