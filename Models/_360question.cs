﻿using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360question
    {
        public int Id { get; set; }
        public int QuestionToMarkId { get; set; }
        public string Question { get; set; }
        public int Order { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual _360questionToMark QuestionToMark { get; set; }
    }
}
