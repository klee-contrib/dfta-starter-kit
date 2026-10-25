////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Commands.Models;

/// <summary>
/// Détail d'un utilisateur en écriture.
/// </summary>
public partial record UtilisateurCommand
{
    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [StringLength(100)]
    public required string Nom { get; set; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [StringLength(100)]
    public required string Prenom { get; set; }

    /// <summary>
    /// Email de l'utilisateur.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Age de l'utilisateur.
    /// </summary>
    public DateOnly? DateNaissance { get; set; }

    /// <summary>
    /// Adresse de l'utilisateur.
    /// </summary>
    [StringLength(100)]
    public string? Adresse { get; set; }

    /// <summary>
    /// Si l'utilisateur est actif.
    /// </summary>
    public bool Actif { get; set; } = true;

    /// <summary>
    /// Profil de l'utilisateur.
    /// </summary>
    public required int ProfilId { get; set; }

    /// <summary>
    /// Type d'utilisateur.
    /// </summary>
    [ReferencedType(typeof(TypeUtilisateur))]
    public TypeUtilisateur.Codes TypeUtilisateurCode { get; set; } = TypeUtilisateur.Codes.GEST;
}
