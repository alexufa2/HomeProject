using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.Models.DTO
{
    public class CompanyGoodPriceDto
    {
        public int Company_Id { get; set; }

        public int Good_Id { get; set; }

        public decimal Price { get; set; }
    }
}
