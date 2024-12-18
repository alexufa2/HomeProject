namespace CompanyContractsWebAPI.Models
{
    public class CreateCompany
    {
        public string Name { get; set; }
        public string Inn { get; set; }
        public string Address { get; set; }
        public int Purpose_Id { get; set; }
    }
}
