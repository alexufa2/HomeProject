namespace CompanyContractsWebAPI.Models.RabbitMq
{
    public class RabbitMQSettings
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
        public QueueSettings ContarctCreatedQueue { get; set; }

    }

    public class QueueSettings
    {
        public string Name { get; set; }
        public User User { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Pass { get; set; }
    }


}
