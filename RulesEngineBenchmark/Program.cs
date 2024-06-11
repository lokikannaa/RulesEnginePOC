using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using RulesEngine.Actions;
using RulesEngine.Models;
using RulesEnginePOC.MyRulesEngine;
using RulesEnginePOC.MyRulesEngine.RuleActions;
using System.Runtime;

namespace RulesEngineBenchmark
{
    [MemoryDiagnoser]
    public class REBenchmark
    {
        private readonly RulesEngine.RulesEngine rulesEngine;
        private readonly object ruleInput;
        private readonly List<Workflow> workflow;

        private class ListItem
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }


        public REBenchmark()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "rules.json", SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
            {
                throw new Exception("Rules not found.");
            }

            var fileData = File.ReadAllText(files[0]);
            workflow = JsonConvert.DeserializeObject<List<Workflow>>(fileData);

            rulesEngine = new RulesEngine.RulesEngine(workflow.ToArray(), new ReSettings
            {
                CustomTypes = [typeof(Utils)],
                CustomActions = new Dictionary<string, System.Func<ActionBase>>
                {
                    { "PartTypeAction", () => new PartTypeAction() },
                    { "PartsAction", () => new PartsAction() }
                },
                EnableFormattedErrorMessage = false,
                EnableScopedParams = false
            });

            ruleInput = new
            {
                VehicleId = 101,
                ContentSiloIds = new List<string> { "1", "2", "3" },
                TaxonomyId = "1",
                VehicleMake = "Toyota",
                VehicleYear = "2020",
                SimpleProp = "simpleProp",
                NestedProp = new
                {
                    SimpleProp = "nestedSimpleProp",
                    ListProp = new List<ListItem>
                    {
                        new ListItem
                        {
                            Id = 1,
                            Value = "first"
                        },
                        new ListItem
                        {
                            Id = 2,
                            Value = "second"
                        }
                    }
                }

            };
        }

        [Params(1,2)]
        public int N;

        [Benchmark]
        public void RuleExecutionDefault()
        {
            foreach (var workflow in workflow)
            {
                _ = rulesEngine.ExecuteAllRulesAsync(workflow.WorkflowName, ruleInput).Result;
            }
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            _ = BenchmarkRunner.Run<REBenchmark>();

        }
    }
}
