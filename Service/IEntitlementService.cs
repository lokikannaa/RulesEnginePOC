namespace RulesEnginePOC.Service
{
    public interface IEntitlementService
    {
        Task<bool> HasAccess(IEnumerable<string> requiredEntitlements, HttpContext httpContext, dynamic inputs);
    }
}
