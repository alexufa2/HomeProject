namespace CompanyContractsWebAPI.Models.DTO
{
    public class CreateContractDto
    {
        public string Number { get; set; }

        public int Company_Id { get; set; }

        public int Good_Id { get; set; }

        public int Good_Count { get; set; }

        public decimal Total_Sum { get; set; }
    }
}
