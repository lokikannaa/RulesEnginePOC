namespace RulesEnginePOC.Models
{
    public class GetPartsRequest
    {
        public int VehicleId { get; set; }
        public IEnumerable<string>? ContentSiloIds { get; set; }
        public string? TaxonomyId { get; set; }
        public int? VehicleYear { get; set; }
        public string? VehicleMake { get; set; }
    }
}
