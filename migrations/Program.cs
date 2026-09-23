using KleeContrib.Dfta.Clients.Db;
using KleeContrib.Dfta.Migrations.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

var host = Host.CreateApplicationBuilder(args);

host.Services.AddLogging(l =>
        l.AddConsole(f => f.FormatterName = "migrations")
            .AddConsoleFormatter<MigrationsConsoleFormatter, ConsoleFormatterOptions>()
    )
    .AddDbContext<KleeContribDftaDbContext>(options =>
    {
        options
            .UseNpgsql(
                host.Configuration.GetDatabaseConnectionString(),
                o => o.CommandTimeout(120).MigrationsAssembly("KleeContrib.Dfta.Migrations")
            )
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

        if (host.Configuration["Database:Server"] != "localhost")
        {
            options.ReplaceService<IMigrationsSqlGenerator, SetRoleNpgsqlMigrationsSqlGenerator>();
        }
    });

var app = host.Build();

await using var scope = app.Services.CreateAsyncScope();

var dbContext = scope.ServiceProvider.GetRequiredService<KleeContribDftaDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

var isReset = false;

var flatArgs = string.Join(' ', args);
logger.LogInformation($"Lancement des migrations avec '{flatArgs}'...");
logger.LogInformation(
    $"Base de données : {host.Configuration["Database:Database"]} sur {host.Configuration["Database:Server"]}{Environment.NewLine}"
);

async Task DropDatabase()
{
    isReset = true;
    logger.LogWarning("Attention, la base de données va être réinitialisée...");
    await dbContext.Database.EnsureDeletedAsync();
    logger.LogWarning("La base de données a été supprimée avec succès.");
}

var isCheck = flatArgs.Contains("--check");

if (!isCheck)
{
    if (flatArgs.Contains("--reset if-impossible"))
    {
        logger.LogWarning(
            "Attention, la base de données sera réinitialisée si la migration est impossible."
        );
    }
    else if (flatArgs.Contains("--reset true"))
    {
        await DropDatabase();
    }
}

var pending = isReset ? [] : await dbContext.Database.GetPendingMigrationsAsync();
var applied = isReset ? [] : await dbContext.Database.GetAppliedMigrationsAsync();
var all = dbContext.Database.GetMigrations();

if (pending.Any())
{
    logger.LogInformation(
        $"Migrations en attente : {string.Join(", ", pending.Select(p => $"'{p}'"))}"
    );
}

var missing = applied.Except(all);

if (missing.Any())
{
    if (!isCheck && flatArgs.Contains("--reset if-impossible"))
    {
        logger.LogWarning(
            $"Migrations manquantes : {string.Join(", ", missing.Select(m => $" '{m}'"))}"
        );
        await DropDatabase();
    }
    else
    {
        logger.LogError(
            $"Migrations manquantes : {string.Join(", ", missing.Select(m => $"'{m}'"))}"
        );
        logger.LogInformation(string.Empty);
        logger.LogError($"La mise à jour de la base de données est IMPOSSIBLE.");
        return 1;
    }
}

if (isCheck)
{
    logger.LogInformation(string.Empty);
    logger.LogInformation($"La mise à jour de la base de données est possible.");
}
else
{
    await dbContext.Database.MigrateAsync();
}

return 0;
