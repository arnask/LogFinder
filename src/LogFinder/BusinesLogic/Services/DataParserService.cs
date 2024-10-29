using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using LogFinder.BusinesLogic.Models;

namespace LogFinder.BusinesLogic.Services;

/// <inheritdoc cref="IDataParserService"/>
public class DataParserService(DirectorySettings directorySettings) : IDataParserService
{
    /// <inheritdoc/>
    public List<RowDictionary> GetParsedData(string? path = null)
    {
        string directoryPath = string.IsNullOrWhiteSpace(path) ? directorySettings.Path : path;

        if (!Directory.Exists(directoryPath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {directoryPath}.");
        }

        var files = Directory.GetFiles(directoryPath, "*.csv");

        if (files.Length == 0)
        {
            throw new FileNotFoundException($"No files were found in selected directory: {directoryPath}.");
        }

        var rows = new List<RowDictionary>();

        foreach (var file in files)
        {
            try
            {
                var parsedRows = ParseCsvFile(file);
                rows.AddRange(parsedRows);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing file {file}: {ex.Message}.");
            }
        }

        return rows;
    }

    private static List<RowDictionary> ParseCsvFile(string filePath)
    {
        using var streamReader = new StreamReader(filePath);
        using var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture));

        var fileRows = new List<RowDictionary>();

        if (!csvReader.Read() || !csvReader.ReadHeader())
        {
            throw new InvalidDataException($"File {filePath} does not contain headers.");
        }

        List<string> headers = csvReader.HeaderRecord?.ToList()
            ?? throw new NullReferenceException($"No headers were found in file {filePath}.");

        while (csvReader.Read())
        {
            var row = GetRow(headers, csvReader);
            fileRows.Add(row);
        }

        return fileRows;
    }

    private static RowDictionary GetRow(List<string> headers, CsvReader row)
    {
        var rowDictionary = new RowDictionary();

        foreach (var header in headers)
        {
            string value = row.GetField(header) ?? string.Empty;
            rowDictionary[header] = value;
        }

        return rowDictionary;
    }
}
