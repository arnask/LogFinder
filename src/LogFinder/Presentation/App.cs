using LogFinder.BusinesLogic.Services;

namespace LogFinder.Presentation;

/// <summary>
/// Main application execution point.
/// </summary>
public class App(IDataParserService dataParserService, IQueryExecutorService queryExecutorService)
{
    /// <summary>
    /// Runs the application and enters the main execution loop.
    /// </summary>
    public void Run()
    {
        while (true) {
        var data = dataParserService.GetParsedData();
        string q = "signatureId='*4608*'";

        queryExecutorService.ExecuteQuery(data, q);
            Console.ReadLine();
        }
    }

}
