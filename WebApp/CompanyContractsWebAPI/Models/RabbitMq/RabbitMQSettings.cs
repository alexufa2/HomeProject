namespace CompanyContractsWebAPI.Models.RabbitMq
{
    public class RabbitMQSettings
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
        public string ToCheckerExhange { get; set; }
        public Sender ContarctSender { get; set; }

        public Sender ContarctDoneSender { get; set; }

    }

    public class Sender
    {
        public string CreateRoutingKey { get; set; }
        public string UpdatedRoutingKey { get; set; }
        public User User { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Pass { get; set; }
    }


}
