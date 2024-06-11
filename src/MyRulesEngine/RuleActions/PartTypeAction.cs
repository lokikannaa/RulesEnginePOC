using RulesEngine.Actions;
using RulesEngine.Models;
using RulesEnginePOC.Models;

namespace RulesEnginePOC.MyRulesEngine.RuleActions
{
    public class PartTypeAction : ActionBase
    {
        public override ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            var entitlementId = context.GetContext<int>("EntitlementId");
            var oem = context.GetContext<bool>("OEM");
            var aftermarket = context.GetContext<bool>("Aftermarket");

            return new ValueTask<object>(new UserEntitlement
            {
                EntitlementId = entitlementId,
                HasOEMAccess = oem,
                HasAftermarketAccess = aftermarket
            });
        }
    }
}
