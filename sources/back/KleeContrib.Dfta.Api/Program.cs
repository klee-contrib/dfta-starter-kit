﻿using System.Text.Json.Serialization;
using Azure.Core;
using Azure.Identity;
using Kinetix.EFCore;
using Kinetix.Monitoring.Core;
using Kinetix.Monitoring.Insights;
using Kinetix.Services;
using Kinetix.Web;
using Kinetix.Web.Filters;
using KleeContrib.Dfta.Api;
using KleeContrib.Dfta.Clients.Db;
using KleeContrib.Dfta.Clients.Db.Securite.Profils;
using KleeContrib.Dfta.Securite.Commands.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Npgsql;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddLogging(l => l.AddSimpleConsole())
    .AddApplicationInsightsTelemetry(o =>
    {
        o.AddAutoCollectedMetricExtractor = false;
        o.EnableDiagnosticsTelemetryModule = false;
        o.EnableEventCounterCollectionModule = false;
        o.EnablePerformanceCounterCollectionModule = false;
        o.EnableQuickPulseMetricStream = false;
    })
    .AddApplicationInsightsTelemetryProcessor<MonitoringTelemetryFilter>()
    .AddApplicationInsightsTelemetryProcessor<DbCommandTelemetryProcessorExt>();

services
    .AddAuthentication()
    .AddMicrosoftIdentityWebApi(builder.Configuration);

services
    .AddAuthorization(options =>
    {
        options.FallbackPolicy = options.DefaultPolicy;
    });

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetDatabaseConnectionString());

if (string.IsNullOrWhiteSpace(builder.Configuration["Database:Password"]))
{
    var credential = new DefaultAzureCredential();

    dataSourceBuilder.UsePeriodicPasswordProvider(
        async (settings, ct) => (await credential.GetTokenAsync(new TokenRequestContext(["https://ossrdbms-aad.database.windows.net/.default"]), ct)).Token,
        TimeSpan.FromMinutes(5),
        TimeSpan.FromSeconds(5));
}

await using var dataSource = dataSourceBuilder.Build();

services
    .AddMonitoring()
    .AddServices("KleeContrib.Dfta", o =>
    {
        o.AddAssemblies(typeof(ProfilMutations).Assembly);
        o.AddAssemblies(typeof(ProfilCommands).Assembly);
    })
    .AddEFCore<KleeContribDftaDbContext>(o => o.UseNpgsql(dataSource));

services
    .AddKinetixExceptionHandler()
    .AddControllers()
        .ConfigureInvalidModelStateSerialization()
        .AddJsonOptions(j => j.JsonSerializerOptions
            .ConfigureSerializerDefaults()
            .AddConverter<JsonStringEnumConverter>());

services.ConfigureHttpJsonOptions(j => j.SerializerOptions
    .ConfigureSerializerDefaults()
    .AddConverter<JsonStringEnumConverter>());

var app = builder.Build();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
    .AddEndpointFilter<TransactionFilter>()
    .AddEndpointFilter<UtcDateFilter>();

app.MapReferenceEndpoints("api/references");
app.MapGet("api/adresses", async (string query) =>
{
    if (query.Trim().Length < 3)
    {
        return new List<string>().Select(e => new { Key = e, Label = e });
    }

    var client = new HttpClient();
    var response = await client.GetFromJsonAsync<Result>($"https://api-adresse.data.gouv.fr/search/?q={query}&limit=10");
    return response!.Features.Select(f => new { Key = f.Properties.Label, f.Properties.Label });
});
await app.RunAsync();

#pragma warning disable

record Result(Feature[] Features);

record Feature(Properties Properties);

record Properties(string Label);