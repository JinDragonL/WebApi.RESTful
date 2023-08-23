namespace WebApi.Restful.Core.Configuration
{
    public class EmailConfig
    {
        public string DefaultSender { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Provider { get; set; }
        public int Port { get; set; }
    }
}
