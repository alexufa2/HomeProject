namespace CompanyContractsWebAPI.Models
{
    public class CompanyGoods : IEntityWithId
    {
        public int Id { get; set; }

        public int Company_Id { get; set; }
        public int Good_Id { get; set; }
        public decimal Price { get; set; }
    }
}
