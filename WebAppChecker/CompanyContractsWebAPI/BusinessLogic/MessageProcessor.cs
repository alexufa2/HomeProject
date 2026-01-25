using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Helpers;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.RabbitMq.Messages;

namespace CompanyContractsWebAPI.BusinessLogic
{
    public class MessageProcessor : IMessageProcessor
    {
        IRepositoryFactory _repositoryFactory;

        public MessageProcessor(IRepositoryFactory repositoryFactory) { 
            _repositoryFactory = repositoryFactory;
        }

        public void ProcessContractCreated(ContractCreated message)
        {
            var contract = Helper.ConvertToContract(message);
            var repository = _repositoryFactory.CreateRepository<IRepository<Contract>>();
            repository.Create(contract);
        }
    }
}
