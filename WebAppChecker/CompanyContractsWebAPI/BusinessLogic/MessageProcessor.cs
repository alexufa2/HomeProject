using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Helpers;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.RabbitMq.Messages;

namespace CompanyContractsWebAPI.BusinessLogic
{
    public class MessageProcessor : IMessageProcessor
    {
        IRepositoryFactory _repositoryFactory;

        public MessageProcessor(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public void ProcessContractCreated(ContractCreated message)
        {
            var contract = Helper.ConvertToContract(message);
            var repository = _repositoryFactory.CreateRepository<IRepository<Contract>>();
            repository.Create(contract);
        }

        public void ProcessContractUpdated(ContractUpdated message)
        {
            var repository = _repositoryFactory.CreateRepository<IRepository<Contract>>();
            var contract = repository.GetByIntegrationId(message.IntegrationId);

            if (contract != null)
            {
                contract.StatusName = message.StatusName;
                contract.Done_Sum = message.Done_Sum;

                repository.Update(contract);
            }
        }

        public void ProcessContractDoneCreated(ContractDoneCreated message)
        {
            var contractDone = Helper.ConvertToContractDone(message);
            var repository = _repositoryFactory.CreateRepository<IRepository<ContractDone>>();
            repository.Create(contractDone);
        }

        public void ProcessContractDoneUpdated(ContractDoneUpdated message)
        {
            var repository = _repositoryFactory.CreateRepository<IRepository<ContractDone>>();
            var contractDone = repository.GetByIntegrationId(message.IntegrationId);

            if (contractDone != null)
            {
                contractDone.StatusName = message.StatusName;
                contractDone.Done_Amount = message.Done_Amount;

                repository.Update(contractDone);
            }
        }


    }
}
