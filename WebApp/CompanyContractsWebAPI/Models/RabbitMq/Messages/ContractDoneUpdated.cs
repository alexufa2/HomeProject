namespace CompanyContractsWebAPI.Models.RabbitMq.Messages
{
    public class ContractDoneUpdated: IIntegrationEntity
    {
        public decimal Done_Amount { get; set; }

        public Guid IntegrationId { get; set; }

        public string StatusName { get; set; }
    }
}
