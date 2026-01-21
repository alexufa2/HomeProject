using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace RabbitMqCustomClient
{
    public class RabbitMQSender
    {
        private ConnectionFactory _factory;

        public RabbitMQSender(string hostName, string virtualHost, int port, string userName, string password)
        {
            _factory = new ConnectionFactory()
            {
                HostName = hostName,
                VirtualHost = virtualHost,
                Port = port,
                UserName = userName,
                Password = password
            };
        }

        public async Task SendMessage<T>(T message, string exhange, string routeKey)
        {
            // Устанавливаем соединение
            using (var connection = await _factory.CreateConnectionAsync())
            {
                using (var channel = await connection.CreateChannelAsync())
                {
                    var properties = new BasicProperties { ContentType = "application/json" };
                    var body = ObjectToBytesConverter.ObjectToBytes(message);

                    // Публикуем сообщение
                    await channel.BasicPublishAsync(
                        exhange,
                        routeKey,
                        true,
                        properties,
                        body
                    );
                }
            }
        }
    }
}
