using Microsoft.EntityFrameworkCore;
using RulesEnginePOC.Models;
using RulesEnginePOC.Service.Interfaces;

namespace RulesEnginePOC.Service
{
    public class RuleService : IRuleService
    {
        private readonly ApplicationDbContext _dbContext;

        public RuleService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Rule> CreateRule(Rule rule)
        {
            rule.CreatedDate = DateTime.UtcNow;
            rule.UpdatedDate = DateTime.UtcNow;
            _dbContext.Rules.Add(rule);
            await _dbContext.SaveChangesAsync();
            return rule;
        }

        public async Task DeleteRule(int id)
        {
            var rule = await _dbContext.Rules.FindAsync(id);
            if (rule != null)
            {
                _dbContext.Rules.Remove(rule);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Rule> GetRule(int id) => 
            await _dbContext.Rules
            .Include(r => r.ChildRules)
            .FirstAsync(r => r.Id == id);

        public async Task<IEnumerable<Rule>> GetAllRules() =>
            await _dbContext.Rules
                .Include(r => r.ChildRules)
                .ToListAsync();

        public async Task<Rule> UpdateRule(Rule rule)
        {
            var existingRule = await _dbContext.Rules.FindAsync(rule.Id);
            if (existingRule != null)
            {
                existingRule.RuleName = rule.RuleName;
                existingRule.Criteria = rule.Criteria;
                existingRule.IsActive = rule.IsActive;
                existingRule.UpdatedDate = DateTime.UtcNow;
                existingRule.ChildRules = rule.ChildRules;

                await _dbContext.SaveChangesAsync();
            }

            return existingRule;
        }
    }

}
