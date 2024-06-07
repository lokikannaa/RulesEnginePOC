using RulesEngine.Actions;
using RulesEngine.Models;
using RulesEnginePOC.Models;
using RulesEnginePOC.MyRulesEngine.RuleActions;
using System.Data;
using System.Text;
using Rule = RulesEnginePOC.Models.Rule;

namespace RulesEnginePOC.MyRulesEngine
{
    public class RulesEvaluatorService : IRulesEvaluatorService
    {
        public RulesEngine.RulesEngine CreateRulesEngine(IEnumerable<Rule> rules, string workflowName, ReSettings? reSettings = null)
        {
            var workflows = CreateWorkflows(rules, workflowName).ToList();

            reSettings = new ReSettings
            {
                CustomTypes = [typeof(Utils)],
                CustomActions = new Dictionary<string, System.Func<ActionBase>>
                {
                    { "PartsForEstimatingAction", () => new PartsForEstimatingAction() },
                    { "PartsAction", () => new PartsAction() }
                }
            };

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
            var reRules = rules.Select(rule => new RulesEngine.Models.Rule
            {
                RuleName = rule.RuleName,
                Operator = rule.ChildRules != null && rule.ChildRules.Any() ? rule.Criteria.Operator.ToString() : null,
                SuccessEvent = "true",
                Expression = GetExpresstionString(rule.Criteria),
                Actions = rule.Actions,
                Rules = rule.ChildRules != null ? ToReRules(rule.ChildRules).ToList() : Enumerable.Empty<RulesEngine.Models.Rule>()
            });

            return reRules;
        }

        private string GetExpresstionString(Criteria criteria)
        {
            var expressions = criteria.Items == null 
                ? ["true == true"] : criteria.Items!.Select(GetExpresstionString).ToList();
            return string.Join(criteria.Operator.ToString(), expressions);
        }

        private string GetExpresstionString(Criterion criterion) => GetExpresstion(criterion.Field, criterion.Operator, criterion.Value);
        private string GetExpresstion(string field, OperatorType operatorType, string value)
        {
            return operatorType switch
            {
                OperatorType.Equal => $" {field} == {value} ",
                OperatorType.NotEqual => $" {field} != {value} ",
                OperatorType.GreaterThan => $" {field} > {value} ",
                OperatorType.GreaterThanOrEqual => $" {field} >= {value} ",
                OperatorType.LessThan => $" {field} < {value} ",
                OperatorType.LessThanOrEqual => $" {field} <= {value} ",
                OperatorType.And => $" {field} && {value} ",
                OperatorType.Or => $" {field} || {value} ",
                OperatorType.In => $" Utils.CheckContains({field}, \"{value}\") == true ",
                OperatorType.ListsEqual => $" Utils.AreListsEqual({field}, \"{value}\") == true ",
                _ => throw new NotSupportedException($"Operator {operatorType} is not supported."),
            };
        }
    }
}
