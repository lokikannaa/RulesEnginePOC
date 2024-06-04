
using RulesEngine.Extensions;
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

        public bool HasAccess(IEnumerable<string> requiredEntitlements, HttpContext httpContext, dynamic[] inputs)
        {
            var workflowName = "EntitlementWorkflow";
            if (httpContext.Items.TryGetValue("EntitlementRules", out var rules))
            {
                var userRules = rules as IEnumerable<Rule>;
                var re = _rulesEvaluatorService.CreateRulesEngine(userRules, workflowName);
                List<RuleResultTree> results = re.ExecuteAllRulesAsync(workflowName, inputs).Result;

                bool outcome = false;

                //Different ways to show test results:
                outcome = results.TrueForAll(r => r.IsSuccess);

                results.OnSuccess((eventName) => {
                    Console.WriteLine($"Result '{eventName}' is as expected.");
                    outcome = true;
                });

                results.OnFail(() => {
                    outcome = false;
                });

                return outcome;
            }

            return true;
        }
    }
}
