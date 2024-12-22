using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.Models.DTO
{
    public class CompanyDto : IEntityWithId
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Inn { get; set; }
        public string Address { get; set; }
    }
}
