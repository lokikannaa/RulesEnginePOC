namespace RulesEnginePOC
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using Microsoft.Extensions.Hosting;
    using RulesEngine.Models;
    using RulesEnginePOC.Models;
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Rule = RulesEnginePOC.Models.Rule;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<UserRule> UserRules { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("Host=localhost;Port=5432;Database=entitlements_db;Username=myuser;Password=mypassword;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var serializerOptions = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
            };

            modelBuilder.Entity<Rule>().HasOne<Rule>().WithMany(r => r.ChildRules).HasForeignKey("RuleNameFK");
            modelBuilder.Entity<Rule>()
                .HasOne(r => r.Entitlement);
            modelBuilder.Entity<Rule>()
                .Property(r => r.Criteria).HasColumnName("criteria")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, serializerOptions),
                    v => JsonSerializer.Deserialize<Criteria>(v, serializerOptions)!
                );
            modelBuilder.Entity<Rule>()
                .Property(r => r.Actions).HasColumnName("actions")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, serializerOptions),
                    v => JsonSerializer.Deserialize<RuleActions>(v, serializerOptions)!
                );

            modelBuilder.Entity<UserRule>().HasKey(ur => new { ur.UserId, ur.RuleId });

            modelBuilder.Entity<UserRule>()
                .HasOne(ur => ur.User);

            modelBuilder.Entity<UserRule>()
                .HasOne(ur => ur.Rule);
        }
    }

}
