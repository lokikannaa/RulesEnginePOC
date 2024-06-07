﻿using RulesEngine.Actions;
using RulesEngine.Models;

namespace RulesEnginePOC.MyRulesEngine.RuleActions
{
    public class PartsForEstimatingAction : ActionBase
    {
        public override ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            var part1Allowed = context.GetContext<string>("part1Allowed");
            var part2Allowed = context.GetContext<string>("part2Allowed");

            return new ValueTask<object>(new
            {
                part1Allowed,
                part2Allowed
            });
        }
    }
}