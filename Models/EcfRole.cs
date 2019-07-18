using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        public virtual ICollection<EcfRoleCompetence> EcfRoleCompetence { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<PositionRole> PositionRole { get; set; }
    }
}
