using LogFinder.BusinesLogic.Models;

namespace LogFinder.BusinesLogic.Mapping;

/// <summary>
/// Bool operation type mapper.
/// </summary>
public static class BoolOperationTypeMapper
{
    private static readonly Dictionary<string, BoolOperationType> _operationTypeDictionary = new(StringComparer.OrdinalIgnoreCase)
    {
        {"None", BoolOperationType.None},
        {"And", BoolOperationType.And },
        {"Or", BoolOperationType.Or },
        {"Not", BoolOperationType.Not }
    };

    /// <summary>
    /// Tries to get BoolOperationType from string value.
    /// </summary>
    public static bool TryGetOperationTypeFromString(string operationType, out BoolOperationType mappedOperationType)
    {
        return _operationTypeDictionary.TryGetValue(operationType, out mappedOperationType);
    }
}