namespace CompanyContractsWebAPI.Models.DTO
{
    public class CreateCompanyGoodPriceDto
    {
        public int Company_Id { get; set; }
        public int Good_Id { get; set; }
        public decimal Price { get; set; }
    }
}
