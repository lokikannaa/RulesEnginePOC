﻿
using RulesEngine.Extensions;
using RulesEngine.Models;
using RulesEnginePOC.MyRulesEngine;
using RulesEnginePOC.Service.Interfaces;
using Rule = RulesEnginePOC.Models.Rule;

namespace RulesEnginePOC.Service
{
    public class EntitlementService : IEntitlementService
    {
        private readonly IRulesEngineService _rulesEvaluatorService;
        public EntitlementService(IRulesEngineService rulesEvaluatorService)
        {
            _rulesEvaluatorService = rulesEvaluatorService;
        }

        public bool HasAccess(IEnumerable<string> requiredEntitlements, HttpContext httpContext, dynamic[] inputs)
        {
            var workflowName = "EntitlementWorkflow";

            if (httpContext.Items.TryGetValue("EntitlementRules", out var rules))
            {
                var userRules = rules as IEnumerable<Rule>;
                var requiredRules = userRules!.Where(r => requiredEntitlements.Contains(r.Entitlement.Name));

                var re = _rulesEvaluatorService.CreateRulesEngine(requiredRules, workflowName);
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

        public List<RuleResultTree> Evaluate(IEnumerable<string> requiredEntitlements, HttpContext httpContext, dynamic[] inputs)
        {
            var workflowName = "EntitlementWorkflow";
            List<RuleResultTree> results = Enumerable.Empty<RuleResultTree>().ToList();

            if (httpContext.Items.TryGetValue("EntitlementRules", out var rules))
            {
                var userRules = rules as IEnumerable<Rule>;
                var requiredRules = userRules!.Where(r => requiredEntitlements.Contains(r.Entitlement?.Name)).ToList();

                if (requiredRules.Any())
                {
                    var re = _rulesEvaluatorService.CreateRulesEngine(requiredRules, workflowName);
                    results = re.ExecuteAllRulesAsync(workflowName, inputs).Result;
                }
            }

            return results;
        }
    }
}
