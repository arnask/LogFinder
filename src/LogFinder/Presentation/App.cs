﻿using LogFinder.BusinesLogic.Services;

namespace LogFinder.Presentation;

/// <summary>
/// Main application execution point.
/// </summary>
public class App(IDataParserService dataParserService)
{
    /// <summary>
    /// Runs the application and enters the main execution loop.
    /// </summary>
    public void Run()
    {
        var data = dataParserService.GetParsedData();
    }

}