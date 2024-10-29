using LogFinder.BusinesLogic.Models;
using LogFinder.BusinesLogic.Services;

namespace LogFinder.Tests;

public class DataParserServiceTests
{
    [Fact]
    public void GetParsedData_ShouldCombineResultsFromMultipleFiles()
    {
        var directorySettings = new DirectorySettings() { Path = "TestData" };

        var dataParserService = new DataParserService(directorySettings);

        var data = dataParserService.GetParsedData();

        Assert.Equal(10, data.Count);
    }
}
