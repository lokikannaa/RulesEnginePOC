namespace RulesEnginePOC.Service
{
    public interface IEntitlementService
    {
        bool HasAccess(IEnumerable<string> requiredEntitlements, HttpContext httpContext, dynamic[] inputs);
    }
}
