using RabbitMQ.Client;

namespace RabbitMqCustomClient
{
    public class RabbitMqSender: BaseRabittMqFactoryWorker
    {
        public RabbitMqSender(string hostName, string virtualHost, int port, string userName, string password) :
            base(hostName, virtualHost, port, userName,password)
        { }

        public async Task SendMessage<T>(T message, string exhange, string routeKey)
        {
            // Устанавливаем соединение
            using (var connection = await Factory.CreateConnectionAsync())
            {
                using (var channel = await connection.CreateChannelAsync())
                {
                    var properties = new BasicProperties 
                    { 
                        ContentType = "application/json",
                        Persistent = true
                    };

                    var body = ObjectConverter.ObjectToBytes(message);

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
