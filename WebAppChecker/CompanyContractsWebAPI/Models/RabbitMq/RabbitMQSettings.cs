namespace CompanyContractsWebAPI.Models.RabbitMq
{
    public class RabbitMQSettings
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
        public ConsumerSettings ContarctConsumer { get; set; }
        public ConsumerSettings ContarctDoneConsumer { get; set; }

    }

    public class ConsumerSettings
    {
        public User User { get; set; }

        public string CreatedQueue { get; set; }

        public string UpdatedQueue { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Pass { get; set; }
    }


}
