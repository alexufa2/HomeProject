using CompanyContractsWebAPI.Models.RabbitMq;
using CompanyContractsWebAPI.Models.RabbitMq.Messages;
using Microsoft.Extensions.Options;
using RabbitMqCustomClient;

namespace CompanyContractsWebAPI.BusinessLogic
{
    public class RabbitMqListener: BackgroundService
    {
        private RabbitMQSettings _rabbitMQSettings;
        private IMessageProcessor _messageProcessor;

        public RabbitMqListener(IOptions<RabbitMQSettings> settings, IMessageProcessor messageProcessor)
        {
            _rabbitMQSettings = settings.Value;
            _messageProcessor = messageProcessor;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var createContractConsumer = 
                new RabbitMqConsumer<ContractCreated>(
                    _rabbitMQSettings.Host,
                    _rabbitMQSettings.VirtualHost,
                    _rabbitMQSettings.Port,
                    _rabbitMQSettings.ContarctConsumer.User.Name,
                    _rabbitMQSettings.ContarctConsumer.User.Pass
                    );

            createContractConsumer.StartConsumerAsync(
                _rabbitMQSettings.ContarctConsumer.CreatedQueue,
                _messageProcessor.ProcessContractCreated
                );

            var updateContractConsumer =
               new RabbitMqConsumer<ContractUpdated>(
                   _rabbitMQSettings.Host,
                   _rabbitMQSettings.VirtualHost,
                   _rabbitMQSettings.Port,
                   _rabbitMQSettings.ContarctConsumer.User.Name,
                   _rabbitMQSettings.ContarctConsumer.User.Pass
                   );

            updateContractConsumer.StartConsumerAsync(
                _rabbitMQSettings.ContarctConsumer.UpdatedQueue,
                _messageProcessor.ProcessContractUpdated
                );

            var createContractDoneConsumer =
               new RabbitMqConsumer<ContractDoneCreated>(
                   _rabbitMQSettings.Host,
                   _rabbitMQSettings.VirtualHost,
                   _rabbitMQSettings.Port,
                   _rabbitMQSettings.ContarctDoneConsumer.User.Name,
                   _rabbitMQSettings.ContarctDoneConsumer.User.Pass
                   );

            createContractDoneConsumer.StartConsumerAsync(
                _rabbitMQSettings.ContarctDoneConsumer.CreatedQueue,
                _messageProcessor.ProcessContractDoneCreated
                );

            var updateContractDoneConsumer =
               new RabbitMqConsumer<ContractDoneUpdated>(
                   _rabbitMQSettings.Host,
                   _rabbitMQSettings.VirtualHost,
                   _rabbitMQSettings.Port,
                   _rabbitMQSettings.ContarctDoneConsumer.User.Name,
                   _rabbitMQSettings.ContarctDoneConsumer.User.Pass
                   );

            updateContractDoneConsumer.StartConsumerAsync(
                _rabbitMQSettings.ContarctDoneConsumer.UpdatedQueue,
                _messageProcessor.ProcessContractDoneUpdated
                );

            return Task.CompletedTask;
        }
    }
}
