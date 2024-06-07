using System.ComponentModel.DataAnnotations.Schema;

namespace RulesEnginePOC.Models
{
    [Table("UserRule")]
    public class UserRule
    {
        public int UserId { get; set; }

        public int RuleId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public Rule Rule { get; set; }
    }
}
