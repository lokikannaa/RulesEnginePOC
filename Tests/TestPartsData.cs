namespace RulesEnginePOC.Tests
{
    public static class TestPartsData
    {
        public static object GetPartsData()
        {
            return new
            {
                EntitlementId = 1,
                ContentSiloIds = new[] { "ContentSiloId1", "ContentSiloId2" },
                VehicleMakes = new[] { "VehicleMake1", "VehicleMake2" }
            };
        }
    }
}
