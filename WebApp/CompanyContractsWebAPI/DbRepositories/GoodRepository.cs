using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class GoodRepository : BaseRepository<Good>
    {
        public GoodRepository(ApplicationContext applicationContext) :
            base(applicationContext)
        { }
    }
}
