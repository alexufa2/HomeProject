using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class CompanyGoodsRepository : BaseRepository<CompanyGoods>
    {
        public CompanyGoodsRepository(ApplicationContext applicationContext) :
            base( applicationContext)
        { }
    }
}
