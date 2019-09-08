using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EmpRoleCompetence
    {
        public EmpRoleCompetence()
        {
            EcfEvaluationResult = new HashSet<EcfEvaluationResult>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public int CompetenceId { get; set; }
        public int CompetenceLevel { get; set; }

        [ForeignKey("CompetenceId")]
        [InverseProperty("EmpRoleCompetence")]
        public virtual EmpCompetence Competence { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("EmpRoleCompetence")]
        public virtual EcfRole Role { get; set; }
        [InverseProperty("CompetenceNavigation")]
        public virtual ICollection<EcfEvaluationResult> EcfEvaluationResult { get; set; }
    }
}
