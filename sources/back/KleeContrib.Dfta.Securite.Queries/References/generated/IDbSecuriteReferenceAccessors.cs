////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Queries.References;

/// <summary>
/// Accesseurs de listes de référence persistées.
/// </summary>
[RegisterContract]
public partial interface IDbSecuriteReferenceAccessors
{
    /// <summary>
    /// Accesseur de référence pour le type Droit.
    /// </summary>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Liste de Droit.</returns>
    [ReferenceAccessor]
    Task<ICollection<Droit>> LoadDroits(CancellationToken ct = default);

    /// <summary>
    /// Accesseur de référence pour le type TypeDroit.
    /// </summary>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Liste de TypeDroit.</returns>
    [ReferenceAccessor]
    Task<ICollection<TypeDroit>> LoadTypeDroits(CancellationToken ct = default);

    /// <summary>
    /// Accesseur de référence pour le type TypeUtilisateur.
    /// </summary>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Liste de TypeUtilisateur.</returns>
    [ReferenceAccessor]
    Task<ICollection<TypeUtilisateur>> LoadTypeUtilisateurs(CancellationToken ct = default);
}
