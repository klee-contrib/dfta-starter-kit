////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Clients.Db.Reference;

/// <summary>
/// This interface was automatically generated. It contains all the operations to load the reference lists declared in module Securite.
/// </summary>
/// <param name="dbContext">DbContext.</param>
[RegisterImpl]
public partial class SecuriteReferenceAccessors(KleeContribDftaDbContext dbContext) : ISecuriteReferenceAccessors
{
    /// <inheritdoc cref="ISecuriteReferenceAccessors.LoadDroits" />
    public ICollection<Droit> LoadDroits()
    {
        return dbContext.Droits.OrderBy(row => row.Code).ToList();
    }

    /// <inheritdoc cref="ISecuriteReferenceAccessors.LoadTypeDroits" />
    public ICollection<TypeDroit> LoadTypeDroits()
    {
        return dbContext.TypeDroits.OrderBy(row => row.Libelle).ToList();
    }

    /// <inheritdoc cref="ISecuriteReferenceAccessors.LoadTypeUtilisateurs" />
    public ICollection<TypeUtilisateur> LoadTypeUtilisateurs()
    {
        return dbContext.TypeUtilisateurs.OrderBy(row => row.Libelle).ToList();
    }
}
