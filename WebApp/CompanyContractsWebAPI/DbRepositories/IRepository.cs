namespace CompanyContractsWebAPI.DbRepositories
{
    public interface IRepository<T> where T : class
    {
        int Create(T item);
        
        T GetById(int id);
        
        IEnumerable<T> GetAll();

        bool Update(T item);

        bool Delete(int id);
    }
}
