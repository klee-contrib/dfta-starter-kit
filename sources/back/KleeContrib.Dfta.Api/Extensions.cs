namespace KleeContrib.Dfta.Api;

/// <summary>
/// Cool.
/// </summary>
public static class Extensions
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
