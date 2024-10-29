using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LogFinder.DataLayer.Entities;

/// <summary>
/// Query result entity which is placed in database.
/// </summary>
public class QueryResultEntity(string searchQuery, List<RowEntity> results)
{
    /// <summary>
    /// Id.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    /// <summary>
    /// Search query.
    /// </summary>
    public string SearchQuery { get; set; } = searchQuery;

    /// <summary>
    /// Result count.
    /// </summary>
    public int ResultsCount { get; set; } = results.Count;

    /// <summary>
    /// Results.
    /// </summary>
    public List<RowEntity> Results { get; set; } = results;
}
