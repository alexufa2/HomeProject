using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.DbRepositories
{
    public interface ICompanyGoodPriceRepository
    {
        CompanyGoodPrice Create(CompanyGoodPrice item);
        CompanyGoodPrice Update(CompanyGoodPrice item);
        IEnumerable<CompanyGoodPrice> GetAll();
        IEnumerable<CompanyGoodPrice> GetByCompanyId(int companyId);
        IEnumerable<CompanyGoodPrice> GetByGoodId(int goodId);

        bool Delete(int companyId, int goodId);
    }
}
