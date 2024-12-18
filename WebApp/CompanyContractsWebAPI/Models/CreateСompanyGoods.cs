namespace CompanyContractsWebAPI.Models
{
    public class CreateCompanyGoods
    {
        public int Company_Id { get; set; }
        public int Good_Id { get; set; }
        public decimal Price { get; set; }
    }
}
