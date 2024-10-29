namespace LogFinder.BusinesLogic.Models;

/// <summary>
/// Directory settings.
/// </summary>
public class DirectorySettings
{
    /// <summary>
    /// Section name.
    /// </summary>
    public const string Section = "Directory";

    /// <summary>
    /// Path.
    /// </summary>
    public string Path { get; set; } = string.Empty;
}
