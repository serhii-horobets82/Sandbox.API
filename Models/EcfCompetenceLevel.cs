using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EcfCompetenceLevel
    {
        public EcfCompetenceLevel()
        {
            RoleGradeCompetence = new HashSet<RoleGradeCompetence>();
        }

        public int Id { get; set; }
        public string CompetenceId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }

        public virtual EcfCompetence Competence { get; set; }
        public virtual ICollection<RoleGradeCompetence> RoleGradeCompetence { get; set; }
    }
}
