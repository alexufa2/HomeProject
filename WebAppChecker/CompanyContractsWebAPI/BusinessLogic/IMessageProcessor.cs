using CompanyContractsWebAPI.Models.RabbitMq.Messages;

namespace CompanyContractsWebAPI.BusinessLogic
{
    public interface IMessageProcessor
    {
        void ProcessContractCreated(ContractCreated message);
    }
}
