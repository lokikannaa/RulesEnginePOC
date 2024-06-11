using RulesEngine.Models;

namespace RulesEnginePOC.Service.Interfaces
{
    public interface IEntitlementService
    {
        bool HasAccess(IEnumerable<string> requiredEntitlements, HttpContext httpContext, dynamic[] inputs);
        List<RuleResultTree> Evaluate(IEnumerable<string> requiredEntitlements, HttpContext httpContext, dynamic[] inputs);
    }
}
