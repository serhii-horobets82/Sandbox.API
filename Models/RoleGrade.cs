﻿using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class RoleGrade
    {
        public RoleGrade()
        {
            ProjectPosition = new HashSet<ProjectPosition>();
            ProjectPositionCompetence = new HashSet<ProjectPositionCompetence>();
            RoleGradeCompetence = new HashSet<RoleGradeCompetence>();
        }

        public int Id { get; set; }
        public int EmployeeTypeId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int OrganizationId { get; set; }

        public virtual EmployeeType EmployeeType { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<ProjectPosition> ProjectPosition { get; set; }
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
        public virtual ICollection<RoleGradeCompetence> RoleGradeCompetence { get; set; }
    }
}
