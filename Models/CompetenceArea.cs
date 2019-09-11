using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class CompetenceArea
    {
        public CompetenceArea()
        {
            Competence = new HashSet<Competence>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(90)]
        public string Name { get; set; }
        [Required]
        [StringLength(600)]
        public string Description { get; set; }

        [InverseProperty("CompetenceArea")]
        public virtual ICollection<Competence> Competence { get; set; }
    }
}
