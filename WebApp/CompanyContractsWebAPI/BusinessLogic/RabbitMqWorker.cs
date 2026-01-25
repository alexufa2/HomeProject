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
        private RabbitMqSender _rabbitMqSender;

        public RabbitMqWorker(
            IOptions<RabbitMQSettings> settings,
            IRepositoryFactory repositoryFactory)
        {
            _rabbitMQSettings = settings.Value;
            _contractRepository = repositoryFactory.CreateRepository<IContractRepository>();
            _contractDoneRepository = repositoryFactory.CreateRepository<IContractDoneRepository>();

            var contractSender = _rabbitMQSettings.ContarctSender;

            _rabbitMqSender = new RabbitMqSender(
                    _rabbitMQSettings.Host,
                    _rabbitMQSettings.VirtualHost,
                    _rabbitMQSettings.Port,
                    contractSender.User.Name,
                    contractSender.User.Pass);

        }

        public async Task SendContractCreated(Contract createdContract)
        {
            var itemForConvert = _contractRepository.GetById(createdContract.Id);
            var integrationMsg = Helper.Convert<ContractCreated, ContractWithNames>(itemForConvert);
            integrationMsg.StatusName = ContractStatuses.GetStatusName(itemForConvert.Status);

            await _rabbitMqSender.SendMessage(
                integrationMsg,
                _rabbitMQSettings.ToCheckerExhange,
                _rabbitMQSettings.ContarctSender.CreateRoutingKey);
        }

        public async Task SendContractUpdated(Guid integrationId, string status, decimal doneSum)
        {
            var integrationMsg = new ContractUpdated
            {
                IntegrationId = integrationId,
                Done_Sum = doneSum,
                StatusName = status
            };

            await _rabbitMqSender.SendMessage(
                integrationMsg,
                _rabbitMQSettings.ToCheckerExhange,
                _rabbitMQSettings.ContarctSender.UpdatedRoutingKey);
        }


        public async Task SendContractDoneCreated(ContractDone createdContractDone)
        {
            var itemForConvert = _contractDoneRepository.GetById(createdContractDone.Id);
            var integrationMsg = Helper.Convert<ContractDoneCreated, ContractDone>(itemForConvert);
            integrationMsg.StatusName = "Добавлено";

            await _rabbitMqSender.SendMessage(
                 integrationMsg,
                 _rabbitMQSettings.ToCheckerExhange,
                 _rabbitMQSettings.ContarctDoneSender.CreateRoutingKey);

            var contractItem = _contractRepository.GetById(createdContractDone.Contract_Id);

            if (contractItem != null)
                await SendContractUpdated(
                    contractItem.IntegrationId,
                    ContractStatuses.GetStatusName(contractItem.Status),
                    contractItem.Done_Sum);
        }

        public async Task SendContractDoneUpdated(Guid integrationId, int contractId, decimal doneAmount, string status)
        {
            var integrationMsg = new ContractDoneUpdated
            {
                Done_Amount = doneAmount,
                StatusName = status,
                IntegrationId = integrationId
            };

            await _rabbitMqSender.SendMessage(
                 integrationMsg,
                 _rabbitMQSettings.ToCheckerExhange,
                 _rabbitMQSettings.ContarctDoneSender.UpdatedRoutingKey);


            var contractItem = _contractRepository.GetById(contractId);

            if (contractItem != null)
                await SendContractUpdated(
                    contractItem.IntegrationId,
                    ContractStatuses.GetStatusName(contractItem.Status),
                    contractItem.Done_Sum);
        }

    }
}
