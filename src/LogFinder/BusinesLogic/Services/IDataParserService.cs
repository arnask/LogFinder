using LogFinder.BusinesLogic.Models;

namespace LogFinder.BusinesLogic.Services;

/// <summary>
/// Service for parsing data from files.
/// </summary>
public interface IDataParserService
{
    /// <summary>
    /// Parses data from files located in the specified directory.
    /// </summary>
    List<RowDictionary> GetParsedData(string? path = null);
}