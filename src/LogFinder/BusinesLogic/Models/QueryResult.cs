namespace LogFinder.BusinesLogic.Models;

/// <summary>
/// Query result.
/// </summary>
public class QueryResult(string searchQuery, List<RowDictionary> results)
{
    /// <summary>
    /// Search query.
    /// </summary>
    public string SearchQuery { get; } = searchQuery;

    /// <summary>
    /// Query results count.
    /// </summary>
    public int ResultsCount { get; } = results.Count;

    /// <summary>
    /// Query results.
    /// </summary>
    public List<RowDictionary> Result { get; } = results;
}
