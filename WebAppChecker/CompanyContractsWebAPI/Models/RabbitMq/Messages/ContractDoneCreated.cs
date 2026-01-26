namespace CompanyContractsWebAPI.Models.RabbitMq.Messages
{
    public class ContractDoneCreated: IIntegrationEntity
    {
        public Guid Contract_IntegrationId { get; set; }

        public decimal Done_Amount { get; set; }

        public Guid IntegrationId { get; set; }

        public string StatusName { get; set; }
    }
}
