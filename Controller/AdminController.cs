using Microsoft.AspNetCore.Mvc;
using RulesEnginePOC.Service.Interfaces;
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
        public IActionResult GetVehicleId(int vehicleId)
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

        [HttpGet("parts")]
        public IActionResult GetParts(GetPartsRequest request)
        {
            //IEnumerable<string> requiredEntitlements = ["PartsForEstimating", "ContentSilo", "BaseVehicleId"];
            IEnumerable<string> requiredEntitlements = ["ContentSilo"];
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

    public class GetPartsRequest
    {
        public int VehicleId { get; set; }
        public IEnumerable<string> ContentSiloIds { get; set; }
        public string? TaxonomyId { get; set; }
        public int? VehicleYear { get; set; }
        public string? VehicleMake { get; set; }
    }

    public class GetPartsReponse
    {
        public IEnumerable<string> ContentSiloIds { get; set; }
        public IEnumerable<string> VehicleMakes { get; set; }
    }
}
