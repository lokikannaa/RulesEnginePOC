﻿
using RulesEngine.Models;
using Rule = RulesEnginePOC.Models.Rule;

namespace RulesEnginePOC.MyRulesEngine
{
    public interface IRulesEvaluatorService
    {
        RulesEngine.RulesEngine CreateRulesEngine(IEnumerable<Rule> rules, string workflowName, ReSettings? reSettings = null);
    }
}