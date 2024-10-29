namespace LogFinder.DataLayer.Settings;

/// <summary>
/// Database settings.
/// </summary>
public class DatabaseSettings
{
    /// <summary>
    /// Section name.
    /// </summary>
    public const string Section = "Database";

    /// <summary>
    /// Connection string.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Database name.
    /// </summary>
    public string Database { get; set; } = string.Empty;

    /// <summary>
    /// Collection name.
    /// </summary>
    public string Collection { get; set; } = string.Empty;
}
