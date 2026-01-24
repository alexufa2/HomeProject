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
    public class RabbitMqWorker
    {
        private RabbitMQSettings _rabbitMQSettings;
        private IContractRepository _contractRepository;
        private IContractDoneRepository _contractDoneRepository;
        private RabbitMqSender<ContractCreated> _contractCreatedSender;
        

        public RabbitMqWorker(
            IOptions<RabbitMQSettings> settings, 
            IContractRepository contractRepository, 
            IContractDoneRepository contractDoneRepository)
        {
            _rabbitMQSettings = settings.Value;
            _contractRepository = contractRepository;
            _contractDoneRepository = contractDoneRepository;

            _contractCreatedSender =
                new RabbitMqSender<ContractCreated>(
                    _rabbitMQSettings.Host,
                    _rabbitMQSettings.VirtualHost,
                    _rabbitMQSettings.Port,
                    _rabbitMQSettings.ContarctCreateSender.User.Name,
                    _rabbitMQSettings.ContarctCreateSender.User.Pass);
        }

        public async Task SendContractCreated(Contract createdContract)
        {
            var itemForConvert = _contractRepository.GetById(createdContract.Id);
            var integrationMsg = Helper.Convert<ContractCreated, ContractWithNames>(itemForConvert);
            integrationMsg.StatusName = ContractStatuses.GetStatusName(itemForConvert.Status);

            var senderSettings = _rabbitMQSettings.ContarctCreateSender;
            await _contractCreatedSender.SendMessage(integrationMsg, senderSettings.ExhangeName, senderSettings.RoutingKey);
        }
    }
}
