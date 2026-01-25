namespace CompanyContractsWebAPI.Models.RabbitMq.Messages
{
    public class ContractDoneCreated: IIntegrationEntity
    {
        public int Contract_Id { get; set; }

        public decimal Done_Amount { get; set; }

        public Guid IntegrationId { get; set; }

        public string StatusName { get; set; }
    }
}
