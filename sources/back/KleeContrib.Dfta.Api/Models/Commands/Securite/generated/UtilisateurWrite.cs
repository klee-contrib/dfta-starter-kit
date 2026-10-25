////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Api.Models.Commands.Securite;

/// <summary>
/// Détail d'un utilisateur en écriture.
/// </summary>
public partial record UtilisateurWrite
{
    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string? Nom { get; set; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string? Prenom { get; set; }

    /// <summary>
    /// Email de l'utilisateur.
    /// </summary>
    [Required]
    public string? Email { get; set; }

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
    [Required]
    public bool? Actif { get; set; } = true;

    /// <summary>
    /// Profil de l'utilisateur.
    /// </summary>
    [Required]
    public int? ProfilId { get; set; }

    /// <summary>
    /// Type d'utilisateur.
    /// </summary>
    [Required]
    [ReferencedType(typeof(TypeUtilisateur))]
    public TypeUtilisateur.Codes? TypeUtilisateurCode { get; set; } = TypeUtilisateur.Codes.GEST;
}
