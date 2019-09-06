using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360Questionnarie")]
    public partial class _360questionnarie
    {
        public _360questionnarie()
        {
            _360questionnarieStatement = new HashSet<_360questionnarieStatement>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsForManager { get; set; }

        [InverseProperty("Questionnarie")]
        public virtual ICollection<_360questionnarieStatement> _360questionnarieStatement { get; set; }
    }
}
