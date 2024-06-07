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

            if (! _entitlementService.HasAccess(requiredEntitlements, HttpContext, inputs))
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
                    ContentSiloIds = request.ContentSiloIds,
                    TaxonomyId = request.TaxonomyId,
                    VehicleYear = request.VehicleYear,
                    VehicleMake = request.VehicleMake
                }
            });

            return Ok(results.FirstOrDefault(r => r.IsSuccess).ActionResult.Output);
        }
    }

    public class ContentSiloRequest
    {
        public IEnumerable<int>? ContentSiloIds { get; set; }
        public int? TaxonomyId { get; set; }
        public int VehicleYear { get; set; }
        public string VehicleMake { get; set; }
    }
}
