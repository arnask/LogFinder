using LogFinder.BusinesLogic.Models;

namespace LogFinder.BusinesLogic.Services;

/// <summary>
/// Service for executing queries.
/// </summary>
public interface IQueryExecutorService
{
    /// <summary>
    /// Executes queries on the given data.
    /// </summary>
    QueryResult ExecuteQuery(List<RowDictionary> data, string query);
}