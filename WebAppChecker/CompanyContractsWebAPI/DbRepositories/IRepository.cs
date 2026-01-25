using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.DbRepositories
{
    public interface IRepository<T> where T : class, IEntityWithId, new()
    {
        T Create(T item);
        
        T GetById(int id);

        T GetByIntegrationId(Guid integrationId);

        IEnumerable<T> GetAll();

        T Update(T item);

        bool Delete(int id);
    }
}
