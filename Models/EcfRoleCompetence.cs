using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EcfRoleCompetence
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Competence { get; set; }
        public int CompetenceLevel { get; set; }
    }
}
