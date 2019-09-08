using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class _EcfCompetenceLevel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(4)]
        public string CompetenceId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
    }
}
