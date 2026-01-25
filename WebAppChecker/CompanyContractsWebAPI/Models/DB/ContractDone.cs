using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyContractsWebAPI.Models.DB
{
    [Table("contract_done")]
    public class ContractDone : IEntityWithId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(Order = 1)]
        [ForeignKey("Contract")]
        public int Contract_Id { get; set; }

        public Contract Contract { get; set; }

        [Column(TypeName = "money")]
        public decimal Done_Amount { get; set; }

        public Guid IntegrationId { get; set; }

        public string StatusName { get; set; }
    }
}
