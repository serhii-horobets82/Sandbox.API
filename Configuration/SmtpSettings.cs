namespace Evoflare.API.Configuration
{
    public class SmtpSettings
    {
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpHostname { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpSender { get; set; }
    }
}