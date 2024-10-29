using LogFinder.BusinesLogic.Models;

namespace LogFinder.BusinesLogic.Utilities;

/// <summary>
/// Utility which is responsible for triggering alerts.
/// </summary>
public static class AlertUtility
{
    /// <summary>
    /// Triggers alerts by severity level.
    /// </summary>
    public static void TriggerAlert(List<RowDictionary> rows)
    {
        foreach (RowDictionary row in rows)
        {
            if (row.TryGetValue("severity", out var severity))
            {
                int severityLevel = int.Parse(severity);

                Console.WriteLine(severityLevel is >= 1 and <= 10
                    ? $"ALERT: [Severity {severityLevel}] found."
                    : $"ALERT: unknown severity level: [Severity {severityLevel}].");
            }
        }
    }
}
