using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyContractsWebAPI.Models.DB
{
    [Table("contract")]
    public class Contract : IEntityWithId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Number { get; set; }

        [Column(Order = 1)]
        [ForeignKey("Company")]
        public int Company_Id { get; set; }
        public Company Company { get; set; }

        [Column(Order = 2)]
        [ForeignKey("Good")]
        public int Good_Id { get; set; }
        public Good Good { get; set; }

        public int Good_Count { get; set; }
        
        [Column(TypeName = "money")]
        public decimal Total_Sum { get; set; }

        [Column(TypeName = "money")]
        public decimal Done_Sum { get; set; }

        public string Status { get; set; }

        public Guid IntegrationId { get; set; }
    }
}
