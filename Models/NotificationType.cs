using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class NotificationType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Template { get; set; }
    }
}
