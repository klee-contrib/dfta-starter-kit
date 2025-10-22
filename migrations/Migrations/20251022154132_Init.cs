using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KleeContrib.Dfta.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "profils",
                columns: table => new
                {
                    pro_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pro_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    pro_date_creation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pro_date_modification = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profils", x => x.pro_id);
                });

            migrationBuilder.CreateTable(
                name: "traductions",
                columns: table => new
                {
                    trd_resource_key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    trd_locale = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    trd_label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_traductions", x => new { x.trd_resource_key, x.trd_locale });
                });

            migrationBuilder.CreateTable(
                name: "type_droits",
                columns: table => new
                {
                    tdr_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    tdr_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_droits", x => x.tdr_code);
                });

            migrationBuilder.CreateTable(
                name: "type_utilisateurs",
                columns: table => new
                {
                    tut_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    tut_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_utilisateurs", x => x.tut_code);
                });

            migrationBuilder.CreateTable(
                name: "droits",
                columns: table => new
                {
                    dro_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    dro_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tdr_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_droits", x => x.dro_code);
                    table.ForeignKey(
                        name: "FK_droits_type_droits_tdr_code",
                        column: x => x.tdr_code,
                        principalTable: "type_droits",
                        principalColumn: "tdr_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "utilisateurs",
                columns: table => new
                {
                    uti_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uti_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    uti_prenom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    uti_email = table.Column<string>(type: "text", nullable: false),
                    uti_date_naissance = table.Column<DateOnly>(type: "date", nullable: true),
                    uti_adresse = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    uti_actif = table.Column<bool>(type: "boolean", nullable: false),
                    pro_id = table.Column<int>(type: "integer", nullable: false),
                    tut_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    uti_nom_fichier_photo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    uti_date_creation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    uti_date_modification = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utilisateurs", x => x.uti_id);
                    table.ForeignKey(
                        name: "FK_utilisateurs_profils_pro_id",
                        column: x => x.pro_id,
                        principalTable: "profils",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_utilisateurs_type_utilisateurs_tut_code",
                        column: x => x.tut_code,
                        principalTable: "type_utilisateurs",
                        principalColumn: "tut_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "droit_profils",
                columns: table => new
                {
                    dro_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    pro_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_droit_profils", x => new { x.dro_code, x.pro_id });
                    table.ForeignKey(
                        name: "FK_droit_profils_droits_dro_code",
                        column: x => x.dro_code,
                        principalTable: "droits",
                        principalColumn: "dro_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_droit_profils_profils_pro_id",
                        column: x => x.pro_id,
                        principalTable: "profils",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "profils",
                columns: new[] { "pro_id", "pro_date_creation", "pro_date_modification", "pro_libelle" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 22, 15, 41, 30, 631, DateTimeKind.Utc).AddTicks(7622), null, "Profil 1" },
                    { 2, new DateTime(2025, 10, 22, 15, 41, 30, 631, DateTimeKind.Utc).AddTicks(7755), null, "Profil 2" },
                    { 3, new DateTime(2025, 10, 22, 15, 41, 30, 631, DateTimeKind.Utc).AddTicks(7756), null, "Profil 3" },
                    { 4, new DateTime(2025, 10, 22, 15, 41, 30, 631, DateTimeKind.Utc).AddTicks(7760), null, "Profil 4" }
                });

            migrationBuilder.InsertData(
                table: "traductions",
                columns: new[] { "trd_locale", "trd_resource_key", "trd_label" },
                values: new object[,]
                {
                    { "en-US", "securite.droit.values.Create", "Create" },
                    { "fr-FR", "securite.droit.values.Create", "Création" },
                    { "en-US", "securite.droit.values.Delete", "Delete" },
                    { "fr-FR", "securite.droit.values.Delete", "Suppression" },
                    { "en-US", "securite.droit.values.Read", "Read" },
                    { "fr-FR", "securite.droit.values.Read", "Lecture" },
                    { "en-US", "securite.droit.values.Update", "Update" },
                    { "fr-FR", "securite.droit.values.Update", "Mise à jour" },
                    { "en-US", "securite.typeDroit.values.Admin", "Admin" },
                    { "fr-FR", "securite.typeDroit.values.Admin", "Administration" },
                    { "en-US", "securite.typeDroit.values.Read", "Read" },
                    { "fr-FR", "securite.typeDroit.values.Read", "Lecture" },
                    { "en-US", "securite.typeDroit.values.Write", "Write" },
                    { "fr-FR", "securite.typeDroit.values.Write", "Ecriture" },
                    { "en-US", "securite.typeUtilisateur.values.Admin", "Administrator" },
                    { "fr-FR", "securite.typeUtilisateur.values.Admin", "Administrateur" },
                    { "en-US", "securite.typeUtilisateur.values.Client", "Client" },
                    { "fr-FR", "securite.typeUtilisateur.values.Client", "Client" },
                    { "en-US", "securite.typeUtilisateur.values.Gestionnaire", "Manager" },
                    { "fr-FR", "securite.typeUtilisateur.values.Gestionnaire", "Gestionnaire" }
                });

            migrationBuilder.InsertData(
                table: "type_droits",
                columns: new[] { "tdr_code", "tdr_libelle" },
                values: new object[,]
                {
                    { "ADMIN", "securite.typeDroit.values.Admin" },
                    { "READ", "securite.typeDroit.values.Read" },
                    { "WRITE", "securite.typeDroit.values.Write" }
                });

            migrationBuilder.InsertData(
                table: "type_utilisateurs",
                columns: new[] { "tut_code", "tut_libelle" },
                values: new object[,]
                {
                    { "ADMIN", "securite.typeUtilisateur.values.Admin" },
                    { "CLIENT", "securite.typeUtilisateur.values.Client" },
                    { "GEST", "securite.typeUtilisateur.values.Gestionnaire" }
                });

            migrationBuilder.InsertData(
                table: "droits",
                columns: new[] { "dro_code", "dro_libelle", "tdr_code" },
                values: new object[,]
                {
                    { "CREATE", "securite.droit.values.Create", "WRITE" },
                    { "DELETE", "securite.droit.values.Delete", "ADMIN" },
                    { "READ", "securite.droit.values.Read", "READ" },
                    { "UPDATE", "securite.droit.values.Update", "WRITE" }
                });

            migrationBuilder.InsertData(
                table: "utilisateurs",
                columns: new[] { "uti_id", "uti_actif", "uti_adresse", "uti_date_creation", "uti_date_modification", "uti_date_naissance", "uti_email", "uti_nom", "uti_nom_fichier_photo", "uti_prenom", "pro_id", "tut_code" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2025, 10, 22, 15, 41, 30, 631, DateTimeKind.Utc).AddTicks(9945), null, null, "test1@test.com", "Jean", null, "Michel", 1, "ADMIN" },
                    { 2, true, null, new DateTime(2025, 10, 22, 15, 41, 30, 632, DateTimeKind.Utc).AddTicks(68), null, null, "test2@test.com", "Gerard", null, "Jugnos", 2, "GEST" },
                    { 3, true, null, new DateTime(2025, 10, 22, 15, 41, 30, 632, DateTimeKind.Utc).AddTicks(72), null, null, "test3@test.com", "Gad", null, "El", 3, "CLIENT" },
                    { 4, true, null, new DateTime(2025, 10, 22, 15, 41, 30, 632, DateTimeKind.Utc).AddTicks(74), null, null, "test4@test.com", "Bernard", null, "Bruno", 4, "ADMIN" },
                    { 5, true, null, new DateTime(2025, 10, 22, 15, 41, 30, 632, DateTimeKind.Utc).AddTicks(76), null, null, "test5@test.com", "Sisi", null, "Brindacier", 1, "GEST" },
                    { 6, true, null, new DateTime(2025, 10, 22, 15, 41, 30, 632, DateTimeKind.Utc).AddTicks(78), null, null, "test6@test.com", "Bibi", null, "Baba", 2, "CLIENT" },
                    { 7, true, null, new DateTime(2025, 10, 22, 15, 41, 30, 632, DateTimeKind.Utc).AddTicks(80), null, null, "test7@test.com", "Dédé", null, "Dédé", 3, "GEST" },
                    { 8, true, null, new DateTime(2025, 10, 22, 15, 41, 30, 632, DateTimeKind.Utc).AddTicks(85), null, null, "test8@test.com", "Ran", null, "Tanplan", 4, "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "droit_profils",
                columns: new[] { "dro_code", "pro_id" },
                values: new object[,]
                {
                    { "CREATE", 1 },
                    { "DELETE", 4 },
                    { "READ", 2 },
                    { "UPDATE", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_droit_profils_pro_id",
                table: "droit_profils",
                column: "pro_id");

            migrationBuilder.CreateIndex(
                name: "IX_droits_dro_libelle",
                table: "droits",
                column: "dro_libelle");

            migrationBuilder.CreateIndex(
                name: "IX_droits_tdr_code",
                table: "droits",
                column: "tdr_code");

            migrationBuilder.CreateIndex(
                name: "IX_traductions_trd_resource_key",
                table: "traductions",
                column: "trd_resource_key");

            migrationBuilder.CreateIndex(
                name: "IX_type_droits_tdr_libelle",
                table: "type_droits",
                column: "tdr_libelle");

            migrationBuilder.CreateIndex(
                name: "IX_type_utilisateurs_tut_libelle",
                table: "type_utilisateurs",
                column: "tut_libelle");

            migrationBuilder.CreateIndex(
                name: "IX_utilisateurs_pro_id",
                table: "utilisateurs",
                column: "pro_id");

            migrationBuilder.CreateIndex(
                name: "IX_utilisateurs_tut_code",
                table: "utilisateurs",
                column: "tut_code");

            migrationBuilder.CreateIndex(
                name: "IX_utilisateurs_uti_email",
                table: "utilisateurs",
                column: "uti_email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "droit_profils");

            migrationBuilder.DropTable(
                name: "traductions");

            migrationBuilder.DropTable(
                name: "utilisateurs");

            migrationBuilder.DropTable(
                name: "droits");

            migrationBuilder.DropTable(
                name: "profils");

            migrationBuilder.DropTable(
                name: "type_utilisateurs");

            migrationBuilder.DropTable(
                name: "type_droits");
        }
    }
}
