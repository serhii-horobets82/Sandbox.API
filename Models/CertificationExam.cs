using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class CertificationExam
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
