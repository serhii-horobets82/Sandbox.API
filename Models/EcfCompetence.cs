using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class _EcfCompetence
    {
        [StringLength(3)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Summary { get; set; }
    }
}
