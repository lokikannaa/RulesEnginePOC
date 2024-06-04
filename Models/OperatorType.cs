namespace RulesEnginePOC.Models
{
    public enum OperatorType
    {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Contains,
        NotContains,
        StartsWith,
        EndsWith,
        In,

        And,
        Or,
        AndAlso,
        OrElse
    }
}
