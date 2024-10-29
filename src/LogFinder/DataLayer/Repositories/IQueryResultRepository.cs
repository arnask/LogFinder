using LogFinder.DataLayer.Entities;

namespace LogFinder.DataLayer.Repositories;

/// <summary>
/// Repository for managing query results in the database.
/// </summary>
public interface IQueryResultRepository
{
    /// <summary>
    /// Inserts QueryResultEntity to the database.
    /// </summary>
    Task InsertQueryResult(QueryResultEntity log);
}