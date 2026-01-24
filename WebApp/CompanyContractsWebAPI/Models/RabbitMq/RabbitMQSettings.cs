namespace CompanyContractsWebAPI.Models.RabbitMq
{
    public class RabbitMQSettings
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
        public SenderSettings ContarctCreateSender { get; set; }

    }

    public class SenderSettings
    {
        public string ExhangeName { get; set; }
        public string RoutingKey { get; set; }
        public User User { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Pass { get; set; }
    }


}
