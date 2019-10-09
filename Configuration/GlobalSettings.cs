namespace Evoflare.API.Configuration
{
    public class GlobalSettings
    {
        public bool SelfHosted { get; set; }
        public virtual string SiteName { get; set; }
        public virtual string StripeApiKey { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual string LogDirectory { get; set; }

        public virtual SqlSettings SqlServer { get; set; } = new SqlSettings();
        public virtual SqlSettings PostgreSql { get; set; } = new SqlSettings();
        public virtual MailSettings Mail { get; set; } = new MailSettings();

        public virtual SentrySettings Sentry { get; set; } = new SentrySettings();
    }

    public class SqlSettings
    {
        private string _connectionString;
        private string _readOnlyConnectionString;

        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value.Trim('"');
            }
        }

        public string ReadOnlyConnectionString
        {
            get => string.IsNullOrWhiteSpace(_readOnlyConnectionString) ?
                _connectionString : _readOnlyConnectionString;
            set
            {
                _readOnlyConnectionString = value.Trim('"');
            }
        }
    }

    public class ConnectionStringSettings
    {
        private string _connectionString;

        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value.Trim('"');
            }
        }
    }

    public class MailSettings
    {
        public string ReplyToEmail { get; set; }
        public string SendGridApiKey { get; set; }
        public string AmazonConfigSetName { get; set; }
        public SmtpSettings Smtp { get; set; } = new SmtpSettings();

        public class SmtpSettings
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public bool StartTls { get; set; } = false;
            public bool Ssl { get; set; } = false;
            public bool SslOverride { get; set; } = false;
            public string Username { get; set; }
            public string Password { get; set; }
            public bool TrustServer { get; set; } = false;
        }
    }
    public class SentrySettings
    {
        public string Dsn { get; set; }
    }
}