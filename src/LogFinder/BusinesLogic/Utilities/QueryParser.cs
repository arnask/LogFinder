using LogFinder.BusinesLogic.Mapping;
using LogFinder.BusinesLogic.Models;
using System.Text.RegularExpressions;

namespace LogFinder.BusinesLogic.Utilities;

/// <summary>
/// Provides a method to parse queries.
/// </summary>
public static partial class QueryParser
{
    private const string ConditionPattern = @"(\w+)\s*=\s*'([^']*)'";
    private const string ConditionsKeyword = "conditions";
    private const string SqlPattern = $@"SELECT\s*(?<columns>\w+|\*)?\s*(?:FROM\s+(?<table>\w+))?\s+WHERE\s+(?<{ConditionsKeyword}>.+)";
    private const string SelectKeyword = "SELECT";

    /// <summary>
    /// Parses given query.
    /// </summary>
    public static List<Query> ParseQuery(string query)
    {
        return IsSqlQuery(query) ? ParseSqlQuery(query) : ParseConditions(query);
    }

    private static bool IsSqlQuery(string query)
    {
        return query.TrimStart().StartsWith(SelectKeyword, StringComparison.OrdinalIgnoreCase);
    }

    private static List<Query> ParseSqlQuery(string query)
    {
        var sqlQueryMatch = SqlRegex().Match(query);

        if (!sqlQueryMatch.Success)
        {
            throw new ArgumentException("SQL query format is incorrect.");
        }

        string conditions = sqlQueryMatch.Groups[ConditionsKeyword].Value;

        return ParseConditions(conditions);
    }

    private static List<Query> ParseConditions(string conditions)
    {
        List<Query> parsedQueries = [];
        string[] queryParts = BoolRegex().Split(conditions);

        for (int i = 0; i < queryParts.Length; i++)
        {
            BoolOperationType operationType = BoolOperationType.None;

            if (i > 0 && BoolOperationTypeMapper.TryGetOperationTypeFromString(queryParts[i].Trim(), out operationType))
            {
                i++; // The next part should be a condition.

                if (i >= queryParts.Length)
                {
                    throw new ArgumentException("Condition was in an incorrect format.");
                }
            }

            Query parsedQuery = GetParsedQuery(queryParts[i], operationType);
            parsedQueries.Add(parsedQuery);
        }

        return parsedQueries;
    }

    private static Query GetParsedQuery(string condition, BoolOperationType operationType)
    {
        var match = ConditionRegex().Match(condition);
        
        if (!match.Success)
        {
            throw new ArgumentException("Condition was incorrect format.");
        }
        
        return new Query(match.Value, operationType);
    }

    [GeneratedRegex(@"\s*(AND|OR|NOT)\s+")]
    private static partial Regex BoolRegex();

    [GeneratedRegex(SqlPattern, RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex SqlRegex();

    [GeneratedRegex(ConditionPattern)]
    private static partial Regex ConditionRegex();
}
