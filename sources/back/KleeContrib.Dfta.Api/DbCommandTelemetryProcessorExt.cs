using Kinetix.Monitoring.Insights;
using Microsoft.ApplicationInsights.Extensibility;

namespace KleeContrib.Dfta.Api;

/// <summary>
/// Constructeur.
/// </summary>
/// <param name="next">Processor suivant.</param>
/// <param name="config">Composant injecté.</param>
public class DbCommandTelemetryProcessorExt(ITelemetryProcessor next, IConfiguration config)
    : DbCommandTelemetryProcessor(next, new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>() { ["ConnectionStrings:default"] = config.GetDatabaseConnectionString() }).Build())
{
}
