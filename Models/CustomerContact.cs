using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class CustomerContact
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        public int ProjectId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        [InverseProperty("CustomerContact")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("ProjectId")]
        [InverseProperty("CustomerContact")]
        public virtual Project Project { get; set; }
    }
}
