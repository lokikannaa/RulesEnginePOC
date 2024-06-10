using RulesEnginePOC.Models;

namespace RulesEnginePOC.Tests
{
    /* Note:
     * User 1 has access to Toyota Vehicles with ID: 101, 104
     * User 2 has access to Ford Vehicles with ID: 102, 105
     * 
     */
    public static class TestPartsData
    {
        public static List<Part> GetTestParts()
        {
            return new List<Part>
            {
                new Part
                {
                    PartNumber = 1,
                    Name = "Brake Pad",
                    PartType = "OEM",
                    ApplicableVehicles = new List<VehicleInfo>
                    {
                        new VehicleInfo { Make = "Toyota", Year = 2020, BaseVehicleId = 101, Country = "USA" },
                        new VehicleInfo { Make = "Ford", Year = 2019, BaseVehicleId = 102, Country = "Canada" }
                    },
                    TaxonomyId = 1,
                    contentSiloId = 1
                },
                new Part
                {
                    PartNumber = 2,
                    Name = "Oil Filter",
                    PartType = "Aftermarket",
                    ApplicableVehicles = new List<VehicleInfo>
                    {
                        new VehicleInfo { Make = "Toyota", Year = 2020, BaseVehicleId = 101, Country = "USA" },
                        new VehicleInfo { Make = "Acura", Year = 2018, BaseVehicleId = 103, Country = "USA" },
                        new VehicleInfo { Make = "Toyota", Year = 2021, BaseVehicleId = 104, Country = "Japan" }
                    },
                    TaxonomyId = 2,
                    contentSiloId = 2
                },
                new Part
                {
                    PartNumber = 3,
                    Name = "Air Filter",
                    PartType = "OEM",
                    ApplicableVehicles = new List<VehicleInfo>
                    {
                        new VehicleInfo { Make = "Ford", Year = 2019, BaseVehicleId = 102, Country = "Canada" },
                        new VehicleInfo { Make = "Ford", Year = 2017, BaseVehicleId = 105, Country = "USA" }
                    },
                    TaxonomyId = 3,
                    contentSiloId = 3
                },
                new Part
                {
                    PartNumber = 4,
                    Name = "Spark Plug",
                    PartType = "Aftermarket",
                    ApplicableVehicles = new List<VehicleInfo>
                    {
                        new VehicleInfo { Make = "Acura", Year = 2018, BaseVehicleId = 103, Country = "USA" },
                        new VehicleInfo { Make = "Toyota", Year = 2016, BaseVehicleId = 106, Country = "Canada" }
                    },
                    TaxonomyId = 1,
                    contentSiloId = 4
                },
                new Part
                {
                    PartNumber = 5,
                    Name = "Headlight",
                    PartType = "OEM",
                    ApplicableVehicles = new List<VehicleInfo>
                    {
                        new VehicleInfo { Make = "Acura", Year = 2020, BaseVehicleId = 107, Country = "Japan" }
                    },
                    TaxonomyId = 2,
                    contentSiloId = 1
                },
                new Part
                {
                    PartNumber = 6,
                    Name = "Tail Light",
                    PartType = "Aftermarket",
                    ApplicableVehicles = new List<VehicleInfo>
                    {
                        new VehicleInfo { Make = "Ford", Year = 2021, BaseVehicleId = 108, Country = "USA" }
                    },
                    TaxonomyId = 3,
                    contentSiloId = 2
                },
                new Part
                {
                    PartNumber = 7,
                    Name = "Windshield Wiper",
                    PartType = "OEM",
                    ApplicableVehicles = new List<VehicleInfo>
                    {
                        new VehicleInfo { Make = "Ford", Year = 2019, BaseVehicleId = 102, Country = "Canada" },
                        new VehicleInfo { Make = "Toyota", Year = 2015, BaseVehicleId = 109, Country = "Japan" }
                    },
                    TaxonomyId = 1,
                    contentSiloId = 3
                },
                new Part
                {
                    PartNumber = 8,
                    Name = "Battery",
                    PartType = "Aftermarket",
                    ApplicableVehicles = new List<VehicleInfo>
                    {
                        new VehicleInfo { Make = "Acura", Year = 2018, BaseVehicleId = 103, Country = "USA" },
                        new VehicleInfo { Make = "Acura", Year = 2019, BaseVehicleId = 110, Country = "USA" },
                        new VehicleInfo { Make = "Ford", Year = 2005, BaseVehicleId = 117, Country = "USA" }
                    },
                    TaxonomyId = 2,
                    contentSiloId = 4
                },
                new Part
                {
                    PartNumber = 9,
                    Name = "Alternator",
                    PartType = "OEM",
                    ApplicableVehicles = new List<VehicleInfo>
                    {
                        new VehicleInfo { Make = "Ford", Year = 2009, BaseVehicleId = 111, Country = "Canada" }
                    },
                    TaxonomyId = 3,
                    contentSiloId = 1
                },
                new Part
                {
                    PartNumber = 10,
                    Name = "Radiator",
                    PartType = "Aftermarket",
                    ApplicableVehicles = new List<VehicleInfo>
                    {
                        new VehicleInfo { Make = "Toyota", Year = 2020, BaseVehicleId = 101, Country = "USA" },
                        new VehicleInfo { Make = "Toyota", Year = 2018, BaseVehicleId = 112, Country = "Japan" }
                    },
                    TaxonomyId = 1,
                    contentSiloId = 2
                }
            };
        }
    }
}
