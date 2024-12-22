using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CompanyContractsWebAPI.Models.DB
{
    [Table("company_good_price")]
    [PrimaryKey(nameof(Company_Id), nameof(Good_Id))]
    public class CompanyGoodPrice
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Company")]
        public int Company_Id { get; set; }


        [Key]
        [Column(Order = 2)]
        [ForeignKey("Good")]
        public int Good_Id { get; set; }

        public virtual Company Company { get; set; }
        public virtual Good Good { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}
