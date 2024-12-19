using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.Controllers
{
    public class CompanyGoodsController : BaseApiController<CompanyGoods>
    {
        public CompanyGoodsController(IRepository<CompanyGoods> repository) :
            base(repository)
        { }
    }
}
