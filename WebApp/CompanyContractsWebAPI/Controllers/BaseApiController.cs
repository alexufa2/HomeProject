using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContractsWebAPI.Controllers
{
    [ApiController]
    public abstract class BaseApiController<T, U> : ControllerBase
        where T : class, U, IEntityWithId, new()
        where U : class

    {
        protected IRepository<T> _repository;

        public BaseApiController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("[controller]/Create")]
        public virtual ApiResultWithItem<T> Create(U item)
        {
            try
            {
                T dbItem = GetCopyedFields(item);
                T result = _repository.Create(dbItem);

                return new ApiResultWithItem<T>
                {
                    ErrorMassage = string.Empty,
                    ResultItem = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResultWithItem<T>
                {
                    ErrorMassage = ex.Message,
                    ResultItem = null
                };
            }
        }

        [HttpGet, Route("[controller]/GetById")]
        public virtual ApiResultWithItem<T> GetById(int id)
        {
            try
            {
                T result = _repository.GetById(id);

                return new ApiResultWithItem<T>
                {
                    ErrorMassage = string.Empty,
                    ResultItem = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResultWithItem<T>
                {
                    ErrorMassage = ex.Message,
                    ResultItem = null
                };
            }
        }

        [HttpGet, Route("[controller]/GetAll")]
        public virtual ApiResultWithItem<IEnumerable<T>> GetAll()
        {
            try
            {
                var result = _repository.GetAll();

                return new ApiResultWithItem<IEnumerable<T>>
                {
                    ErrorMassage = string.Empty,
                    ResultItem = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResultWithItem<IEnumerable<T>>
                {
                    ErrorMassage = ex.Message,
                    ResultItem = null
                };
            }
        }

        [HttpPut, Route("[controller]/Update")]
        public virtual ApiResultWithItem<T> Update(T item)
        {
            try
            {
                var result = _repository.Update(item);

                return new ApiResultWithItem<T>
                {
                    ErrorMassage = string.Empty,
                    ResultItem = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResultWithItem<T>
                {
                    ErrorMassage = ex.Message,
                    ResultItem = null
                };
            }
        }

        [HttpDelete, Route("[controller]/Delete")]
        public virtual ApiResult Delete(int id)
        {
            try
            {
                var result = _repository.Delete(id);
                return new ApiResult { ErrorMassage = string.Empty };
            }
            catch (Exception ex)
            {
                return new ApiResult { ErrorMassage = ex.Message };
            }
        }

        protected T GetCopyedFields(U item)
        {
            T result = new T();
            var uType = item.GetType();
            var tType = result.GetType();

            foreach (var f in tType.GetFields())
            {
                var rField = uType.GetField(f.Name);
                if (rField == null || rField.IsLiteral)
                    continue;

                rField.SetValue(result, f.GetValue(item));
            }

            foreach (var f in uType.GetProperties())
            {
                var rProp = tType.GetProperty(f.Name);
                if (rProp == null)
                    continue;

                rProp.SetValue(result, f.GetValue(item, null), null);
            }

            return result;
        }
    }
}
