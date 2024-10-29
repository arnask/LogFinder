namespace LogFinder.BusinesLogic.Models;

/// <summary>
/// Represents parsed query.
/// </summary>
public class Query(string condition, BoolOperationType operationType)
{
    /// <summary>
    /// Bool operation type.
    /// </summary>
    public BoolOperationType OperationType { get; } = operationType;

    /// <summary>
    /// Condition.
    /// </summary>
    public string Condition { get; } = condition;
}
