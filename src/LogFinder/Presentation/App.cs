using LogFinder.BusinesLogic.Models;
using LogFinder.BusinesLogic.Services;
using LogFinder.BusinesLogic.Utilities;
using System.Text.Json;

namespace LogFinder.Presentation;

/// <summary>
/// Main application execution point.
/// </summary>
public class App(IQueryExecutorService queryExecutorService, IDataParserService dataParserService)
{
    private const string ChangeDirectoryArgument = "cd";
    private const string AlertArgument = "alert";

    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    private List<RowDictionary> _data = [];

    /// <summary>
    /// Runs the application and enters the main execution loop.
    /// </summary>
    public void Run()
    {
        _data = dataParserService.GetParsedData();

        while (true)
        {
            ShowConsoleHeader();
            var userInput = Console.ReadLine();
            try
            {
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    continue;
                }
                else if (userInput.StartsWith(ChangeDirectoryArgument))
                {
                    string path = userInput[ChangeDirectoryArgument.Length..].TrimStart();
                    _data = dataParserService.GetParsedData(path);
                    Console.WriteLine($"Loaded data from directory: {path}.");
                }
                else
                {
                    bool shouldTriggerAlert = userInput.EndsWith(AlertArgument, StringComparison.OrdinalIgnoreCase);
                    userInput = userInput.Replace(AlertArgument, "", StringComparison.OrdinalIgnoreCase);

                    var queryResult = queryExecutorService.ExecuteQuery(_data, userInput);
                    var jsonResult = JsonSerializer.Serialize(queryResult, _jsonOptions);
                    Console.WriteLine(jsonResult);

                    if (shouldTriggerAlert)
                    {
                        AlertUtility.TriggerAlert(queryResult.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}.");
            }
        }
    }

    private static void ShowConsoleHeader()
    {
        Console.WriteLine("*********************************************************************************************************************");
        Console.WriteLine("                                           Query Log Retrieval Instructions                                          ");
        Console.WriteLine();
        Console.WriteLine("Applicable query formats:");
        Console.WriteLine("1. [column_name = 'search_string']");
        Console.WriteLine("2. [SELECT FROM WHERE text = 'search_string']");
        Console.WriteLine(" - Example: [signatureId='Microsoft-Windows-Security-Auditing:4608'] or [SELECT FROM WHERE signatureId = 'Microsoft-Windows-Security-Auditing:4608']");
        Console.WriteLine();
        Console.WriteLine("To add an alert to severity levels, append 'alert' to your query:");
        Console.WriteLine(" - Example: [column_name = 'search_string' alert] or [SELECT FROM WHERE text = 'search_string' alert]");
        Console.WriteLine();
        Console.WriteLine("To change the files directory, use the following command:");
        Console.WriteLine(" - [cd path/to/csv/files]");
        Console.WriteLine("*********************************************************************************************************************");
    }
}
