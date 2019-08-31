using System;
namespace Evoflare.API.Options
{
    public class ThirdParty
    {
        public Landing Landing { get; set; }
    }

    public class Landing
    {
        public Guid Key { get; set; }
    }
}
