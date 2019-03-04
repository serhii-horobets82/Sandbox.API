namespace Evoflare.API.Auth.Models
{
    public class Token
    {
        public string Id { get; set; }
        public long ExpiresIn { get; set; }
        public string AuthToken { get; set; }
    }
}
