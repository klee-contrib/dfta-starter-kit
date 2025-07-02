using KleeContrib.Dfta.Clients.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KleeContrib.Dfta.Migrations;

/// <summary>
/// DbContextFactory pour les migrations EF.
/// </summary>
public class KleeContribDftaDbContextFactory : IDesignTimeDbContextFactory<KleeContribDftaDbContext>
{
    /// <inheritdoc cref="IDesignTimeDbContextFactory{TContext}.CreateDbContext" />
    public KleeContribDftaDbContext CreateDbContext(string[] args)
    {
        var server = Environment.GetEnvironmentVariable("database_server");
        var database = args.Length > 0 ? args[0] : Environment.GetEnvironmentVariable("database_name");
        var userId = Environment.GetEnvironmentVariable("database_admin_userid");
        var password = Environment.GetEnvironmentVariable("database_admin_secret");

        Console.WriteLine($"Base de données : {database} sur {server}");

        var connectionString = $"Server={server};Database={database};User Id={userId};Password={password};Port=5432;Ssl Mode={(server == "localhost" ? "Disable" : "VerifyFull")}";
        var optionsBuilder = new DbContextOptionsBuilder<KleeContribDftaDbContext>();
        optionsBuilder
            .UseNpgsql(connectionString, o => o
                .CommandTimeout(120)
                .MigrationsAssembly("KleeContrib.Dfta.Migrations"))
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

        if (server != "localhost")
        {
            optionsBuilder.ReplaceService<IMigrationsSqlGenerator, SetRoleNpgsqlMigrationsSqlGenerator>();
        }

        return new KleeContribDftaDbContext(optionsBuilder.Options);
    }
}