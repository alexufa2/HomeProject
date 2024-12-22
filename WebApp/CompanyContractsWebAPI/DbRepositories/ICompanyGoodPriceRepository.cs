using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.DbRepositories
{
    public interface ICompanyGoodPriceRepository
    {
        CompanyGoodPrice Create(CompanyGoodPrice item);
        CompanyGoodPrice Update(CompanyGoodPrice item);

        IEnumerable<CompanyGoodPrice> GetAll();
        bool Delete(CompanyGoodPrice item);
    }
}
