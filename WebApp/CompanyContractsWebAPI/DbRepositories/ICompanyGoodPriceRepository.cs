using CompanyContractsWebAPI.Models.DB;

namespace CompanyContractsWebAPI.DbRepositories
{
    public interface ICompanyGoodPriceRepository
    {
        CompanyGoodPrice Create(CompanyGoodPrice item);
        CompanyGoodPrice Update(CompanyGoodPrice item);
        IEnumerable<CompanyGoodPriceWithGoodName> GetAll();
        IEnumerable<CompanyGoodPriceWithGoodName> GetByCompanyId(int companyId);
        IEnumerable<Good> GetNotExistsByCompanyId(int companyId);
        IEnumerable<CompanyGoodPriceWithGoodName> GetByGoodId(int goodId);

        bool Delete(int companyId, int goodId);


    }
}
