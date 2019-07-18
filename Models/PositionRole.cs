using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class PositionRole
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public int RoleId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [ForeignKey("PositionId")]
        [InverseProperty("PositionRole")]
        public virtual Position Position { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("PositionRole")]
        public virtual EcfRole Role { get; set; }
    }
}
