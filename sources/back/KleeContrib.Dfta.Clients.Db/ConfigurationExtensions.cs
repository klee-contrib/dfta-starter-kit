using Microsoft.Extensions.Configuration;

namespace KleeContrib.Dfta.Clients.Db;

/// <summary>
/// Extensions de configuration pour construire le connection string de la base de données.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Reconstruit le connection string à partir de la config dans "Database".
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    /// <returns>Connection string.</returns>
    public static string GetDatabaseConnectionString(this IConfiguration configuration)
    {
        return string.Join(
            ';',
            configuration
                .GetSection("Database")
                .AsEnumerable()
                .Where(kv => !string.IsNullOrEmpty(kv.Value))
                .Select(kv => $"{kv.Key.Split(":")[^1].Replace('_', ' ')}={kv.Value}")
        );
    }
}
