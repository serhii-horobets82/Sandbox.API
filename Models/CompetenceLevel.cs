using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evoflare.API.Models
{
    public partial class CompetenceLevel
    {
        public CompetenceLevel(int id, string competenceId, int level, string description)
        {
            Id = id;
            CompetenceId = competenceId;
            Level = level;
            Description = description;
        }

        public int Id { get; set; }
        public string CompetenceId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }

        public Competence Competence { get; set; }
    }
}
