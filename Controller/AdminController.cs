using Microsoft.AspNetCore.Mvc;
using RulesEnginePOC.Service.Interfaces;
using System.Dynamic;
using System.Net;

namespace RulesEnginePOC.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        public readonly IEntitlementService _entitlementService;
        public AdminController(IEntitlementService entitlementService)
        {
            _entitlementService = entitlementService;
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetVehicleId(int vehicleId)
        {
            IEnumerable<string> requiredEntitlements = ["BaseVehicleId"];

            var inputs = new dynamic[]
            {
                new { VehicleId = vehicleId }
            };

            if (!_entitlementService.HasAccess(requiredEntitlements, HttpContext, inputs))
            {
                return StatusCode((int)HttpStatusCode.Forbidden);
            }

            return Ok("I have access to this Vehicle!");
        }

        [HttpGet("partsForEstimating")]
        public async Task<IActionResult> GetPartsForEstimating(int vehicleId)
        {
            IEnumerable<string> requiredEntitlements = ["PartsForEstimating"];

            var results = _entitlementService.Evaluate(requiredEntitlements, HttpContext, Array.Empty<object>());

            return Ok(results.FirstOrDefault(r => r.IsSuccess).ActionResult.Output);
        }

        [HttpGet("contentSilo")]
        public async Task<IActionResult> GetContentSilo(ContentSiloRequest request)
        {
            IEnumerable<string> requiredEntitlements = ["ContentSilo", "BaseVehicleId"];

            var results = _entitlementService.Evaluate(requiredEntitlements, HttpContext, new dynamic[]
            {
                new
                { 
                    request.ContentSiloIds,
                    request.TaxonomyId,
                    request.VehicleYear,
                    request.VehicleMake
                }
            });
            var outcome = results.FirstOrDefault(r => r.IsSuccess)?.ActionResult.Output;

            return Ok(outcome.ToString());
        }
    }

    public class ContentSiloRequest
    {
        public IEnumerable<string>? ContentSiloIds { get; set; }
        public string TaxonomyId { get; set; }
        public int VehicleYear { get; set; }
        public string VehicleMake { get; set; }
    }

    public class ContentSiloResponse
    {
        public IEnumerable<string> ContentSiloIds { get; set; }
        public IEnumerable<string> VehicleMakes { get; set; }
    }
}
