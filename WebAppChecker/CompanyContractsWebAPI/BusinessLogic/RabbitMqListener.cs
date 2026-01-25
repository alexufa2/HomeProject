using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Helpers;
using CompanyContractsWebAPI.Models;
using CompanyContractsWebAPI.Models.DB;
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
                    _rabbitMQSettings.ContarctCreatedQueue.User.Name,
                    _rabbitMQSettings.ContarctCreatedQueue.User.Pass
                    );

            createContractConsumer.StartConsumerAsync(
                _rabbitMQSettings.ContarctCreatedQueue.Name,
                _messageProcessor.ProcessContractCreated
                );

            return Task.CompletedTask;
        }
       
    }
}
