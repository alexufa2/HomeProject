using CompanyContractsWebAPI.Models.DB;
using System.ComponentModel.Design;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class CompanyGoodPriceRepository : ICompanyGoodPriceRepository
    {
        protected ApplicationContext _applicationContext { get; set; }

        public CompanyGoodPriceRepository(ApplicationContext applicationContext)
        {
            if (applicationContext == null)
                throw new ArgumentNullException(nameof(applicationContext));

            _applicationContext = applicationContext;
        }

        public CompanyGoodPrice Create(CompanyGoodPrice item)
        {
            _applicationContext.CompanyGoodPrices.Add(item);
            _applicationContext.SaveChanges();
            return item;
        }

        public CompanyGoodPrice Update(CompanyGoodPrice item)
        {
            var currentItem =
                _applicationContext.CompanyGoodPrices
                .FirstOrDefault(f => f.Company_Id == item.Company_Id && f.Good_Id == item.Good_Id);

            if (currentItem == null)
                return null;

            currentItem.Price = item.Price;

            _applicationContext.SaveChanges();
            return currentItem;
        }

        public bool Delete(int companyId, int goodId)
        {
            var currentItem =
                _applicationContext.CompanyGoodPrices
                .FirstOrDefault(f => f.Company_Id == companyId && f.Good_Id == goodId);

            if (currentItem == null)
                return false;

            _applicationContext.CompanyGoodPrices.Remove(currentItem);
            _applicationContext.SaveChanges();
            return true;
        }

        public IEnumerable<CompanyGoodPrice> GetAll()
        {
            return _applicationContext.CompanyGoodPrices;
        }

        public IEnumerable<CompanyGoodPrice> GetByCompanyId(int companyId)
        {
            return _applicationContext.CompanyGoodPrices.Where(w => w.Company_Id == companyId);
        }

        public IEnumerable<CompanyGoodPrice> GetByGoodId(int goodId)
        {
            return _applicationContext.CompanyGoodPrices.Where(w => w.Good_Id == goodId);
        }
    }
}
