namespace CompanyContractsWebAPI.Models.RabbitMq.Messages
{
    public class ContractUpdated: IIntegrationEntity
    {
        public decimal Done_Sum { get; set; }

        public string StatusName { get; set; }

        public Guid IntegrationId { get; set; }
    }
}
