namespace CompanyContractsWebAPI.Models
{
    public interface IEntityWithId
    {
        public int Id { get; set; }

        public Guid IntegrationId { get; set; }
    }
}
