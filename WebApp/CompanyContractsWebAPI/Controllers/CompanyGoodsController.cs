using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.Controllers
{
    public class CompanyGoodsController : BaseApiController<CompanyGoods, CreateCompanyGoods>
    {
        public CompanyGoodsController(IRepository<CompanyGoods> repository) :
            base(repository)
        { }
    }
}
