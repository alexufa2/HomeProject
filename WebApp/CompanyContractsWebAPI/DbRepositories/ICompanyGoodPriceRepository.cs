using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.DbRepositories
{
    public interface ICompanyGoodPriceRepository
    {
        CompanyGoodPrice Create(CompanyGoodPrice item);
        CompanyGoodPrice Update(CompanyGoodPrice item);
        IEnumerable<CompanyGoodPriceWitNames> GetAll();
        IEnumerable<CompanyGoodPriceWitNames> GetByCompanyId(int companyId);
        IEnumerable<Good> GetNotExistsByCompanyId(int companyId);
        IEnumerable<CompanyGoodPriceWitNames> GetByGoodId(int goodId);

        bool Delete(int companyId, int goodId);


    }
}
