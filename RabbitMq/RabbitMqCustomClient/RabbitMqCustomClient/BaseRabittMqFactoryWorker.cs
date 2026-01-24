using RabbitMQ.Client;

namespace RabbitMqCustomClient
{
    public abstract  class BaseRabittMqFactoryWorker
    {
        protected ConnectionFactory Factory;

        public BaseRabittMqFactoryWorker(string hostName, string virtualHost, int port, string userName, string password)
        {
            Factory = new ConnectionFactory()
            {
                HostName = hostName,
                VirtualHost = virtualHost,
                Port = port,
                UserName = userName,
                Password = password
            };
        }
    }
}
