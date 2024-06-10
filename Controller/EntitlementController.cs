using Microsoft.AspNetCore.Mvc;
using RulesEnginePOC.Models;
using RulesEnginePOC.Service.Interfaces;

namespace RulesEnginePOC.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class EntitlementController : ControllerBase
    {
        public readonly IEntitlementService _entitlementService;
        public EntitlementController(IEntitlementService entitlementService)
        {
            _entitlementService = entitlementService;
        }


        [HttpGet("partsForEstimating")]
        public IActionResult GetPartsForEstimating(int vehicleId)
        {
            IEnumerable<string> requiredEntitlements = ["PartsForEstimating"];

            var results = _entitlementService.Evaluate(requiredEntitlements, HttpContext, Array.Empty<object>());

            return Ok(results.FirstOrDefault(r => r.IsSuccess).ActionResult.Output);
        }

        [HttpGet("contentSilo")]
        public IActionResult GetContentSilo(GetPartsRequest request)
        {
            IEnumerable<string> requiredEntitlements = ["ContentSilo", "BaseVehicleId"];

            var results = _entitlementService.Evaluate(requiredEntitlements, HttpContext,
            [
                new
                {
                    request.VehicleId,
                    request.ContentSiloIds,
                    request.TaxonomyId,
                    request.VehicleYear,
                    request.VehicleMake
                }
            ]);
            var outcome = results.FirstOrDefault(r => r.IsSuccess)?.ActionResult.Output;

            return Ok(outcome?.ToString());
        }
    }
}
