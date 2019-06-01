using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class ProjectPosition
    {
        public ProjectPosition()
        {
            ProjectPositionCompetence = new HashSet<ProjectPositionCompetence>();
        }

        public int Id { get; set; }
        public int RoleGradeId { get; set; }
        public string PositionName { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual RoleGrade RoleGrade { get; set; }
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
    }
}
