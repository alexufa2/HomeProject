namespace CompanyContractsWebAPI.Models
{
    public class CompanyPurpose: CreateCompanyPurpose, IEntityWithId
    {
        public int Id { get; set; }
    }
}
