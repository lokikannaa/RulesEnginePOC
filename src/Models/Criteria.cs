namespace RulesEnginePOC.Models
{
    public class Criteria
    {
        public IEnumerable<Criterion>? Items { get; set; }
        public OperatorType Operator { get; set; }
    }

}
