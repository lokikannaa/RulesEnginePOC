using RulesEnginePOC.Models;

namespace RulesEnginePOC.Test
{
    public static class TestRuleData
    {
        public static Rule GetTestRule()
        {
            return new Rule
            {
                Id = 1,
                Name = "Test Rule",
                IsActive = true,
                Criteria = new Criteria
                {
                    Items = [
                        new Criterion
                            {
                                Field = "input1.VehicleId",
                                Operator = OperatorType.Equal,
                                Value = "100"
                            },
                            new Criterion
                            {
                                Field = "input1.VehicleId",
                                Operator = OperatorType.Equal,
                                Value = "200"
                            }
                        ],
                    CriteriaOperator = OperatorType.And,
                }
            };
        }
    }
}
