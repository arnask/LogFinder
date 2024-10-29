using LogFinder.BusinesLogic.Models;
using LogFinder.DataLayer.Entities;

namespace LogFinder.BusinesLogic.Mapping;

/// <summary>
/// Provides method for mappping QueryResult to QueryResultEntity.
/// </summary>
public static class QueryResultMapper
{
    /// <summary>
    /// Maps QueryResult to QueryResultEntity.
    /// </summary>
    public static QueryResultEntity MapToQueryResultEntity(this QueryResult queryResult)
    {
        List<RowEntity> resultEntities = queryResult.Result.Select(ConvertToRowEntity).ToList();

        return new QueryResultEntity(queryResult.SearchQuery, resultEntities);
    }

    private static RowEntity ConvertToRowEntity(RowDictionary row)
    {
        var rowEntity = new RowEntity();

        foreach (var column in row)
        {
            rowEntity[column.Key] = column.Value;
        }

        return rowEntity;
    }
}
