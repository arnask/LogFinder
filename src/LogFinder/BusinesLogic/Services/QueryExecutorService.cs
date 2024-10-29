using LogFinder.BusinesLogic.Models;
using LogFinder.BusinesLogic.Utilities;

namespace LogFinder.BusinesLogic.Services;

/// <inheritdoc cref="IQueryExecutorService"/>
public class QueryExecutorService : IQueryExecutorService
{
    private const char ConditionDelimiter = '=';
    private const char Wildcard = '*';

    /// <inheritdoc/>
    public QueryResult ExecuteQuery(List<RowDictionary> data, string query)
    {
        List<Query> parsedQuery = QueryParser.ParseQuery(query);

        List<RowDictionary> filteredData = FilterData(data, parsedQuery);

        var queryResult = new QueryResult(query, filteredData);

        return queryResult;
    }

    private static List<RowDictionary> FilterData(List<RowDictionary> data, List<Query> conditions)
    {
        var resultData = new List<RowDictionary>();

        foreach (var condition in conditions)
        {
            (string columnName, string searchText) = ParseCondition(condition.Condition);

            var filteredData = data
                .Where(row => row.TryGetValue(columnName, out var value)
                && MatchesCondition(value, searchText)).ToList();

            resultData = condition.OperationType switch
            {
                BoolOperationType.None => filteredData,
                BoolOperationType.And => resultData.Intersect(filteredData).ToList(),
                BoolOperationType.Or => resultData.Union(filteredData).ToList(),
                BoolOperationType.Not => resultData.Except(filteredData).ToList(),
                _ => resultData
            };
        }

        return resultData;
    }

    private static (string columnName, string searchText) ParseCondition(string condition)
    {
        var queryParts = condition.Split(ConditionDelimiter);

        if (queryParts.Length != 2)
        {
            throw new ArgumentException($"Invalid condition format: {condition}");
        }

        string columnName = queryParts[0].Trim();
        string searchText = queryParts[1].Trim().Trim('\'');
        return (columnName, searchText);
    }

    private static bool MatchesCondition(string value, string searchText)
    {
        if (string.IsNullOrEmpty(searchText))
        {
            return false;
        }

        string trimmedSearchText = searchText.Trim(Wildcard);

        if (searchText.StartsWith(Wildcard) && searchText.EndsWith(Wildcard))
        {
            return value.Contains(trimmedSearchText, StringComparison.OrdinalIgnoreCase);
        }
        else if (searchText.StartsWith(Wildcard))
        {
            return value.EndsWith(trimmedSearchText, StringComparison.OrdinalIgnoreCase);
        }
        else if (searchText.EndsWith(Wildcard))
        {
            return value.StartsWith(trimmedSearchText, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            return value.Equals(trimmedSearchText, StringComparison.OrdinalIgnoreCase);
        }
    }
}
