using RulesEngine.Actions;
using RulesEngine.Models;
using RulesEnginePOC.Models;

namespace RulesEnginePOC.MyRulesEngine.RuleActions
{
    public class PartsAction : ActionBase
    {
        public override ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            var entitlementId = context.GetContext<int>("EntitlementId");
            var contentSiloIds = context.GetContext<List<int>>("ContentSiloIds");
            var vehicleMakes = context.GetContext<List<string>>("VehicleMakes");
            var taxonomyIds = context.GetContext<List<int>>("TaxonomyIds");

            return new ValueTask<object>(new UserEntitlement
            {
                EntitlementId = entitlementId,
                ContentSiloIds = contentSiloIds,
                VehicleMakes = vehicleMakes,
                TaxonomyIds = taxonomyIds
            });
        }
    }
}
