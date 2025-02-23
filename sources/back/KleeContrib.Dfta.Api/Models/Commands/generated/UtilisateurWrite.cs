////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Api.Models.Commands;

/// <summary>
/// Détail d'un utilisateur en écriture.
/// </summary>
public partial record UtilisateurWrite
{
    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string? Nom { get; set; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string? Prenom { get; set; }

    /// <summary>
    /// Email de l'utilisateur.
    /// </summary>
    [Required]
    [Domain(Domains.Email)]
    public string? Email { get; set; }

    /// <summary>
    /// Age de l'utilisateur.
    /// </summary>
    [Domain(Domains.Date)]
    public DateOnly? DateNaissance { get; set; }

    /// <summary>
    /// Adresse de l'utilisateur.
    /// </summary>
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string? Adresse { get; set; }

    /// <summary>
    /// Si l'utilisateur est actif.
    /// </summary>
    [Required]
    [Domain(Domains.Booleen)]
    public bool? Actif { get; set; } = true;

    /// <summary>
    /// Profil de l'utilisateur.
    /// </summary>
    [Required]
    [Domain(Domains.Id)]
    public int? ProfilId { get; set; }

    /// <summary>
    /// Type d'utilisateur.
    /// </summary>
    [Required]
    [ReferencedType(typeof(TypeUtilisateur))]
    [Domain(Domains.Code)]
    public TypeUtilisateur.Codes? TypeUtilisateurCode { get; set; } = TypeUtilisateur.Codes.GEST;
}
