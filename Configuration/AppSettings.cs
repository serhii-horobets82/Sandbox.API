using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evoflare.API.Configuration
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public bool RecreateDbOnStart { get; set; }
        public string DbCoreSchema { get; set; }
        public int RetryTimeout { get; set; }
    }
}
