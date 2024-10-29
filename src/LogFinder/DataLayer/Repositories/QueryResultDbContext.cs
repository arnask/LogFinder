using LogFinder.DataLayer.Entities;
using LogFinder.DataLayer.Settings;
using MongoDB.Driver;

namespace LogFinder.DataLayer.Repositories;

/// <summary>
/// Represents the database context for accessing database collections.
/// </summary>
public class QueryResultDbContext
{
    private readonly IMongoDatabase _database;
    private readonly DatabaseSettings _databaseSettings;

    /// <inheritdoc cref="QueryResultDbContext"/>
    public QueryResultDbContext(DatabaseSettings databaseSettings)
    {
        _databaseSettings = databaseSettings ?? throw new ArgumentNullException(nameof(databaseSettings));
        
        var client = new MongoClient(_databaseSettings.ConnectionString);
        _database = client.GetDatabase(_databaseSettings.Database);
    }

    /// <summary>
    /// Property to provide access to perform operations on query results.
    /// </summary>
    public IMongoCollection<QueryResultEntity> QueryResults => _database.GetCollection<QueryResultEntity>(_databaseSettings.Collection);
}
