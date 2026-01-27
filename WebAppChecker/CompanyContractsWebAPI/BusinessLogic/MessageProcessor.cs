using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Helpers;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.RabbitMq.Messages;

namespace CompanyContractsWebAPI.BusinessLogic
{
    public class MessageProcessor : IMessageProcessor
    {
        IRepositoryFactory _repositoryFactory;
        ContractsHub _contractsHub;

        public MessageProcessor(IRepositoryFactory repositoryFactory, ContractsHub contractsHub)
        {
            _repositoryFactory = repositoryFactory;
            _contractsHub = contractsHub;
        }

        public async Task ProcessContractCreated(ContractCreated message)
        {
            var contract = Helper.ConvertToContract(message);
            var repository = _repositoryFactory.CreateRepository<IRepository<Contract>>();
            repository.Create(contract);

            await _contractsHub.SendReloadContracts();
        }

        public async Task ProcessContractUpdated(ContractUpdated message)
        {
            var repository = _repositoryFactory.CreateRepository<IRepository<Contract>>();
            var contract = repository.GetByIntegrationId(message.IntegrationId);

            if (contract != null)
            {
                contract.StatusName = message.StatusName;
                contract.Done_Sum = message.Done_Sum;

                repository.Update(contract);

                await _contractsHub.SendReloadContracts();
            }


        }

        public async Task ProcessContractDoneCreated(ContractDoneCreated message)
        {
            var contractDone = Helper.ConvertToContractDone(message);
            var contractRepository = _repositoryFactory.CreateRepository<IRepository<Contract>>();
            var contract = contractRepository.GetByIntegrationId(message.Contract_IntegrationId);

            if (contract != null)
            {
                contractDone.Contract_Id = contract.Id;
                var repository = _repositoryFactory.CreateRepository<IContractDoneRepository>();
                repository.Create(contractDone);

                await _contractsHub.SendReloadContractDoneForContract(contract.Id);
            }
        }

        public async Task ProcessContractDoneUpdated(ContractDoneUpdated message)
        {
            var repository = _repositoryFactory.CreateRepository<IContractDoneRepository>();
            var contractDone = repository.GetByIntegrationId(message.IntegrationId);

            if (contractDone != null)
            {
                contractDone.StatusName = message.StatusName;
                contractDone.Done_Amount = message.Done_Amount;

                repository.Update(contractDone);
                
                await _contractsHub.SendReloadContractDoneForContract(contractDone.Contract_Id);
            }
        }
    }
}
