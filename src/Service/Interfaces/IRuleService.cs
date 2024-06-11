using RulesEnginePOC.Models;

namespace RulesEnginePOC.Service.Interfaces
{
    public interface IRuleService
    {
        Task<Rule> CreateRule(Rule rule);
        Task<Rule> GetRule(int id);
        Task<IEnumerable<Rule>> GetAllRules();
        Task<Rule> UpdateRule(Rule rule);
        Task DeleteRule(int id);
    }

}
