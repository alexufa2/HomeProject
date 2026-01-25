namespace CompanyContractsWebAPI.Models.RabbitMq.Messages
{
    public class ContractCreated: IIntegrationEntity
    {
        public string Number { get; set; }

        public string Company_Name { get; set; }

        public string Good_Name { get; set; }

        public int Good_Count { get; set; }

        public decimal Total_Sum { get; set; }

        public string StatusName { get; set; }

        public Guid IntegrationId { get; set; }
    }
}
