namespace CompanyContractsWebAPI.Models
{
    public class CompanyGoods : CreateCompanyGoods, IEntityWithId
    {
        public int Id { get; set; }
    }
}
