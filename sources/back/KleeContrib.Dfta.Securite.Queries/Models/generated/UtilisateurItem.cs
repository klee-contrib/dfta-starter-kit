////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Queries.Models;

/// <summary>
/// Détail d'un utilisateur en liste.
/// </summary>
public partial record UtilisateurItem
{
    /// <summary>
    /// Id de l'utilisateur.
    /// </summary>
    [Domain(Domains.Id)]
    public required int Id { get; init; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Nom { get; init; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Prenom { get; init; }

    /// <summary>
    /// Email de l'utilisateur.
    /// </summary>
    [Domain(Domains.Email)]
    public required string Email { get; init; }

    /// <summary>
    /// Type d'utilisateur.
    /// </summary>
    [ReferencedType(typeof(TypeUtilisateur))]
    [Domain(Domains.Code)]
    public TypeUtilisateur.Codes TypeUtilisateurCode { get; init; } = TypeUtilisateur.Codes.GEST;
}
