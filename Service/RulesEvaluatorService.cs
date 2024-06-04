using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEnginePOC.Models;
using Rule = RulesEnginePOC.Models.Rule;

namespace RulesEnginePOC.Service
{
    public class RulesEvaluatorService : IRulesEvaluatorService
    {
        public RulesEngine.RulesEngine CreateRulesEngine(IEnumerable<Rule> rules, string workflowName, ReSettings? reSettings = null)
        {
            var workflows = CreateWorkflows(rules, workflowName).ToList();
            return new RulesEngine.RulesEngine(workflows.ToArray(), reSettings);
        }

        private IEnumerable<Workflow> CreateWorkflows(IEnumerable<Rule> rules, string workflowName)
        {
            var workflows = new List<Workflow>();
            var workflow = new Workflow
            {
                WorkflowName = workflowName,
                Rules = ToReRules(rules).ToList()
            };
            workflows.Add(workflow);

            return workflows;
        }

        private IEnumerable<RulesEngine.Models.Rule> ToReRules(IEnumerable<Rule> rules)
        {
            var reRules = rules.Select(rules => new RulesEngine.Models.Rule
            {
                RuleName = rules.Name,
                Operator = rules.Criteria.CriteriaOperator.ToString(),
                SuccessEvent = "true",
                Expression = GetExpresstionString(rules.Criteria)
            });

            return reRules;
        }

        private string GetExpresstionString(Criteria criteria)
        {
            var expressions = criteria.Items.Select(GetExpresstionString);
            return string.Join(criteria.CriteriaOperator.ToString(), " " + expressions + " ");
        }

        private string GetExpresstionString(Criterion criterion)
        {
            return $"{criterion.Field} {GetOperator(criterion.Operator)} {criterion.Value}";
        }
        private string GetOperator(OperatorType operatorType)
        {
            return operatorType switch
            {
                OperatorType.Equal => "==",
                OperatorType.NotEqual => "!=",
                OperatorType.GreaterThan => ">",
                OperatorType.GreaterThanOrEqual => ">=",
                OperatorType.LessThan => "<",
                OperatorType.LessThanOrEqual => "<=",
                OperatorType.And => "&&",
                OperatorType.Or => "||",
                _ => throw new NotSupportedException($"Operator {operatorType} is not supported."),
            };
        }
    }
}
