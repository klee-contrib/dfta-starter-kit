////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.Globalization;
using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Common.References.Securite;
using KleeContrib.Dfta.Securite.Queries.References;
using Microsoft.EntityFrameworkCore;

namespace KleeContrib.Dfta.Clients.Db.Securite.References;

/// <summary>
/// Implémentation de IDbSecuriteReferenceAccessors.
/// </summary>
/// <param name="dbContext">DbContext.</param>
[RegisterImpl]
public partial class DbSecuriteReferenceAccessors(KleeContribDftaDbContext dbContext) : IDbSecuriteReferenceAccessors
{
    /// <inheritdoc cref="IDbSecuriteReferenceAccessors.LoadDroits" />
    public async Task<ICollection<Droit>> LoadDroits(CancellationToken ct = default)
    {
        return await (
            from row in dbContext.Droits
            join tra in dbContext.Traductions on new { ResourceKey = row.Libelle, Locale = CultureInfo.CurrentUICulture.Name } equals new { tra.ResourceKey, tra.Locale }
            orderby row.Code
            select new Droit
            {
                Code = row.Code,
                Libelle = tra.Label,
                TypeDroitCode = row.TypeDroitCode
            }
        ).ToListAsync(ct);
    }

    /// <inheritdoc cref="IDbSecuriteReferenceAccessors.LoadTypeDroits" />
    public async Task<ICollection<TypeDroit>> LoadTypeDroits(CancellationToken ct = default)
    {
        return await (
            from row in dbContext.TypeDroits
            join tra in dbContext.Traductions on new { ResourceKey = row.Libelle, Locale = CultureInfo.CurrentUICulture.Name } equals new { tra.ResourceKey, tra.Locale }
            orderby row.Libelle
            select new TypeDroit
            {
                Code = row.Code,
                Libelle = tra.Label
            }
        ).ToListAsync(ct);
    }

    /// <inheritdoc cref="IDbSecuriteReferenceAccessors.LoadTypeUtilisateurs" />
    public async Task<ICollection<TypeUtilisateur>> LoadTypeUtilisateurs(CancellationToken ct = default)
    {
        return await (
            from row in dbContext.TypeUtilisateurs
            join tra in dbContext.Traductions on new { ResourceKey = row.Libelle, Locale = CultureInfo.CurrentUICulture.Name } equals new { tra.ResourceKey, tra.Locale }
            orderby row.Libelle
            select new TypeUtilisateur
            {
                Code = row.Code,
                Libelle = tra.Label
            }
        ).ToListAsync(ct);
    }
}
