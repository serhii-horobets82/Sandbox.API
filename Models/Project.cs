using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class Project
    {
        public Project()
        {
            CustomerContact = new HashSet<CustomerContact>();
            EmployeeRelations = new HashSet<EmployeeRelations>();
            Position = new HashSet<Position>();
            ProjectCareerPath = new HashSet<ProjectCareerPath>();
            ProjectPosition = new HashSet<ProjectPosition>();
            Team = new HashSet<Team>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int OrganizationId { get; set; }

        [InverseProperty("Project")]
        public virtual ICollection<CustomerContact> CustomerContact { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<Position> Position { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectCareerPath> ProjectCareerPath { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectPosition> ProjectPosition { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<Team> Team { get; set; }
    }
}
