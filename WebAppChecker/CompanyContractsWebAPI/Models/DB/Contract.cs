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

        public string Company_Name { get; set; }

        public string Good_Name { get; set; }

        public int Good_Count { get; set; }

        public decimal Total_Sum { get; set; }

        public decimal Done_Sum { get; set; }

        public string StatusName { get; set; }

        public Guid IntegrationId { get; set; }
    }
}
