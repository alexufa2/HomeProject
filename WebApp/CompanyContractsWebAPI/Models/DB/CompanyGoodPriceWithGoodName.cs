namespace CompanyContractsWebAPI.Models.DB
{
    public class CompanyGoodPriceWithGoodName
    {
        public int Company_Id { get; set; }
        public int Good_Id { get; set; }
        public string GoodName{ get; set; }
        public decimal Price { get; set; }
    }
}
