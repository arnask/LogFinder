using LogFinder.DataLayer.Entities;
using MongoDB.Driver;

namespace LogFinder.DataLayer.Repositories;

/// <inheritdoc cref="IQueryResultRepository"/>
public class QueryResultRepository(QueryResultDbContext context) : IQueryResultRepository
{
    private readonly IMongoCollection<QueryResultEntity> _queryResults = context.QueryResults;

    /// <inheritdoc/>
    public async Task InsertQueryResult(QueryResultEntity queryResult)
    {
        await _queryResults.InsertOneAsync(queryResult);
    }
}