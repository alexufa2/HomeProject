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

        public IEnumerable<CompanyGoodPriceWitNames> GetAll()
        {
            return _applicationContext.CompanyGoodPrices
                .Join(
                    _applicationContext.Goods,
                   cgp => cgp.Good_Id,
                   good => good.Id,
                   (cgp, good) => new { cgp, good }
                )
               .Join(
                    _applicationContext.Companyes,
                    combo => combo.cgp.Company_Id,
                    c =>c.Id,
                    (combo, c)=> new CompanyGoodPriceWitNames
                    {
                        Company_Id = combo.cgp.Company_Id,
                        Company_Name = c.Name,
                        Good_Id= combo.cgp.Good_Id,
                        Good_Name= combo.good.Name,
                        Price = combo.cgp.Price
                    }); 
        }

        public IEnumerable<CompanyGoodPriceWitNames> GetByCompanyId(int companyId)
        {
            return GetAll().Where(w => w.Company_Id == companyId);
        }

        public IEnumerable<CompanyGoodPriceWitNames> GetByGoodId(int goodId)
        {
            return GetAll().Where(w => w.Good_Id == goodId);
        }

        public IEnumerable<Good> GetNotExistsByCompanyId(int companyId)
        {
            int[] existsCompanyGoods =
                _applicationContext.CompanyGoodPrices
                .Where(w=>w.Company_Id == companyId)
                .Select(s => s.Good_Id)
                .ToArray();

            return _applicationContext.Goods.Where(w => !existsCompanyGoods.Contains(w.Id));
        }
    }
}
