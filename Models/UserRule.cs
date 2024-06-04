using System.ComponentModel.DataAnnotations.Schema;

namespace RulesEnginePOC.Models
{
    [Table("UserRule")]
    public class UserRule
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("rule_id")]
        public int RuleId { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public Rule Rule { get; set; }
    }
}
