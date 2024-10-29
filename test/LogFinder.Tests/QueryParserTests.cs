using LogFinder.BusinesLogic.Models;
using LogFinder.BusinesLogic.Utilities;

namespace LogFinder.Tests;

public class QueryParserTests
{
    [Theory]
    [InlineData("signatureId = 'Microsoft-Windows-Security-Auditing:4608'")]
    [InlineData("SELECT FROM signatureId WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608'")]
    [InlineData("SELECT WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608'")]
    public void ParseQuery_WithoutBoolOperators_ShouldReturnCorrectQuery(string query)
    {
        var results = QueryParser.ParseQuery(query);

        List<Query> expectedQueries =
        [
            new("signatureId = 'Microsoft-Windows-Security-Auditing:4608'", BoolOperationType.None),
        ];

        AssertAllQueriesAreCorrect(expectedQueries, results);
    }

    [Theory]
    [InlineData("signatureId = 'Microsoft-Windows-Security-Auditing:4608' AND eventId = '*4608*' OR category = 'User'")]
    [InlineData("SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608' AND eventId = '*4608*' OR category = 'User'")]
    public void ParseQuery_WithBoolOpearators_ShouldReturnCorrectQueries(string query)
    {
        var results = QueryParser.ParseQuery(query);

        List<Query> expectedQueries =
        [
            new("signatureId = 'Microsoft-Windows-Security-Auditing:4608'", BoolOperationType.None),
            new("eventId = '*4608*'", BoolOperationType.And),
            new("category = 'User'", BoolOperationType.Or)
        ];

        AssertAllQueriesAreCorrect(expectedQueries, results);
    }

    [Fact]
    public void ParseQuery_StartsWithBoolOperator_ShouldThrowException()
    {
        string query = "AND signatureId = 'Microsoft-Windows-Security-Auditing:4608'";

        Assert.Throws<ArgumentException>(() => QueryParser.ParseQuery(query));
    }

    [Fact]
    public void ParseQuery_TwoBoolOperators_ShouldThrowException()
    {
        string query = "signatureId = 'Microsoft-Windows-Security-Auditing:4608' AND OR";

        Assert.Throws<ArgumentException>(() => QueryParser.ParseQuery(query));
    }

    private static void AssertAllQueriesAreCorrect(List<Query> expectedQueries, List<Query> queries)
    {
        Assert.Equal(expectedQueries.Count, queries.Count);

        for (int i = 0; i < queries.Count; i++)
        {
            Assert.Equal(expectedQueries[i].Condition, queries[i].Condition);
            Assert.Equal(expectedQueries[i].OperationType, queries[i].OperationType);
        }
    }
}
