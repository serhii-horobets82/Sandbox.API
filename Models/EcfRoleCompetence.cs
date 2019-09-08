using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class _EcfRoleCompetence
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        [Required]
        [StringLength(3)]
        public string CompetenceId { get; set; }
        public int CompetenceLevel { get; set; }
    }
}
