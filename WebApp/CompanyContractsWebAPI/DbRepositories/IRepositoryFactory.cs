namespace CompanyContractsWebAPI.DbRepositories
{
    public interface IRepositoryFactory
    {
        T CreateRepository<T>();
    }
}
