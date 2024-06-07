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
        StartsWith,
        EndsWith,
        In,
        ListsEqual,

        And,
        Or,
        AndAlso,
        OrElse
    }
}
