////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Common.References.Securite;
using KleeContrib.Dfta.Securite.Queries;

namespace KleeContrib.Dfta.Clients.Db.Reference;

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
        return dbContext.Droits.OrderBy(row => row.Code).ToList();
    }

    /// <inheritdoc cref="IDbSecuriteReferenceAccessors.LoadTypeDroits" />
    public ICollection<TypeDroit> LoadTypeDroits()
    {
        return dbContext.TypeDroits.OrderBy(row => row.Libelle).ToList();
    }

    /// <inheritdoc cref="IDbSecuriteReferenceAccessors.LoadTypeUtilisateurs" />
    public ICollection<TypeUtilisateur> LoadTypeUtilisateurs()
    {
        return dbContext.TypeUtilisateurs.OrderBy(row => row.Libelle).ToList();
    }
}
