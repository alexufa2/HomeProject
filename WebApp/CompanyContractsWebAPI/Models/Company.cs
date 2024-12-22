using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyContractsWebAPI.Models
{
    [Table("company")]
    public class Company : IEntityWithId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Inn { get; set; }
        public string Address { get; set; }
        public int Purpose_Id { get; set; }
    }
}
