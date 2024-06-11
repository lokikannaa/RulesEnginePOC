namespace RulesEnginePOC.Models
{
    public class GetPartsRequest
    {
        public int VehicleId { get; set; }
        public IEnumerable<int>? ContentSiloIds { get; set; }
        public int? TaxonomyId { get; set; }
        public int? VehicleYear { get; set; }
        public string? VehicleMake { get; set; }
    }

    public class GetPartsReponse
    {
        public IEnumerable<Part> Parts { get; set; }
    }

    public class UserEntitlement
    {
        public int EntitlementId { get; set; }
        public IEnumerable<int>? ContentSiloIds { get; set; }
        public IEnumerable<string>? VehicleMakes { get; set; }
        public IEnumerable<int>? TaxonomyIds { get; set; }
        public bool? HasOEMAccess { get; set; }
        public bool? HasAftermarketAccess { get; set; }
    }

    public class Part
    {
        public int PartNumber { get; set; }
        public string Name { get; set; }
        public string PartType { get; set; }
        public IEnumerable<VehicleInfo> ApplicableVehicles { get; set; }
        public int TaxonomyId { get; set; }
        public int contentSiloId { get; set; }
    }

    public class VehicleInfo
    {
        public string Make { get; set; }
        public int Year { get; set; }
        public int BaseVehicleId { get; set; }
        public string Country { get; set; }
    }
}
