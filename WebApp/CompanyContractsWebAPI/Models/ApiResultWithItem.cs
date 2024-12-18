namespace CompanyContractsWebAPI.Models
{
    public class ApiResultWithItem<T> : ApiResult
    {
        public T ResultItem { get; set; }
    }
}
