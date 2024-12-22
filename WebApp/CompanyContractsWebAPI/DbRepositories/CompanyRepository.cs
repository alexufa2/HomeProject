using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class CompanyRepository : BaseRepository<Company>
    {
        public CompanyRepository(ApplicationContext applicationContext) :
            base(applicationContext)
        { }
    }
}
