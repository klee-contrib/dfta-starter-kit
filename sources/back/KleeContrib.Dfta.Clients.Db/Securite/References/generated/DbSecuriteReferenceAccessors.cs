////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.Globalization;
using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Common.References.Securite;
using KleeContrib.Dfta.Securite.Queries.References;

namespace KleeContrib.Dfta.Clients.Db.Securite.References;

/// <summary>
/// Implémentation de IDbSecuriteReferenceAccessors.
/// </summary>
/// <param name="dbContext">DbContext.</param>
[RegisterImpl]
public partial class DbSecuriteReferenceAccessors(KleeContribDftaDbContext dbContext) : IDbSecuriteReferenceAccessors
{
    /// <inheritdoc cref="IDbSecuriteReferenceAccessors.LoadDroits" />
    public ICollection<Droit> LoadDroits()
    {
        return (
            from row in dbContext.Droits
            join tra in dbContext.Traductions on new { ResourceKey = row.Libelle, Locale = CultureInfo.CurrentCulture.Name } equals new { tra.ResourceKey, tra.Locale }
            orderby row.Code
            select new Droit
            {
                Code = row.Code,
                Libelle = tra.Label,
                TypeDroitCode = row.TypeDroitCode
            }
        ).ToList();
    }

    /// <inheritdoc cref="IDbSecuriteReferenceAccessors.LoadTypeDroits" />
    public ICollection<TypeDroit> LoadTypeDroits()
    {
        return (
            from row in dbContext.TypeDroits
            join tra in dbContext.Traductions on new { ResourceKey = row.Libelle, Locale = CultureInfo.CurrentCulture.Name } equals new { tra.ResourceKey, tra.Locale }
            orderby row.Libelle
            select new TypeDroit
            {
                Code = row.Code,
                Libelle = tra.Label
            }
        ).ToList();
    }

    /// <inheritdoc cref="IDbSecuriteReferenceAccessors.LoadTypeUtilisateurs" />
    public ICollection<TypeUtilisateur> LoadTypeUtilisateurs()
    {
        return (
            from row in dbContext.TypeUtilisateurs
            join tra in dbContext.Traductions on new { ResourceKey = row.Libelle, Locale = CultureInfo.CurrentCulture.Name } equals new { tra.ResourceKey, tra.Locale }
            orderby row.Libelle
            select new TypeUtilisateur
            {
                Code = row.Code,
                Libelle = tra.Label
            }
        ).ToList();
    }
}
