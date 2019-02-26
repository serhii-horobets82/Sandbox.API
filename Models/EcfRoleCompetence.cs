using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EcfRoleCompetence
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string CompetenceId { get; set; }
        public int CompetenceLevel { get; set; }

        public virtual EcfCompetence Competence { get; set; }
        public virtual EcfRole Role { get; set; }
    }
}
