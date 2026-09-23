using KleeContrib.Dfta.Clients.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace KleeContrib.Dfta.Migrations.Migrations;

/// <inheritdoc />
[DbContext(typeof(KleeContribDftaDbContext))]
[Migration("20260101000000_CreateDb")]
public class CreateDb : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

        var isLocal = config["Database:Server"] == "localhost";
        var user = Environment.GetEnvironmentVariable("AppUserId");

        if (isLocal)
        {
            // Création et configuration user applicatif local.
            migrationBuilder.Sql(
                $"""
                DO $$ BEGIN CREATE USER "{user}" WITH PASSWORD 'local'; EXCEPTION WHEN duplicate_object THEN null; END $$;
                GRANT USAGE ON SCHEMA public TO "{user}";
                ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO "{user}";
                """
            );
        }
        else
        {
            // Création et configuration user applicatif sur Azure.
            var dbName = config["Database:Database"]!;

            using var dataSource = NpgsqlDataSource.Create(
                config.GetDatabaseConnectionString().Replace(dbName, "postgres")
            );

            using var cmd = dataSource.CreateCommand(
                $"""
                ALTER DATABASE "{dbName}" OWNER TO "azure_pg_admin";
                DO $$ BEGIN PERFORM pgaadauth_create_principal('{user}', false, false); EXCEPTION WHEN duplicate_object THEN null; END $$;
                """
            );
            cmd.ExecuteNonQuery();

            migrationBuilder.Sql(
                $"""
                SET ROLE azure_pg_admin; GRANT USAGE ON SCHEMA public TO "{user}";
                SET ROLE azure_pg_admin; ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO "{user}";
                """
            );
        }

        // Configuration recherche full-text.
        migrationBuilder.Sql(
            """
                CREATE EXTENSION unaccent;
                CREATE TEXT SEARCH CONFIGURATION simple_unaccent ( COPY = simple );
                ALTER TEXT SEARCH CONFIGURATION simple_unaccent ALTER MAPPING FOR hword, hword_part, word WITH unaccent, simple;
            """
        );
    }
}
