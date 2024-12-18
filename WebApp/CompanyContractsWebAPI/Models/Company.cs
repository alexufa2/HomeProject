namespace CompanyContractsWebAPI.Models
{
    public class Company : CreateCompany, IEntityWithId
    {
        public int Id { get; set; }
    }
}
