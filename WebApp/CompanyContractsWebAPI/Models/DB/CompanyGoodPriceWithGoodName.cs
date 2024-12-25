namespace CompanyContractsWebAPI.Models.DB
{
    public class CompanyGoodPriceWitNames
    {
        public int Company_Id { get; set; }

        public string Company_Name { get; set; }
        public int Good_Id { get; set; }
        public string Good_Name{ get; set; }
        public decimal Price { get; set; }
    }
}
