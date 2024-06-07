using RulesEngine.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RulesEnginePOC.Models
{
    [Table("Rule")]
    public class Rule
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int EntitlementId { get; set; }
        public Criteria Criteria { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public RuleActions? Actions { get; set; }

        public IEnumerable<Rule>? ChildRules { get; set; }
        public Entitlement? Entitlement { get; set; }
    }
}
