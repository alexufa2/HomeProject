using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.Models.DTO
{
    public class GoodDto : IEntityWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Measurement_Unit { get; set; }
    }
}
