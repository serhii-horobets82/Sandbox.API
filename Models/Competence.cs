using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evoflare.API.Models
{
    public partial class Competence
    {
        public Competence()
        {
            CompetenceLevel = new HashSet<CompetenceLevel>();
        }

        public Competence(string id, string name, string summary)
        {
            Id = id;
            Name = name;
            Summary = summary;
            CompetenceLevel = new HashSet<CompetenceLevel>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }

        public ICollection<CompetenceLevel> CompetenceLevel { get; set; }
    }
}
