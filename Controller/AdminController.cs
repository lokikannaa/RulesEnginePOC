using Microsoft.AspNetCore.Mvc;
using RulesEnginePOC.Models;
using RulesEnginePOC.Service.Interfaces;
using RulesEnginePOC.Tests;
using System.Net;
using System.Text.Json;

namespace RulesEnginePOC.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        public readonly IEntitlementService _entitlementService;
        private IEnumerable<Part> _allParts;

        public AdminController(IEntitlementService entitlementService, IEnumerable<Part> allParts)
        {
            _entitlementService = entitlementService;
            _allParts = TestPartsData.GetTestParts();
        }

        [HttpGet("vehicle/{vehicleId}")]
        public IActionResult GetVehicleId(int vehicleId)
        {
            IEnumerable<string> requiredEntitlements = ["BaseVehicleId"];

            var inputs = new dynamic[]
            {
                new { VehicleId = vehicleId }
            };

            var requestedMake = _allParts.FirstOrDefault(p => p.ApplicableVehicles.Any(v => v.BaseVehicleId == vehicleId))?.ApplicableVehicles.FirstOrDefault(v => v.BaseVehicleId == vehicleId)?.Make;

            if (!_entitlementService.HasAccess(requiredEntitlements, HttpContext, inputs))
            {
                return StatusCode((int)HttpStatusCode.Forbidden, $"You do not have access to this {requestedMake} vehicle");
            }

            return Ok($"You have access to this {requestedMake} vehicle");
        }

        [HttpGet("parts")]
        public IActionResult GetParts(GetPartsRequest request)
        {
            var hasAccessToBaseVehicled = _entitlementService.HasAccess(["BaseVehicleId"], HttpContext, [new { request.VehicleId }]);
            if (!hasAccessToBaseVehicled)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "You do not have access to this vehicle");
            }

            IEnumerable<string> requiredEntitlements = ["PartType", "ContentSilo"];
            var outcome = _entitlementService.Evaluate(requiredEntitlements, HttpContext,
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

            var result = _allParts;

            var isAllPass = outcome.TrueForAll(r => r.IsSuccess); // return all parts filtered by request params

            //if (isAllPass)
            //{
            //    var x = result
            //        .Where(p => p.ApplicableVehicles.Any(v => v.Make == request.VehicleMake && v.Year == request.VehicleYear && v.BaseVehicleId == request.VehicleId))
            //        .Where(p => p.TaxonomyId == request.TaxonomyId)
            //        .Where(p => request.ContentSiloIds != null && request.ContentSiloIds.Contains(p.contentSiloId));
            //    result = result
            //        .Where(p => p.ApplicableVehicles.Any(v => v.Make == request.VehicleMake && v.Year == request.VehicleYear && v.BaseVehicleId == request.VehicleId))
            //        .Where(p => p.TaxonomyId == request.TaxonomyId)
            //        .Where(p => request.ContentSiloIds != null && request.ContentSiloIds.Contains(p.contentSiloId))
            //        .Select(x => new Part
            //        {
            //            contentSiloId = x.contentSiloId,
            //            Name = x.Name,
            //            PartNumber = x.PartNumber,
            //            PartType = x.PartType,
            //            TaxonomyId = x.TaxonomyId,
            //            ApplicableVehicles = x.ApplicableVehicles.Where(v => v.BaseVehicleId == request.VehicleId)
            //        });

            //    return Ok(result);
            //}

            var actionResults = outcome
                    .Where(o => o.ActionResult.Output != null)
                    .Select(o =>
                    {
                        var output = (UserEntitlement)o.ActionResult.Output;
                        return new UserEntitlement
                        {
                            EntitlementId = output.EntitlementId,
                            ContentSiloIds = output.ContentSiloIds,
                            TaxonomyIds = output.TaxonomyIds,
                            VehicleMakes = output.VehicleMakes,
                            HasOEMAccess = output.HasOEMAccess,
                            HasAftermarketAccess = output.HasAftermarketAccess
                        };
                    });
            var contentSiloEntitlement = actionResults.FirstOrDefault(r => r.EntitlementId == 2);
            var partTypeEntitlement = actionResults.FirstOrDefault(r => r.EntitlementId == 3);

            if (hasAccessToBaseVehicled)
            {
                result = result
                    .Where(p => p.ApplicableVehicles.Any(v => v.BaseVehicleId == request.VehicleId))
                    .Select(x => new Part
                    {
                        contentSiloId = x.contentSiloId,
                        Name = x.Name,
                        PartNumber = x.PartNumber,
                        PartType = x.PartType,
                        TaxonomyId = x.TaxonomyId,
                        ApplicableVehicles = x.ApplicableVehicles.Where(v => v.BaseVehicleId == request.VehicleId)
                    });
            }

            if (contentSiloEntitlement != null && contentSiloEntitlement.ContentSiloIds != null)
            {
                result = result.Where(p => contentSiloEntitlement.ContentSiloIds.Contains(p.contentSiloId));
            }

            if (partTypeEntitlement != null)
            {
                if (partTypeEntitlement.HasOEMAccess == false)
                {
                    result = result.Where(p => p.PartType != "OEM");
                }

                if (partTypeEntitlement.HasAftermarketAccess == false)
                {
                    result = result.Where(p => p.PartType != "Aftermarket");
                }
            }

            return Ok(result);
        }

    }
}
