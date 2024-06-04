using Microsoft.EntityFrameworkCore;
using RulesEnginePOC.Models;

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

        public async Task<Rule> GetRule(int id) => await _dbContext.Rules.FindAsync(id);

        public async Task<IEnumerable<Rule>> GetAllRules()
        {
            return await _dbContext.Rules.ToListAsync();
        }

        public async Task<Rule> UpdateRule(Rule rule)
        {
            var existingRule = await _dbContext.Rules.FindAsync(rule.Id);
            if (existingRule != null)
            {
                existingRule.Name = rule.Name;
                existingRule.Criteria = rule.Criteria;
                existingRule.IsActive = rule.IsActive;
                existingRule.UpdatedDate = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }

            return existingRule;
        }
    }

}
