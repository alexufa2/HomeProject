namespace RabbitMqCustomClient
{
    using RabbitMQ.Client;
    using System.Text;

    public class RabbitMQSender
    {
        private string _hostName;
        private int _port;
        private string _userName;
        private string _password;

        public RabbitMQSender(string hostName, int port, string userName, string password)
        {
            _hostName = hostName;
            _port = port;
            _userName = userName;
            _password = password;
        }

        public async Task SendMessage<T>(T message)
        {
            // Создаем фабрику подключения
            var factory = new ConnectionFactory()
            {
                HostName = _hostName, 
                Port = _port,            
                UserName = _userName,     
                Password = _password      
            };

            // Устанавливаем соединение
            using (var connection = await factory.CreateConnectionAsync())
            {
                using (var channel = await connection.CreateChannelAsync())
                {
                    // Преобразуем сообщение в байты
                    var body = ObjectToBytesConverter.ObjectToBytes(message);

                    // Публикуем сообщение
                    await channel.BasicPublishAsync(
                        "",        // используем exchange по умолчанию
                        "hello", // ключ маршрутизации (имя очереди)
                        true,
                        body
                    );
                }
            }
        }
    }
}
