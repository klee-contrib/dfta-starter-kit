////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Queries.Models;

/// <summary>
/// Détail d'un profil en lecture.
/// </summary>
public partial record ProfilRead
{
    /// <summary>
    /// Id technique.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Libellé du profil.
    /// </summary>
    [StringLength(100)]
    public required string Libelle { get; init; }

    /// <summary>
    /// Date de création de l'utilisateur.
    /// </summary>
    public required DateTime DateCreation { get; init; }

    /// <summary>
    /// Date de modification de l'utilisateur.
    /// </summary>
    public DateTime? DateModification { get; init; }

    /// <summary>
    /// Code du droit.
    /// </summary>
    [ReferencedType(typeof(Droit))]
    public required IList<Droit.Codes> DroitCodes { get; init; }

    /// <summary>
    /// Utilisateurs ayant ce profil.
    /// </summary>
    public IList<UtilisateurItem> Utilisateurs { get; init; } = [];
}
