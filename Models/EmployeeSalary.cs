using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EmployeeSalary
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Period { get; set; }
        public int Basic { get; set; }
        public int Bonus { get; set; }
        public bool Archived { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeSalary")]
        public virtual Employee Employee { get; set; }
    }
}
