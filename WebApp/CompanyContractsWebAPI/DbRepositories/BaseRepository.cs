using CompanyContractsWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyContractsWebAPI.DbRepositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntityWithId, new()
    {
        protected ApplicationContext _applicationContext { get; set; }

        public BaseRepository(ApplicationContext applicationContext)
        {
            if (applicationContext == null)
                throw new ArgumentNullException(nameof(applicationContext));

            _applicationContext = applicationContext;
        }

        public virtual T Create(T item)
        {
            var dbSet = _applicationContext.Set<T>();
            dbSet.Add(item);
            _applicationContext.SaveChanges();
            return item;
        }

        public virtual T GetById(int id)
        {
            return _applicationContext.Set<T>().FirstOrDefault(f => f.Id == id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _applicationContext.Set<T>();
        }

        public virtual T Update(T item)
        {
            T originalItem = _applicationContext.Set<T>().FirstOrDefault(f => f.Id == item.Id);
            if (originalItem == null)
                return null;

            CopyFields(item, originalItem);
            _applicationContext.SaveChanges();
            return originalItem;
        }

        public virtual bool Delete(int id)
        {
            var dbSet = _applicationContext.Set<T>();
            T originalItem = dbSet.FirstOrDefault(f => f.Id == id);
            if (originalItem == null)
                return false;

            dbSet.Remove(originalItem);
            _applicationContext.SaveChanges();
            return true;
        }

        protected void CopyFields(T sourceItem, T destItem)
        {
            if (sourceItem == null || destItem == null)
                return;

            var type = typeof(T);

            foreach (var field in type.GetFields())
            {
                field.SetValue(destItem, field.GetValue(sourceItem));
            }

            foreach (var prop in type.GetProperties())
            {
                prop.SetValue(destItem, prop.GetValue(sourceItem, null), null);
            }
        }
    }
}
