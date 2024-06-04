
using RulesEngine.Models;
using Rule = RulesEnginePOC.Models.Rule;

namespace RulesEnginePOC.Service
{
    public class EntitlementService : IEntitlementService
    {
        private readonly IRulesEvaluatorService _rulesEvaluatorService;
        public EntitlementService(IRulesEvaluatorService rulesEvaluatorService)
        {
            _rulesEvaluatorService = rulesEvaluatorService;
        }

        public async Task<bool> HasAccess(IEnumerable<string> requiredEntitlements, HttpContext httpContext, dynamic inputs)
        {
            var workflowName = "EntitlementWorkflow";
            if (httpContext.Items.TryGetValue("EntitlementRules", out var rules))
            {
                var userRules = rules as IEnumerable<Rule>;
                var re = _rulesEvaluatorService.CreateRulesEngine(userRules, workflowName);
                List<RuleResultTree> response = await re.ExecuteAllRulesAsync(workflowName, inputs).Result;
            }

            return await Task.FromResult(true);
        }
    }
}
