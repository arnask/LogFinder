using LogFinder.BusinesLogic.Models;
using LogFinder.BusinesLogic.Services;
using LogFinder.DataLayer.Entities;
using LogFinder.DataLayer.Repositories;
using NSubstitute;

namespace LogFinder.Tests;

public class QueryExecutorServiceTests
{
    private readonly QueryExecutorService _queryExecutorService;
    private readonly IQueryResultRepository _mockQueryResultRepository;

    public QueryExecutorServiceTests()
    {
        _mockQueryResultRepository = Substitute.For<IQueryResultRepository>();
        _queryExecutorService = new QueryExecutorService(_mockQueryResultRepository);
    }

    [Theory]
    [InlineData("SELECT FROM WHERE deviceVendor = 'Microsoft'")]
    [InlineData("deviceVendor = 'Microsoft'")]
    public void ExecuteQuery_WhenCorrectQueryIsProvided_ShouldReturnCorrectResult(string query)
    {
        var data = new List<RowDictionary>
        {
            CreateRowDictionary(("deviceVendor", "Microsoft")),
            CreateRowDictionary(("deviceVendor", "Apple")),
            CreateRowDictionary(("deviceVendor", "Microsoft")),
            CreateRowDictionary(("deviceVendor", "Apple"))
        };

        var queryResult = _queryExecutorService.ExecuteQuery(data, query);
        var expectedRows = new List<RowDictionary> { data[0], data[2] };

        AssertQueryResultIsCorrect(expectedRows, queryResult);
    }

    [Theory]
    [InlineData("SELECT FROM WHERE deviceVendor = 'Microsoft' AND severity = '3'")]
    [InlineData("deviceVendor = 'Microsoft' AND severity = '3'")]
    public void ExecuteQuery_WhenAndOperationIsApplied_ShouldReturnCorrectResult(string query)
    {
        var dataRows = new List<RowDictionary>
        {
            CreateRowDictionary(("deviceVendor", "Microsoft"), ("severity", "3")),
            CreateRowDictionary(("deviceVendor", "Microsoft"), ("severity", "2")),
            CreateRowDictionary(("deviceVendor", "Microsoft"), ("severity", "2")),
            CreateRowDictionary(("deviceVendor", "Microsoft"), ("severity", "3"))
        };

        var queryResult = _queryExecutorService.ExecuteQuery(dataRows, query);
        var expectedRows = new List<RowDictionary> { dataRows[0], dataRows[3] };

        AssertQueryResultIsCorrect(expectedRows, queryResult);
    }

    [Theory]
    [InlineData("SELECT FROM WHERE severity = '2' OR severity = '3'")]
    [InlineData("severity = '2' OR severity = '3'")]
    public void ExecuteQuery_WhenOrOperationIsApplied_ShouldReturnCorrectResult(string query)
    {
        var dataRows = new List<RowDictionary>
        {
            CreateRowDictionary(("severity", "1")),
            CreateRowDictionary(("severity", "2")),
            CreateRowDictionary(("severity", "3"))
        };

        var queryResult = _queryExecutorService.ExecuteQuery(dataRows, query);
        var expectedRows = new List<RowDictionary> { dataRows[1], dataRows[2] };

        AssertQueryResultIsCorrect(expectedRows, queryResult);
    }

    [Theory]
    [InlineData("SELECT FROM WHERE deviceVendor = 'Microsoft' NOT severity = '1'")]
    [InlineData("deviceVendor = 'Microsoft' NOT severity = '1'")]
    public void ExecuteQuery_WhenNotOperationIsApplied_ShouldReturnCorrectResult(string query)
    {
        var dataRows = new List<RowDictionary>
        {
            CreateRowDictionary(("deviceVendor", "Microsoft"), ("severity", "1")),
            CreateRowDictionary(("deviceVendor", "Microsoft"), ("severity", "2")),
            CreateRowDictionary(("deviceVendor", "Microsoft"), ("severity", "3"))
        };

        var queryResult = _queryExecutorService.ExecuteQuery(dataRows, query);
        var expectedRows = new List<RowDictionary> { dataRows[1], dataRows[2] };

        AssertQueryResultIsCorrect(expectedRows, queryResult);
    }

    [Theory]
    [InlineData("SELECT FROM WHERE deviceVendor = 'Micro*'")]
    [InlineData("deviceVendor = 'Micro*'")]
    [InlineData("SELECT FROM WHERE deviceVendor = '*soft'")]
    [InlineData("deviceVendor = '*soft'")]
    [InlineData("SELECT FROM WHERE deviceVendor = '*cro*'")]
    [InlineData("deviceVendor = '*cro*'")]
    public void ExecuteQuery_WhenUsingWildCards_ShouldReturnCorrectResult(string query)
    {
        var rows = new List<RowDictionary>
        {
            CreateRowDictionary(("deviceVendor", "Microsoft")),
            CreateRowDictionary(("deviceVendor", "Apple"))
        };

        var queryResult = _queryExecutorService.ExecuteQuery(rows, query);
        var expectedRows = new List<RowDictionary> { rows[0] };

        AssertQueryResultIsCorrect(expectedRows, queryResult);
    }

    private static RowDictionary CreateRowDictionary(params (string Key, string Value)[] properties)
    {
        var columnDictionary = new RowDictionary();

        foreach ((string Key, string Value) in properties)
        {
            columnDictionary[Key] = Value;
        }

        return columnDictionary;
    }

    private void AssertQueryResultIsCorrect(List<RowDictionary> expectedRows, QueryResult queryResult)
    {
        Assert.Equal(expectedRows.Count, queryResult.ResultsCount);

        AssertMatchedRowsAreCorrect(expectedRows, queryResult.Result);

        _mockQueryResultRepository.Received(1).InsertQueryResult(Arg.Any<QueryResultEntity>());
    }

    private static void AssertMatchedRowsAreCorrect(List<RowDictionary> expectedRows, List<RowDictionary> actualRows)
    {
        for (int i = 0; i < expectedRows.Count; i++)
        {
            Assert.Equal(expectedRows[i].Count, actualRows[i].Count);

            foreach (var expectedColumn in expectedRows[i])
            {
                if (actualRows[i].TryGetValue(expectedColumn.Key, out var actualColumnValue))
                {
                    Assert.Equal(expectedColumn.Value, actualColumnValue);
                }
                else
                {
                    throw new KeyNotFoundException($"Column '{expectedColumn.Key}' not found in actual results.");
                }
            }
        }
    }
}
