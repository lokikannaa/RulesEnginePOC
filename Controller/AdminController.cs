using Microsoft.AspNetCore.Mvc;
using RulesEnginePOC.Service;

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

        [HttpGet("vehicle/{id}")]
        public async Task<IActionResult> GetVehicleId(int vehicleId)
        {
            IEnumerable<string> requiredEntitlements = ["BaseVehicleId"];

            if (!await _entitlementService.HasAccess(requiredEntitlements, HttpContext, vehicleId))
            {
                return Unauthorized();
            }

            return Ok("I have access to this Vehicle!");
        }
    }
}
