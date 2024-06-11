namespace RulesEnginePOC.Models
{
    public class Criterion
    {
        public string Field { get; set; }
        public OperatorType Operator { get; set; }
        public string Value { get; set; }

    }
}
