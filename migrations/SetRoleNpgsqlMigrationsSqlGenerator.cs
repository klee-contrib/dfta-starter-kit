using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;

namespace KleeContrib.Dfta.Migrations;

#pragma warning disable EF1001

/// <summary>
/// Modifie le service qui génère les appels à "CREATE TABLE" pour mettre un "SET ROLE azure_pg_admin" avant, avant de donner l'ownership de toutes les tables
/// au groupe admin et non à la service connection de DevOps uniquement.
/// </summary>
public class SetRoleNpgsqlMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies, INpgsqlSingletonOptions npgsqlSingletonOptions)
    : NpgsqlMigrationsSqlGenerator(dependencies, npgsqlSingletonOptions)
{
    protected override void Generate(CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder, bool terminate = true)
    {
        AddSetRole(builder);
        base.Generate(operation, model, builder, terminate);
    }

    private void AddSetRole(MigrationCommandListBuilder builder)
    {
        var sqlHelper = Dependencies.SqlGenerationHelper;

        builder.Append("SET ROLE ")
            .Append("\"azure_pg_admin\"")
            .AppendLine(sqlHelper.StatementTerminator)
            .EndCommand();
    }
}