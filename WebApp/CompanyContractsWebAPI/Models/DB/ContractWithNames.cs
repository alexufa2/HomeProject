namespace CompanyContractsWebAPI.Models.DB
{

    public class ContractWithNames : IEntityWithId
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int Company_Id { get; set; }
        public string Company_Name { get; set; }

        public int Good_Id { get; set; }
        public string Good_Name { get; set; }

        public int Good_Count { get; set; }
        public decimal Total_Sum { get; set; }

        public decimal Done_Sum { get; set; }

        public string Status { get; set; }

        public Guid IntegrationId { get; set; }
    }
}
