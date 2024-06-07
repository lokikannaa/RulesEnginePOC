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
        [Column("id")]
        public int Id { get; set; }
        [Column("rule_name")]
        public string Name { get; set; }
        [Column("entitlement_id")]
        public int EntitlementId { get; set; }
        [Column("criteria", TypeName = "jsonb")]
        public Criteria Criteria { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        public IEnumerable<Rule>? ChildRules { get; set; }

        [Column("actions", TypeName = "jsonb")]
        public RuleActions? Actions { get; set; }
        public Entitlement? Entitlement { get; set; }
    }
}
