using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RulesEnginePOC.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("api_key")]
        public string ApiKey { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}

