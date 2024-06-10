namespace RulesEnginePOC.Models
{
    public class GetPartsReponse
    {

    }

    public class UserEntitlements
    {
        public IEnumerable<string> ContentSiloIds { get; set; }
        public IEnumerable<string> VehicleMakes { get; set; }
    }
}
