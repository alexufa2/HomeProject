namespace CompanyContractsWebAPI.Models
{
    public class ApiResult
    {
        public bool IsSuccess { get { return string.IsNullOrEmpty(ErrorMassage); } }
        public string ErrorMassage { get; set; }
    }
}
