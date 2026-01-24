using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqCustomClient
{
    public class RabbitMqConsumer<T>: BaseRabittMqFactoryWorker
    {


        public RabbitMqConsumer(string hostName, string virtualHost, int port, string userName, string password) :
           base(hostName, virtualHost, port, userName, password)
        { }


        public async Task StartConsumerAsync(string queue, Action<T> action)
        {
            var connection = await Factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            // Создаем асинхронного потребителя
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, args) =>
            {
                try
                {
                    var body = args.Body.ToArray();
                    T message = ObjectConverter.BytesToObject<T>(body);
                    action(message);

                    var properties = args.BasicProperties;

                    // Подтверждение обработки
                    await channel.BasicAckAsync(
                        deliveryTag: args.DeliveryTag,
                        multiple: false
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    // Отказ от сообщения с requeue = true
                    await channel.BasicNackAsync(
                        deliveryTag: args.DeliveryTag,
                        multiple: false,
                        requeue: true
                    );
                }
            };

            // Начинаем потребление
            await channel.BasicConsumeAsync(
                queue: queue,
                autoAck: false,
                consumer: consumer
            );
        }
    }
}
