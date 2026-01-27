using CompanyContractsWebAPI.Models.RabbitMq.Messages;

namespace CompanyContractsWebAPI.BusinessLogic
{
    public interface IMessageProcessor
    {
        Task ProcessContractCreated(ContractCreated message);
        Task ProcessContractUpdated(ContractUpdated message);

        Task ProcessContractDoneCreated(ContractDoneCreated message);
        Task ProcessContractDoneUpdated(ContractDoneUpdated message);

    }
}
