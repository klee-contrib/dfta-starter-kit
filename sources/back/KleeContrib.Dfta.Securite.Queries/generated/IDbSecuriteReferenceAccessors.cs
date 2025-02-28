////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Queries;

/// <summary>
/// Accesseurs de listes de référence persistées.
/// </summary>
[RegisterContract]
public partial interface IDbSecuriteReferenceAccessors
{
    /// <summary>
    /// Accesseur de référence pour le type Droit.
    /// </summary>
    /// <returns>Liste de Droit.</returns>
    [ReferenceAccessor]
    ICollection<Droit> LoadDroits();

    /// <summary>
    /// Accesseur de référence pour le type TypeDroit.
    /// </summary>
    /// <returns>Liste de TypeDroit.</returns>
    [ReferenceAccessor]
    ICollection<TypeDroit> LoadTypeDroits();

    /// <summary>
    /// Accesseur de référence pour le type TypeUtilisateur.
    /// </summary>
    /// <returns>Liste de TypeUtilisateur.</returns>
    [ReferenceAccessor]
    ICollection<TypeUtilisateur> LoadTypeUtilisateurs();
}
