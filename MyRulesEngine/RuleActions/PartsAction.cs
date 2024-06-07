using RulesEngine.Actions;
using RulesEngine.Models;

namespace RulesEnginePOC.MyRulesEngine.RuleActions
{
    public class PartsAction : ActionBase
    {
        public override ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            var contentSiloIds = context.GetContext<object>("ContentSiloIds");
            var vehicleMakes = context.GetContext<object>("VehicleMakes");

            return new ValueTask<object>(new
            {
                contentSiloIds,
                vehicleMakes
            });
        }
    }
}
