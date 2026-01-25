using CompanyContractsWebAPI.Models.RabbitMq.Messages;

namespace CompanyContractsWebAPI.BusinessLogic
{
    public interface IMessageProcessor
    {
        void ProcessContractCreated(ContractCreated message);
        void ProcessContractUpdated(ContractUpdated message);

        void ProcessContractDoneCreated(ContractDoneCreated message);
        void ProcessContractDoneUpdated(ContractDoneUpdated message);

    }
}
