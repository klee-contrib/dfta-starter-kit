using System.Text.Json.Serialization;
using Kinetix.EFCore;
using Kinetix.Monitoring.Core;
using Kinetix.Services;
using Kinetix.Web;
using Kinetix.Web.Filters;
using KleeContrib.Dfta.Api;
using KleeContrib.Dfta.Clients.Db;
using KleeContrib.Dfta.Clients.Db.Securite.Profils;
using KleeContrib.Dfta.Securite.Commands.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddLogging(l => l.AddSimpleConsole());

services
    .AddAuthentication()
    .AddMicrosoftIdentityWebApi(builder.Configuration);

services
    .AddAuthorization(options =>
    {
        options.FallbackPolicy = options.DefaultPolicy;
    });

services
    .AddMonitoring()
    .AddServices("KleeContrib.Dfta", o =>
    {
        o.AddAssemblies(typeof(ProfilMutations).Assembly);
        o.AddAssemblies(typeof(ProfilCommands).Assembly);
    })
    .AddEFCore<KleeContribDftaDbContext>(o => o
        .UseNpgsql(builder.Configuration.GetDatabaseConnectionString())
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

services
.AddWeb()
    .AddControllers(a =>
    {
        a.Filters.AddService<CultureFilter>();
        a.Filters.AddService<ExceptionFilter>();
        a.Filters.AddService<TransactionFilter>();
        a.Filters.AddService<UtcDateFilter>();
    })
        .AddJsonOptions(j =>
        {
            j.ConfigureSerializer();
            j.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

services.ConfigureHttpJsonOptions(j =>
{
    j.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    j.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapReferenceEndpoints("api/references");
app.MapGet("api/adresses", async (string query) =>
{
    if (query.Trim().Length < 3)
    {
        return new List<string>().Select(e => new { Key = e, Label = e });
    }

    var client = new HttpClient();
    var response = await client.GetFromJsonAsync<Result>($"https://api-adresse.data.gouv.fr/search/?q={query}&limit=10");
    return response.Features.Select(f => new { Key = f.Properties.Label, f.Properties.Label });
});
app.Run();

record Result(Feature[] Features);
record Feature(Properties Properties);
record Properties(string Label);