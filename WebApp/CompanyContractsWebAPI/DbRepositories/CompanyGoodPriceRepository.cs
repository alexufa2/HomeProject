using CompanyContractsWebAPI.Models;

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

        public bool Delete(CompanyGoodPrice item)
        {
            var currentItem =
                _applicationContext.CompanyGoodPrices
                .FirstOrDefault(f => f.Company_Id == item.Company_Id && f.Good_Id == item.Good_Id);

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
    }
}
