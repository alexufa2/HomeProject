namespace CompanyContractsWebAPI.Models.DTO
{
    public class ContractDoneDto : IEntityWithId
    {
        public int Id { get; set; }

        public int Contract_Id { get; set; }

        public decimal Done_Amount { get; set; }

        public Guid IntegrationId { get; set; }
    }
}
