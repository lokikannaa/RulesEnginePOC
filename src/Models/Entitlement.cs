using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RulesEnginePOC.Models
{
    [Table("Entitlement")]
    public class Entitlement
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
