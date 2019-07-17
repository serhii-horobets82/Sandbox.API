﻿using System;
using System.Collections.Generic;

namespace Evoflare.API.Core.Models
{
    public partial class AppVersion
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Database { get; set; }
        public DateTime CreationDate { get; set; }
        public string Organization { get; set; }
    }
}
