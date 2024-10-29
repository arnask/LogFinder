namespace LogFinder.BusinesLogic.Models;

/// <summary>
/// Represents the types of bool operations.
/// </summary>
public enum BoolOperationType
{
    /// <summary>
    /// Represents that there is no bool operation.
    /// </summary>
    None,

    /// <summary>
    /// Represents the logical AND operation.
    /// </summary>
    And,

    /// <summary>
    /// Represents the logical OR operation.
    /// </summary>
    Or,

    /// <summary>
    /// Represents the logical NOT operation.
    /// </summary>
    Not
}
