using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [Required]
        public string Message { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ViewDate { get; set; }
    }
}
