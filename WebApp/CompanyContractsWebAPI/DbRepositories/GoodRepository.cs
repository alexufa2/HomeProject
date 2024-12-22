using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class GoodRepository : BaseRepository<Good>
    {
        public GoodRepository(ApplicationContext applicationContext) :
            base(applicationContext)
        { }
    }
}
